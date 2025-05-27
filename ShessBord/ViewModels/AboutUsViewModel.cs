using System;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.Interfaces;
using ShessBord.Services;

namespace ShessBord.ViewModels;

public class AboutUsViewModel : ViewModelBase, IRoutableViewModel
{
    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppSettingsService _appSettingsService;

        #endregion

    #region Routing
    
        public IScreen HostScreen { get; }
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
        
        public ReactiveCommand<Unit,IRoutableViewModel> NextToAboutUsCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToPlayCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToMenuCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToFriendsCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToSettingsCommand { get; }
    
    #endregion

    #region Change Language

        public ICommand ChangeLanguageCommand { get; }
    
    #endregion

    #region Language Change Fields
    
    public string TextPlay => _localization["Play"];
    public string TextFriend => _localization["Friend"];
    public string TextAboutUs => _localization["AboutUs"];
    public string TextSettings => _localization["Settings"];
    public string TextRowUp => _localization["RollUp"];
    public string TextApplication1 => _localization["Application1"];    
    public string TextApplication2 => _localization["Application2"];
    public string TextApplication3 => _localization["Application3"];
    public string TextApplication4 => _localization["Application4"];
    public string TextApplication5 => _localization["Application5"];
    public string TextApplication6 => _localization["Application6"];
    public string TextApplication7 => _localization["Application7"];
    public string TextApplication8 => _localization["Application8"];
    public string TextApplication9 => _localization["Application9"];
    public string TextWhatThis => _localization["WhatThis"];
    public string TextWhyGo => _localization["WhyGo"];
    public string TextTechnologies => _localization["Technologies"];
    public string TextContacts => _localization["Contacts"];
    
    #endregion

    #region HidePanel

    private bool _isHidePanel;  
    
    public bool IsHidePanel
    {
        get => _isHidePanel;
        set
        {
            this.RaiseAndSetIfChanged(ref _isHidePanel, value);
            BitmapLogo = value ? new(@"Assets\go.png")  : new Bitmap(@"Assets\logo.png");  
            _appSettingsService.IsSidePanelOpen = value;
            WeightPanel = value ? 160 : 60; 
            _appSettingsService.Save();
        }
    }

    private int _weightPanel = 160;

    public int WeightPanel
    {
        get => _weightPanel;
        set => this.RaiseAndSetIfChanged(ref _weightPanel, value);
    }

    private Bitmap _bitmapLogo = new(@"Assets\go.png");  
    
    public Bitmap BitmapLogo
    {
        get => _bitmapLogo;
        set => this.RaiseAndSetIfChanged(ref _bitmapLogo, value);
    }
    
    public ReactiveCommand<Unit,Unit> HidePanelCommand { get; }
    
    #endregion

    public AboutUsViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IServiceProvider serviceProvider,
        IAppSettingsService appSettingsService)
    { 
        HostScreen = screen;
        _localization = localization;
        _serviceProvider = serviceProvider;
        _appSettingsService = appSettingsService;

        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code); 
        });

        HidePanelCommand = ReactiveCommand.Create(OpenAndClosePanel);
        IsHidePanel = _appSettingsService.IsSidePanelOpen;
        
        // NextTo
        NextToAboutUsCommand = ReactiveCommand.CreateFromObservable(NextToAboutUs);
        NextToPlayCommand = ReactiveCommand.CreateFromObservable(NextToPlay);
        NextToMenuCommand = ReactiveCommand.CreateFromObservable(NextToMenu);
        NextToFriendsCommand= ReactiveCommand.CreateFromObservable(NextToFriends);
        NextToSettingsCommand  = ReactiveCommand.CreateFromObservable(NextToSettings);
        
    }
    
    //Открытие и закрытие панели
    private void OpenAndClosePanel() => IsHidePanel = !IsHidePanel;
    
    // К Станице c настройками
    private IObservable<IRoutableViewModel> NextToSettings()
    {
        var settingsView = _serviceProvider.GetRequiredService<SettingsViewModel>();
        return HostScreen.Router.Navigate.Execute(settingsView);
    }
    
    // К Станице о нас
    private IObservable<IRoutableViewModel> NextToAboutUs()
    {
        var aboutUsView = _serviceProvider.GetRequiredService<AboutUsViewModel>();
        return HostScreen.Router.Navigate.Execute(aboutUsView);
    }
    
    // К Старанице Игра
    private IObservable<IRoutableViewModel> NextToPlay()
    {
        var playView = _serviceProvider.GetRequiredService<PlayViewModel>();
        return HostScreen.Router.Navigate.Execute(playView);
    }
    
    // К Станице main в меню
    private IObservable<IRoutableViewModel> NextToMenu()
    {
        var menuView = _serviceProvider.GetRequiredService<MenuViewModel>();
        return HostScreen.Router.Navigate.Execute(menuView);
    }
    
    // К Станице с друзьями
    private IObservable<IRoutableViewModel> NextToFriends()
    {
        var friendsView = _serviceProvider.GetRequiredService<FriendsViewModel>();
        return HostScreen.Router.Navigate.Execute(friendsView);
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