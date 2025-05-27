using System;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.Interfaces;
using ShessBord.Services;

namespace ShessBord.ViewModels;

public class ForgotPasswordViewModel : ViewModelBase, IRoutableViewModel
{
    #region Dependency Injection
    
    private readonly IAppLocalizationService _localization;
    private readonly IServiceProvider _serviceProvider;
    
    public ReactiveCommand<Unit,IRoutableViewModel> NextToAuthenticationCommand { get; }
    
    #endregion
    
    #region Change Language

        public ICommand ChangeLanguageCommand { get; }
    
    #endregion

    #region Language Change Fields
    
    public string TextSingUpButton => _localization["SingUpButton"];
    public string TextWatermarkLogin => _localization["WatermarkLogin"];
    public string TextForgotPassword => _localization["ForgotPassword"];    
    public string TextSubmit => _localization["Submit"];
        
    

    #endregion
    
    #region Routing
    
    public IScreen HostScreen { get; }
    
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
    
    #endregion
    
    public ForgotPasswordViewModel(IScreen screen,IAppLocalizationService localization,IServiceProvider serviceProvider)
    { 
        HostScreen = screen;
        _localization = localization;
        _serviceProvider = serviceProvider;
        
        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code); 
        });
        
        NextToAuthenticationCommand = ReactiveCommand.CreateFromObservable(NextToAuthentication);
        
        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
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
        var authecticationView = _serviceProvider.GetRequiredService<AuthenticationViewModel>();
        return HostScreen.Router.Navigate.Execute(authecticationView);
    }
}