using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ReactiveUI;
using ShessBord.Clients;
using ShessBord.Interfaces;
using ShessBord.ViewModels;

namespace ShessBord.Services;

public static class AppServices
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        //HttpClient 
        services.AddHttpClient("DefaultClient", client => 
        {
            client.BaseAddress = new Uri("http://localhost:5160/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });
        
        // ApiAuth
        services.AddTransient<IAuthApiClient, AuthApiClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient("DefaultClient");
            return new AuthApiClient(httpClient); 
        });
        
        // ApiUser
        services.AddTransient<IUserApiClient, UserApiClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient("DefaultClient");
            return new UserApiClient(httpClient);
        });
        
        // ApiGame
        services.AddTransient<IGameApiClient, GameApiClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient("DefaultClient");
            return new GameApiClient(httpClient);
        });
        
        // ApiFriend
        services.AddTransient<IFriendApiClient, FriendApiClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient("DefaultClient");
            return new FriendApiClient(httpClient);
        });
        
        // ApiMatchmaking
        services.AddTransient<IMatchmakingApiClient, MatchmakingApiClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient("DefaultClient");
            return new MatchmakingApiClient(httpClient);
        });
        
        // ApiWebSocket
        services.AddTransient<IGameClient>(sp =>
        {
            // Получаем конфигурацию (appsettings.json)
            var configuration = sp.GetRequiredService<IConfiguration>();
    
            // Извлекаем URL сервера и ID игрока
            string serverUrl = configuration["GameServer:Url"] ?? throw new InvalidOperationException("GameServer:Url not configured");
    
            Guid playerId = Guid.Parse(configuration["GameServer:PlayerId"] ?? throw new InvalidOperationException("GameServer:PlayerId not configured"));

            // Создаем и возвращаем клиент
            return new GameClient(serverUrl, playerId);
        });

        // ViewModels
        services.AddSingleton<IScreen>(x => x.GetRequiredService<MainWindowViewModel>());
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<AboutUsViewModel>();
        services.AddTransient<PlayViewModel>();
        services.AddTransient<MenuViewModel>();
        services.AddTransient<HistoryViewModel>();
        services.AddTransient<FriendsViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<AuthenticationViewModel>();
        services.AddTransient<ForgotPasswordViewModel>();
        services.AddTransient<RegistrationViewModel>();
        services.AddTransient<AccountViewModel>();
        services.AddTransient<MatchViewModel>();

        // Service
        services.AddSingleton<IViewLocator, AppViewLocator>();
        services.AddSingleton<IAppLocalizationService, AppLocalizationService>();
        services.AddSingleton<IAppTokenStorage, AppTokenStorage>();
        services.AddSingleton<IAppValidationDataService, AppValidationDataService>();
        services.AddSingleton<IAppAuthService, AppAuthService>();
        services.AddSingleton<IAppUserService, AppUserService>();
        services.AddSingleton<IAppGameService, AppGameService>();
        services.AddSingleton<IAppFriendService, AppFriendService>();
        services.AddSingleton<IAppSettingsService, AppSettingsService>();
        services.AddSingleton<IAppMatchService, AppMatchService>();
        services.AddSingleton<IAppMatchmakingService, AppMatchmakingService>();
        services.AddSingleton<IGameWebSocketService, GameWebSocketService>();
        
        return services.BuildServiceProvider();
    }
}