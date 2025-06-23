using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.Controls;
using ShessBord.Interfaces;
using ShessBord.Models;
using ShessBord.Services;
using Color = System.Drawing.Color;

namespace ShessBord.ViewModels;

public class PlayViewModel : ViewModelBase, IRoutableViewModel
{
    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppSettingsService _appSettingsService;
        private readonly IAppMatchmakingService _appMatchmakingService;
        private readonly IGameWebSocketService _gameWebSocketService;

        #endregion

    #region Routing
    
        public IScreen HostScreen { get; }
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
        
        public ReactiveCommand<Unit,IRoutableViewModel> NextToAboutUsCommand { get; }
        public ReactiveCommand<Unit,Unit> NextToPlayCommand { get; }
        
        public ReactiveCommand<Unit,Unit> CancalLoadCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToMenuCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToFriendsCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToSettingsCommand { get; }
        public ReactiveCommand<Unit,Unit> NextToMatchCommand { get; }
    
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
    public string TextBoardSize => _localization["BoardSize"];
    
    public string TextTimeControl => _localization["TimeControl"];
    
    #endregion

    #region ChooseBoard
    
    private bool _isLoading = false;

    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }
    
    public ReactiveCommand<string, Unit> SelectOptionCommand { get; }

    private int SelectedBoardIndex { get; set; } = 9;
    
    private Bitmap _bimapBoard = new Bitmap(@"Assets\mini.png");
    
    public Bitmap BimapBoard
    {
        get => _bimapBoard;
        set => this.RaiseAndSetIfChanged(ref _bimapBoard, value);
    }
    
    

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

    public PlayViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IServiceProvider serviceProvider,
        IAppSettingsService appSettingsService,
        IAppMatchmakingService appMatchmakingService,
        IGameWebSocketService gameWebSocketService)
    { 
        HostScreen = screen;
        _localization = localization;
        _serviceProvider = serviceProvider;
        _appSettingsService = appSettingsService;
        _appMatchmakingService = appMatchmakingService;
        _gameWebSocketService = gameWebSocketService;

        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code); 
        });

        HidePanelCommand = ReactiveCommand.Create(OpenAndClosePanel);
        IsHidePanel = _appSettingsService.IsSidePanelOpen;

        SelectOptionCommand = ReactiveCommand.Create<string>(option => ChangeBoardSize(option));
        
        // NextTo
        NextToAboutUsCommand = ReactiveCommand.CreateFromObservable(NextToAboutUs);
        NextToPlayCommand = ReactiveCommand.Create(NextToPlay);
        NextToMenuCommand = ReactiveCommand.CreateFromObservable(NextToMenu);
        NextToFriendsCommand= ReactiveCommand.CreateFromObservable(NextToFriends);
        NextToSettingsCommand  = ReactiveCommand.CreateFromObservable(NextToSettings);
        NextToMatchCommand = ReactiveCommand.Create(NextToMatch);
        CancalLoadCommand = ReactiveCommand.Create(CancelLoad);

    }
    
    //Открытие и закрытие панели
    private void OpenAndClosePanel() => IsHidePanel = !IsHidePanel;

    private void CancelLoad()
    {
        IsLoading = false;
        _appMatchmakingService.Cancel();

    }
    
    // К Станице c Матчу
    private async void NextToMatch()
    {
        IsLoading = true;
        
        var isOponent = await _appMatchmakingService.StartSearch("Bullet", SelectedBoardIndex);
        while (IsLoading)
        {
            if (isOponent)
            {
                var settingsView = _serviceProvider.GetRequiredService<MatchViewModel>();
                HostScreen.Router.Navigate.Execute(settingsView);
                
                IsLoading = false;
            }
            
            await Task.Delay(500);
            
            isOponent = await _appMatchmakingService.Search("Bullet", SelectedBoardIndex);
        }
    }
    
    
    // К Станице c настройками
    private IObservable<IRoutableViewModel> NextToSettings()
    {
        var matchView= _serviceProvider.GetRequiredService<MatchViewModel>();
        return HostScreen.Router.Navigate.Execute(matchView);
    }
    
    // К Станице о нас
    private IObservable<IRoutableViewModel> NextToAboutUs()
    {
        var aboutUsView = _serviceProvider.GetRequiredService<AboutUsViewModel>();
        return HostScreen.Router.Navigate.Execute(aboutUsView);
    }
    
    // К Старанице Игра
    private void NextToPlay()
    {
        var playView = _serviceProvider.GetRequiredService<PlayViewModel>();
        HostScreen.Router.Navigate.Execute(playView);
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
    
    // Конвектор доски
    private void ChangeBoardSize(string i)
    {
        SelectedBoardIndex = ConvertToInt(i);
        BimapBoard = i switch
        {
            "mini" => new Bitmap(@"Assets\mini.png"),
            "norm" => new Bitmap(@"Assets\norm.png"),   
            "max" => new Bitmap(@"Assets\max.png"),
            _ => throw new ArgumentOutOfRangeException(nameof(i), i, null)
        };
    }
    
    private int ConvertToInt(string i)
    {
        int ii = i switch
        {
            "mini" => 9,
            "norm" => 16,   
            "max" => 19,
            _ => throw new ArgumentOutOfRangeException(nameof(i), i, null)
        };

        return ii;
    }
}