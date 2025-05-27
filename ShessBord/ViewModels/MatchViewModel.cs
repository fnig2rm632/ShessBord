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
using ShessBord.Services;

namespace ShessBord.ViewModels;

public class MatchViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly IAppMatchService _appMatchService;

    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppSettingsService _appSettingsService;
        private readonly IDisposable _stateSubscription;

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
        IServiceProvider serviceProvider,
        IAppSettingsService appSettingsService,
        IAppMatchService appMatchService)
    {
        _appMatchService = appMatchService;
        HostScreen = screen;
        _localization = localization;
        _serviceProvider = serviceProvider;
        _appSettingsService = appSettingsService;

        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code); 
        });

        // Match
        appMatchService.BoardUpdated.Subscribe(_ => UpdateBoard());
        MovedCommand = ReactiveCommand.Create<CellPlace>(move => Moved(move));
        ExecuteStartNewGame(19);
        
        
        // Panel
        HidePanelCommand = ReactiveCommand.Create(OpenAndClosePanel);
        IsHidePanel = _appSettingsService.IsSidePanelOpen;
        
        // NextTo
        NextToAboutUsCommand = ReactiveCommand.CreateFromObservable(NextToAboutUs);
        NextToPlayCommand = ReactiveCommand.CreateFromObservable(NextToPlay);
        NextToMenuCommand = ReactiveCommand.CreateFromObservable(NextToMenu);
        NextToFriendsCommand= ReactiveCommand.CreateFromObservable(NextToFriends);
        NextToSettingsCommand  = ReactiveCommand.CreateFromObservable(NextToSettings);
        
    }
    
    // Текущий игрок
        public GoGameBoardControl.SelectedSideType CurrentPlayer => 
            _appMatchService.CurrentPlayer == StoneColor.Black ? 
                GoGameBoardControl.SelectedSideType.Black : 
                GoGameBoardControl.SelectedSideType.White;
        
        // Статус игры
        public string GameStatus => _isGameActive ? 
            $"Ход: {CurrentPlayer}" : 
            "Игра не начата";
        
        // Команды
        public ReactiveCommand<CellPlace, Unit> MakeMoveCommand { get; }
        public ReactiveCommand<int, Unit> StartNewGameCommand { get; }
        public ReactiveCommand<Unit, Unit> PassCommand { get; }
        
        // Обработка хода
        private void ExecuteMakeMove(CellPlace move)
        {
            if (!_isGameActive) return;
            
            if (_appMatchService.MakeMove(move.Column, move.Row))
            {
                UpdateBoard();
            }
        }
        
        // Начало новой игры
        private void ExecuteStartNewGame(int boardSize)
        {
            Size = boardSize switch
            {
                9 => GoGameBoardControl.BoardSize.Mini,
                13 => GoGameBoardControl.BoardSize.Norm,
                19 => GoGameBoardControl.BoardSize.Max,
                _ => GoGameBoardControl.BoardSize.Mini,
            };
            
            _appMatchService.StartNewGame(boardSize);
            _isGameActive = true;
            InitializeCells(boardSize);
            this.RaisePropertyChanged(nameof(GameStatus));
        }
        
        // Пас (пропуск хода)
        private void ExecutePass()
        {
            if (!_isGameActive) return;
            
            _appMatchService.MakeMove(-1, -1); // Специальные координаты для паса
            this.RaisePropertyChanged(nameof(CurrentPlayer));
            this.RaisePropertyChanged(nameof(GameStatus));
        }
        
        // Инициализация клеток доски
        private void InitializeCells(int boardSize)
        {
            Cells.Clear();

            Cells = new();
        }
        
        // Обновление состояния доски
        private void UpdateBoard()
        {
            // Обновляем состояние каждой клетки
            for (int i = 0; i < Cells.Count; i++)
            {
                var cell = Cells[i];
                var stone = _appMatchService.CurrentBoard[cell.Column, cell.Row];
                
                cell.Status = stone?.Color == StoneColor.Black ? 'b' : 
                             stone?.Color == StoneColor.White ? 'w' : 
                             null;
                
                cell.SideType = CurrentPlayer;
            }
            
            // Обновляем свойства
            this.RaisePropertyChanged(nameof(CurrentPlayer));
            this.RaisePropertyChanged(nameof(GameStatus));
            
            // Проверка окончания игры
            if (_appMatchService.GameResult != null)
            {
                _isGameActive = false;
                // Можно показать результат игры
                Console.WriteLine($"Игра окончена! Результат: {_appMatchService.GameResult}");
            }
            
            NextToMenuCommand = ReactiveCommand.CreateFromObservable(NextToMenu);
        }
 
    //Сдвинулась пешка
    private void Moved(CellPlace move)
    {
        Console.WriteLine(move.Column + " "+ move.Row);
        ExecuteMakeMove(move);
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