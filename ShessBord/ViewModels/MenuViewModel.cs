using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.DTO.User;
using ShessBord.Interfaces;
using ShessBord.Models;
using ShessBord.Resources;
using ShessBord.Services;

namespace ShessBord.ViewModels;

public class MenuViewModel : ViewModelBase, IRoutableViewModel
{
    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppTokenStorage _tokenStorage;
        private readonly IAppUserService _appUserService;
        private readonly IAppSettingsService _appSettingsService;

        #endregion

    #region Routing
    
        public IScreen HostScreen { get; }
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
        
        public ReactiveCommand<Unit,IRoutableViewModel> NextToAboutUsCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToPlayCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToMenuCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToFriendsCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToAccountCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToHistoryCommand { get; }
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
    public string TextPlay10 => _localization["Play10"];
    public string TextPlay5 => _localization["Play5"];
    public string TextPlayWithFriends => _localization["PlayWithFriends"];
    public string TextNewGame => _localization["NewGame"];
    public string TextStatistic => _localization["Statistics"];
    public string TextParties => _localization["Parties"];
    public string TextBullet => _localization["Bullet"];
    public string TextRapid => _localization["Rapid"];
    public string TextBlitz => _localization["Blitz"];
    public string TextWins => _localization["Wins"];
    public string TextArchiveOfParties => _localization["ArchiveOfParties"];
    public string TextView  => _localization["View"];
    public string TextTime => _localization["Time"];
    public string TextData => _localization["Data"];
    public string TextPlayerWhite => _localization["PlayerWhite"];
    public string TextPlayerBlack => _localization["PlayerBlack"]; 
    public string TextSizeBoard => _localization["SizeBoard"];
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
    
    #region UserData
    
        private string _userData;

        public string UserData
        {
            get => _userData;
            set => this.RaiseAndSetIfChanged(ref _userData, value);
        }
        
        private Statistic _statistic;

        public Statistic Statistic
        {
            get => _statistic;
            set => this.RaiseAndSetIfChanged(ref _statistic, value);
        }
        
    
    #endregion

    #region ClientData

    private UserRequestDto _user;

    public UserRequestDto User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    #endregion

    public MenuViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IServiceProvider serviceProvider,
        IAppTokenStorage appTokenStorage,
        IAppUserService appUserService,
        IAppSettingsService appSettingsService)
    { 
        HostScreen = screen;
        _localization = localization;
        _serviceProvider = serviceProvider;
        _tokenStorage = appTokenStorage;
        _appUserService = appUserService;
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
        NextToFriendsCommand = ReactiveCommand.CreateFromObservable(NextToFriends);
        NextToAccountCommand = ReactiveCommand.CreateFromObservable(NextToAccount);
        NextToHistoryCommand = ReactiveCommand.CreateFromObservable(NextToHistory);
        NextToSettingsCommand  = ReactiveCommand.CreateFromObservable(NextToSettings);
        
        // Client
        _ = LoadUserProfileAsync();
    }
    
    // Загрузка данный профиля
    private async Task LoadUserProfileAsync()
    {
        User = (await _appUserService.GetMainUserAsync())!;

        var mainId = _tokenStorage.GetTokens().userId;
        
        Statistic tmp = new()
        {
            Parties = User.Games.Count(),
            Win = User.Games.Count(x => x.PlayerWinId == mainId)
        };
        
        foreach (var game in User.Games)
        {
            switch (game.Type)
            {
                case "Bullet": tmp.Bullet++; break; 
                case "Blitz": tmp.Blitz++; break; 
                case "Rapid": tmp.Rapid++; break; 
            }
        }
        
        Statistic = tmp;
    }
    
    //Открытие и закрытие панели
    private void OpenAndClosePanel() => IsHidePanel = !IsHidePanel;
    
    // К Станице c настройками
    private IObservable<IRoutableViewModel> NextToSettings()
    {
        var settingsView = _serviceProvider.GetRequiredService<SettingsViewModel>();
        return HostScreen.Router.Navigate.Execute(settingsView);
    }
    
    // К Станице c Аккаунтом 
    private IObservable<IRoutableViewModel> NextToAccount()
    {
        var accountView = _serviceProvider.GetRequiredService<AccountViewModel>();
        return HostScreen.Router.Navigate.Execute(accountView);
    }
    
    // К Станице Истории
    private IObservable<IRoutableViewModel> NextToHistory()
    {
        var historyView = _serviceProvider.GetRequiredService<HistoryViewModel>();
        return HostScreen.Router.Navigate.Execute(historyView);
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