using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.Controls;
using ShessBord.Controls.GoBordControl;
using ShessBord.Interfaces;
using ShessBord.Models;
using ShessBord.Services;

namespace ShessBord.ViewModels;

public class MatchViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly IAppMatchService _appMatchService;

    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IAppTokenStorage _tokenStorage;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppSettingsService _appSettingsService;
        private readonly IGameWebSocketService _gameWebSocketService;
        private readonly IAppMatchmakingService _appMatchmakingService;
        private readonly IMatchmakingApiClient _matchmakingApiClient;

        #endregion

    #region Routing
    
        public IScreen HostScreen { get; }
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
        
        public ReactiveCommand<Unit,IRoutableViewModel> NextToAboutUsCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToPlayCommand { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToMenuCommand { get; set; }
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

    #region Match

    
    private bool _isGameActive;

    private GoGameBoardControl.BoardSize _size = GoGameBoardControl.BoardSize.Mini;

    public GoGameBoardControl.BoardSize Size
    {
        get => _size;
        set => this.RaiseAndSetIfChanged(ref _size, value);
    }
    
    
    
    private ObservableCollection<CellBoard> _cells = new ObservableCollection<CellBoard>();
    public ObservableCollection<CellBoard> Cells
    {
        get => _cells;
        set => this.RaiseAndSetIfChanged(ref _cells, value);
    } 
    
    public ReactiveCommand<CellPlace, Unit> MovedCommand { get; }
    

    #endregion

    public MatchViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IAppTokenStorage tokenStorage,
        IServiceProvider serviceProvider,
        IAppSettingsService appSettingsService,
        IGameWebSocketService gameWebSocketService,
        IAppMatchmakingService appMatchmakingService)
    {
        HostScreen = screen;
        _localization = localization;
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        _appSettingsService = appSettingsService;
        _gameWebSocketService = gameWebSocketService;
        _appMatchmakingService = appMatchmakingService;

        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code); 
        });

        // Match
        _appMatchmakingService.Start();
        
        MovedCommand = ReactiveCommand.Create<CellPlace>(move => Moved(move));
        PassCommand = ReactiveCommand.Create(ExecutePass);
        
        
        // Panel
        HidePanelCommand = ReactiveCommand.Create(OpenAndClosePanel);
        IsHidePanel = _appSettingsService.IsSidePanelOpen;
        
        // NextTo
        NextToMenuCommand = ReactiveCommand.CreateFromObservable(NextToMenu);
        NextToSettingsCommand  = ReactiveCommand.CreateFromObservable(NextToSettings);
        
    }
    
        
  
        
        // Команды
        public ReactiveCommand<CellPlace, Unit> MakeMoveCommand { get; }
        public ReactiveCommand<int, Unit> StartNewGameCommand { get; }
        public ReactiveCommand<Unit, Unit> PassCommand { get; }
        
        // Обработка хода
        private void ExecuteMakeMove(CellPlace move)
        {
            if (!_isGameActive) return;
            
        }
        
        // Начало новой игры
        private void ExecuteStartNewGame()
        {
            
            _isGameActive = true;
            InitializeCells();
           
        }
        
        // Пас (пропуск хода)
        private void ExecutePass()
        {
            if (!_isGameActive) return;
            
            Console.WriteLine("Пас");
            
        }
        
        // Инициализация клеток доски
        private void InitializeCells()
        {
            Cells.Clear();

            Cells = new();
        }
        
        // Обновление состояния доски
        private void UpdateBoard()
        {
            
        }
 
    //Сдвинулась пешка
    private void Moved(CellPlace move)
    {
        Console.WriteLine(move.Column + " "+ move.Row);
        
        var Hod = new GameMove
        {
            IdPlayer = Guid.Parse(_tokenStorage.GetTokens().userId),
            X = move.Column,
            Y = move.Row,
        };

        _gameWebSocketService.SendMoveAsync(Hod).ConfigureAwait(false);
    }
    
    //Открытие и закрытие панели
    private void OpenAndClosePanel() => IsHidePanel = !IsHidePanel;
    
    // К Станице c настройками
    private IObservable<IRoutableViewModel> NextToSettings()
    {
        var settingsView = _serviceProvider.GetRequiredService<SettingsViewModel>();
        return HostScreen.Router.Navigate.Execute(settingsView);
    }
    
    // К Станице main в меню
    private IObservable<IRoutableViewModel> NextToMenu()
    {
        var menuView = _serviceProvider.GetRequiredService<MenuViewModel>();
        return HostScreen.Router.Navigate.Execute(menuView);
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