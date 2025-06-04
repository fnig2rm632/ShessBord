using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.Controls;
using ShessBord.Controls.GoBordControl;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.ViewModels;

public class MatchViewModel : ViewModelBase, IRoutableViewModel
{

    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IAppTokenStorage _tokenStorage;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppSettingsService _appSettingsService;
        private readonly IGameWebSocketService _gameWebSocketService;
        private readonly IAppMatchmakingService _appMatchmakingService;
        private readonly IAppGameService _appGameService;

        #endregion

    #region Routing
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0,5);
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit,IRoutableViewModel> NextToMenuCommand { get; }
    
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

        public ReactiveCommand<Unit, Unit> PassCommand { get; }
        
        public ReactiveCommand<Unit, Unit> OpenCloseDialogViewCommand { get; }
        public ReactiveCommand<CellPlace, Unit> MovedCommand { get; }
        
        private GoGameBoardControl.BoardSize _size = GoGameBoardControl.BoardSize.Mini;
        public GoGameBoardControl.BoardSize Size
        {
            get => _size;
            set => this.RaiseAndSetIfChanged(ref _size, value);
        }
        
        
        private ObservableCollection<CellBoard> _cells = new();
        public ObservableCollection<CellBoard> Cells
        {
            get => _cells;
            set => this.RaiseAndSetIfChanged(ref _cells, value);
            
        } 
    
        private GameStatus _gameStatus;
        public GameStatus GameStatus
        {
            get => _gameStatus;
            set
            {
                this.RaiseAndSetIfChanged(ref _gameStatus, value);
                
                Cells = ConvertMatrixToSell(value.FieldMatrix);
                
                if (value.WinPlayer == value.Player1Id || value.WinPlayer == value.Player2Id)
                {
                    FinishGame(value.WinPlayer.ToString() == _tokenStorage.GetTokens().userId);
                }
            }
        }

        private void FinishGame(bool isWin)
        {
            IsLoading = true;
            Result = isWin ? "Победа" : "Поражение";
            _gameWebSocketService.DisconnectAsync();
        }
        

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        private string _result = "";

        public string Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref _result, value);
        }
        
        public int IdGame { get; set; }
    
    #endregion

    public MatchViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IAppTokenStorage tokenStorage,
        IServiceProvider serviceProvider,
        IAppSettingsService appSettingsService,
        IGameWebSocketService gameWebSocketService,
        IAppMatchmakingService appMatchmakingService,
        IAppGameService appGameService)
    {
        HostScreen = screen;
        _localization = localization;
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        _appSettingsService = appSettingsService;
        _gameWebSocketService = gameWebSocketService;
        _appMatchmakingService = appMatchmakingService;
        _appGameService = appGameService;

        _localization.LanguageChanged.Subscribe(_ => UpdateLocalizedProperties());
        
        _ = StartGame();
        
        ChangeLanguageCommand = ReactiveCommand.Create<string>(code => 
        {
            _localization.SetLanguage(code); 
        });

        // Match
        MovedCommand = ReactiveCommand.Create<CellPlace>(move => Moved(move));
        PassCommand = ReactiveCommand.Create(ExecutePass);
        OpenCloseDialogViewCommand = ReactiveCommand.Create(Resign);

        _gameWebSocketService.ConnectAsync(Guid.Parse(_tokenStorage.GetTokens().userId));
        
        _gameWebSocketService.GameUpdates
            .ObserveOn(RxApp.MainThreadScheduler) // UI-поток
            .Subscribe(
                status =>
                {
                    GameStatus = status; // триггерит UI
                },
                error =>
                {
                    Console.WriteLine(error);
                }
            );
        
        // Panel
        IsHidePanel = _appSettingsService.IsSidePanelOpen;
        
        // NextTo
        NextToMenuCommand = ReactiveCommand.CreateFromObservable(NextToMenu);
    }
    
    private async Task StartGame()
    {
        var newGame = await _appGameService.PostStartedGameAsync();
        IdGame = newGame.Id;
    }
    
    private void OpenCloseDialogView()
    {
        IsLoading = true;
    }
       
    // Пас (пропуск хода)
    private void ExecutePass()
    {
        var stone = new GameMove
        {
            IdPlayer = Guid.Parse(_tokenStorage.GetTokens().userId),
            IdGame = IdGame,
            Type = GameMove.MoveType.Pass
        };

        _gameWebSocketService.SendMoveAsync(stone).ConfigureAwait(false);
    }

    
    //Сдвинулась пешку
    private void Moved(CellPlace move)
    {
        var stone = new GameMove
        {
            IdPlayer = Guid.Parse(_tokenStorage.GetTokens().userId),
            X = move.Column,
            Y = move.Row,
            IdGame = IdGame,
            Type = GameMove.MoveType.PlaceStone
        };

        _gameWebSocketService.SendMoveAsync(stone).ConfigureAwait(false);
    }
    
    // Сдаться
    private void Resign()
    {
        var stone = new GameMove
        {
            IdPlayer = Guid.Parse(_tokenStorage.GetTokens().userId),
            IdGame = IdGame,
            Type = GameMove.MoveType.Resign
        };

        _gameWebSocketService.SendMoveAsync(stone).ConfigureAwait(false);
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
    
    // Метод конвертиции матрицы в Cells
    private ObservableCollection<CellBoard> ConvertMatrixToSell(List<List<char>> matrix)
    {
        if (matrix.Count <= 0)
        {
            return Cells;
        }
            
        ObservableCollection<CellBoard> cells = Cells;
            
        foreach (var cell in cells)
        {
            var temp = matrix[cell.Column][cell.Row];
            if (temp == 'w' || temp == 'b')
            {
                cell.Status = temp;
            }
            else
            {
                cell.Status = null;
            }
        }

        return new ObservableCollection<CellBoard>(cells);
            
    }
}