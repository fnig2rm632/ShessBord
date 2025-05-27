using System;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.DTO.Authentication;
using ShessBord.Interfaces;
using ShessBord.Services;
using WebTest.Dtos.Authification;

namespace ShessBord.ViewModels;

public class RegistrationViewModel : ViewModelBase, IRoutableViewModel
{
    #region Dependency Injection
    
    private readonly IAppLocalizationService _localization;
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppValidationDataService _checkPasswordService;
    private readonly IAuthApiClient _authApiClient;
    private readonly IAppTokenStorage _tokenStorage;


    public ReactiveCommand<Unit,IRoutableViewModel> NextToAuthenticationCommand { get; }
    public ReactiveCommand<Unit,Unit> NextToMenuCommand { get; }
    
    #endregion
    
    #region Change Language

        public ICommand ChangeLanguageCommand { get; }
    
    #endregion

    #region Language Change Fields
    
    public string TextDoYouAlreadyHaveAnAccount => _localization["DoYouAlreadyHaveAnAccount"];
    public string TextRegistration => _localization["Registration"];    
    public string TextRegister => _localization["Register"];
    public string TextWatermarkPassword => _localization["WatermarkPassword"];
    public string TextName => _localization["Name"];

    #endregion
    
    #region Routing
    
    public IScreen HostScreen { get; }
    
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
    
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
    
    private string? _errorText;

    public string? ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
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
    
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
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
    
    public RegistrationViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IServiceProvider serviceProvider, 
        IAppValidationDataService checkPasswordService,
        IAuthApiClient authApiClient,
        IAppTokenStorage tokenStorage)
    { 
        HostScreen = screen;
        _localization = localization;
        _serviceProvider = serviceProvider;
        _checkPasswordService = checkPasswordService;
        _authApiClient = authApiClient;
        _tokenStorage = tokenStorage;

        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code);
            ErrorText = _checkPasswordService.AllValidator(Name, Email, Password, _localization).massage;
        });
        
        NextToAuthenticationCommand = ReactiveCommand.CreateFromObservable(NextToAuthentication);
        NextToMenuCommand = ReactiveCommand.CreateFromTask(NextToMenu);
        
        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
    }
    
    // Переход к Меню
    private async Task NextToMenu()
    {
        try
        {
            IsLoading = true;
            ErrorText = string.Empty;

            var validationPassword = _checkPasswordService.AllValidator(Name,Email,Password, _localization);
            
            if (validationPassword.isValid)
            {
                Error = false;
                
                var response = await _authApiClient.RegisterAsync(new RegisterRequestDto()
                {
                    Email = Email, 
                    Password = Password,
                    Username = Name,
                });
                
                _tokenStorage.SaveTokens(response?.AccessToken!, response?.RefreshToken!, response?.UserId!);
                
                var menuView = _serviceProvider.GetRequiredService<MenuViewModel>();
                await HostScreen.Router.Navigate.Execute(menuView);
            }
            else
            {
                Error = true;
                ErrorText = validationPassword.massage;
            }
        }
        catch (HttpRequestException e)
        {
            ErrorText = "Уже существует такой Email или Имя";
            Error = true;
        }
        catch (Exception e)
        {
            ErrorText = "Ошибка входа: " + e.Message;
            Error = true;
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private void UpdateLocalizedProperties()
    {
        var properties = GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(string));
    
        foreach (var prop in properties)
        {
            this.RaisePropertyChanged(prop.Name);
        }
    }
    
    private IObservable<IRoutableViewModel> NextToAuthentication()
    {
        var authenticationView = _serviceProvider.GetRequiredService<AuthenticationViewModel>();
        return HostScreen.Router.Navigate.Execute(authenticationView);
    }
}