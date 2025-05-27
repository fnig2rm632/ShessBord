using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ShessBord.DTO.Friend;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.ViewModels;

public class FriendsViewModel : ViewModelBase, IRoutableViewModel
{
    #region Dependency Injection
    
        private readonly IAppLocalizationService _localization;
        private readonly IAppTokenStorage _tokenStorage;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppFriendService _appFriendService;
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

        public ICommand ChangeLanguageCommand { get; set; }

        #endregion

    #region Language Change Fields
    
    public string TextPlay => _localization["Play"];
    public string TextFriend => _localization["Friend"];
    public string TextAboutUs => _localization["AboutUs"];
    public string TextSettings => _localization["Settings"];
    public string TextRowUp => _localization["RollUp"];
    public string TextSearchByName => _localization["TextSearchByName"];
    
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

    #region Fuction Friend

    private string _textBoxSearchByName;

    public string TextBoxSearchByName
    {
        get => _textBoxSearchByName;
        set
        {
            this.RaiseAndSetIfChanged(ref _textBoxSearchByName, value); 
            _ = GetFriendsAsync();
        }
    }

    public ReactiveCommand<string, Unit> RemoveFriendCommand { get; }

    public ReactiveCommand<string, Unit> AcceptFriendCommand { get; }
    
    public ReactiveCommand<string, Unit> SendFriendCommand { get; }
    
    

    #endregion

    #region DataClient

    private List<FriendItem> friends;

    public List<FriendItem> Friends
    {
        get => friends;
        set => this.RaiseAndSetIfChanged(ref friends, value);
    }

    #endregion

    public FriendsViewModel(
        IScreen screen,
        IAppLocalizationService localization,
        IAppTokenStorage tokenStorage,
        IServiceProvider serviceProvider,
        IAppFriendService appFriendService,
        IAppSettingsService appSettingsService)
    { 
        HostScreen = screen;
        _localization = localization;
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        _appFriendService = appFriendService;
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
        
        // Укнопки управления friend
        RemoveFriendCommand = ReactiveCommand.CreateFromTask<string>(RemoveFriendAsync);
        AcceptFriendCommand = ReactiveCommand.CreateFromTask<string>(AcceptFriendAsync);
        SendFriendCommand = ReactiveCommand.CreateFromTask<string>(SendFriendAsync);
        
        // ClientData
        _ = GetFriendsAsync();
    }
    
    // Удаления друга
    private async Task RemoveFriendAsync(string friendId)
    {
        await _appFriendService.DeleteFriendRequest(friendId);
        _ = GetFriendsAsync();
    }
    
    // Принять запрос друга
    private async Task AcceptFriendAsync(string friendId)
    {
        await _appFriendService.AcceptFriendRequest(friendId);
        _ = GetFriendsAsync();
    }
    
    // Отправить запрос друга
    private async Task SendFriendAsync(string friendId)
    {
        await _appFriendService.SendFriendRequest(friendId);
        _ = GetFriendsAsync();
    }

    // Метод Определения FiendsList
    private async Task GetFriendsAsync()
    {
        ServiceResponse<List<FriendResponseDto>> tmp =  new ();

        if (!string.IsNullOrEmpty(TextBoxSearchByName))
        {
            tmp = await _appFriendService.FindFriendsList(TextBoxSearchByName);
        }
        else
        {
            tmp = await _appFriendService.GetFriendsList();
        }
        
        var friendItems = new List<FriendItem>();
        
        var mainId = _tokenStorage.GetTokens().userId;
        
        foreach (var t in tmp.Data)
        {
            var friendItem = new FriendItem()
            {
                Response = t,
                RemoveCommand = RemoveFriendCommand,
                AcceptCommand = AcceptFriendCommand,
                SendCommand = SendFriendCommand,
                IsVisibleRemove = false,
                IsVisibleAccept = false,
                IsVisibleSend = false,
            };

            if (t.Status == "Pending")
            {
                friendItem.IsVisibleAccept = true;
            }
            
            if (t.Status == "Pending" || t.Status == "Accepted")
            {
                friendItem.IsVisibleRemove = true;
            }

            if (t.Status != "Pending" && t.Status != "Accepted")
            {
                friendItem.IsVisibleSend = true;
            }
            
            friendItems.Add(friendItem);
        }
        
        Friends = friendItems;
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