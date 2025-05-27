using System;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.Interfaces;
using ShessBord.Services;
using WebTest.Dtos.Authification;

namespace ShessBord.ViewModels;

public class AuthenticationViewModel : ViewModelBase, IRoutableViewModel
{
    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthApiClient _authApiClient;
        private readonly IAppTokenStorage _tokenStorage;
    
    #endregion

    #region Routing
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToForgotPasswordCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextRegistrationCommand { get; }
        public ReactiveCommand<Unit, Unit> NextToMenuCommand { get; }
    
    #endregion

    #region Change Language

        public ICommand ChangeLanguageCommand { get; }
    
    #endregion

    #region Language Change Fields
    
        public string TextSingUpButton => _localization["SingUpButton"];
        public string TextWatermarkLogin => _localization["WatermarkLogin"];
        public string TextWatermarkPassword => _localization["WatermarkPassword"];
        public string TextForgotPassword => _localization["ForgotPassword"];    
        public string TextLogIn => _localization["LogIn"];
        public string TextSingUpButtonAndroid => _localization["SingUpButtonAndroid"];
        public string TextInvalidPasswordOrEmailAddress => _localization["InvalidPasswordOrEmailAddress"];

    #endregion

    #region PasswordCharing
        private bool _isVisiblePassword;
        
        private string _passwordChar = "•";

        public bool IsVisiblePassword
        {
            get => _isVisiblePassword;
            set
            {
                this.RaiseAndSetIfChanged(ref _isVisiblePassword, value);
                PasswordChar = value ? "" : "•"; // Если пароль видим, убираем маскировку
            }
        }
        public string PasswordChar
        {
            get => _passwordChar;
            set => this.RaiseAndSetIfChanged(ref _passwordChar, value);
        }

    #endregion

    #region Login

    private bool _error;

    public bool Error
    {
        get => _error;
        set => this.RaiseAndSetIfChanged(ref _error, value);
    }
    
        private string _password = string.Empty;

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
    
        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
    
        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get => _errorMessage; 
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }
    
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }
        

    #endregion

    public AuthenticationViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IServiceProvider serviceProvider,
        IAuthApiClient authApiClient,
        IAppTokenStorage tokenStorage)
    { 
        HostScreen = screen;
        _localization = localization;
        _serviceProvider = serviceProvider;
        _authApiClient = authApiClient;
        _tokenStorage = tokenStorage;
        
        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code); 
        });
        
        NextToForgotPasswordCommand = ReactiveCommand.CreateFromObservable(NextToForgotPassword);
        NextRegistrationCommand = ReactiveCommand.CreateFromObservable(NextToRegistration);
        NextToMenuCommand = ReactiveCommand.CreateFromTask(NextToMenu);
    }

    // Переход к забыл пароль
    private IObservable<IRoutableViewModel> NextToForgotPassword()
    {
        var forgotPasswordView = _serviceProvider.GetRequiredService<ForgotPasswordViewModel>();
        return HostScreen.Router.Navigate.Execute(forgotPasswordView);
    }
    // Переоход к регистрации
    private IObservable<IRoutableViewModel> NextToRegistration()
    {
        var registrationView = _serviceProvider.GetRequiredService<RegistrationViewModel>();
        return HostScreen.Router.Navigate.Execute(registrationView);
    }
    
    // Переход к Меню
    private async Task NextToMenu()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            
            var response = await _authApiClient.LoginAsync(new LoginRequestDto
            {
                Email = Email, 
                Password = Password
            });
            
            _tokenStorage.SaveTokens(response?.AccessToken!, response?.RefreshToken! , response?.UserId!);
            
            Error = false;
            
            var menuView = _serviceProvider.GetRequiredService<MenuViewModel>();
            await HostScreen.Router.Navigate.Execute(menuView);
        }
        catch (HttpRequestException e)
        {
            ErrorMessage = "Ошибка входа: " + e.Message;
            Error = true;
        }
        catch (Exception e)
        {
            ErrorMessage = "Ошибка входа: " + e.Message;
            Error = true;
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    // Поискт обновляймых string полей
    private void UpdateLocalizedProperties()
    {
        var properties = GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(string));
    
        foreach (var prop in properties)
        {
            this.RaisePropertyChanged(prop.Name);
        }
    }
    
}