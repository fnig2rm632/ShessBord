using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using ShessBord.Interfaces;

namespace ShessBord.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    { 
        public RoutingState Router { get; } = new();
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppAuthService _appAuthService;

        public MainWindowViewModel(
            IServiceProvider serviceProvider, 
            IAppAuthService appAuthService)
        {
            _serviceProvider = serviceProvider;
            _appAuthService = appAuthService;
            _serviceProvider.GetRequiredService<IViewLocator>();
            
            InitializeCommand = ReactiveCommand.CreateFromTask(InitializeAsync);
            
            
            Dispatcher.UIThread.Post(() => 
            {
                InitializeCommand.Execute().Subscribe();
            });
        }
        
        public ReactiveCommand<Unit, Unit> InitializeCommand { get; }

        private async Task InitializeAsync()
        {
            try
            {
                var isAuthenticated = await _appAuthService.TryAutoLoginAsync();
                await NavigateToStartPageAsync(isAuthenticated);
            }
            catch (OperationCanceledException)
            {
                // Игнорируем отмену операции
            }
            catch (Exception ex)
            {
                await NavigateToStartPageAsync(false);
                Console.WriteLine(ex);
            }
        }
        
        

        // Настройка начального окна
        private Task NavigateToStartPageAsync(bool isAuthenticated)
        {
            try
            {
                if (isAuthenticated)
                {
                    var startViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
                    Router.Navigate.Execute(startViewModel).Subscribe();
                }
                else
                {
                    var startViewModel = _serviceProvider.GetRequiredService<AuthenticationViewModel>();
                    Router.Navigate.Execute(startViewModel).Subscribe();
                }
                
            }
            catch (Exception e)
            {
                Dispatcher.UIThread.Post(() => 
                {
                    Console.WriteLine(@"Error auto auth " + e.Message);
                }, DispatcherPriority.Background);
            }

            return Task.CompletedTask;
        }
        
    }
}
