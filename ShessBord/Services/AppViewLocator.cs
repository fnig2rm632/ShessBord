using System;
using ReactiveUI;
using ShessBord.ViewModels;
using ShessBord.Views.Android;

namespace ShessBord.Services
{
    internal class AppViewLocator : IViewLocator
    {
        public IViewFor ResolveView<T>(T? viewModel, string? cotract = null)
        {
            if (OperatingSystem.IsAndroid())
            {
                return viewModel switch
                {
                    MatchViewModel context => new MatchAndroidView() { DataContext = context},
                    RegistrationViewModel context => new RegistrationAndroidView { DataContext = context},
                    MenuViewModel context => new MenuAndroidView { DataContext = context },
                    HistoryViewModel context => new HistoryAndroidView { DataContext = context },
                    AccountViewModel context => new AccountAndroidView { DataContext = context },
                    SettingsViewModel context => new SettingsAndroidView { DataContext = context },
                    FriendsViewModel context => new FriendsAndroidView { DataContext = context },
                    PlayViewModel context => new PlayAndroidView { DataContext = context },
                    AboutUsViewModel context => new AboutUsAndroidView { DataContext = context },
                    ForgotPasswordViewModel context => new ForgotPasswordAndroidView { DataContext = context },
                    AuthenticationViewModel context => new AuthenticationAndroidView { DataContext = context },
                    _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
                };
            }
            else
            {
                return viewModel switch
                {
                    MatchViewModel context => new MatchDesktopView() { DataContext = context},
                    RegistrationViewModel context => new Views.Desktop.RegistrationDesktopView { DataContext = context},
                    MenuViewModel context => new MenuDesktopView { DataContext = context },
                    HistoryViewModel context => new HistoryDesktopView { DataContext = context },
                    AccountViewModel context => new AccountDesktopView { DataContext = context },
                    SettingsViewModel context => new SettingsDesktopView { DataContext = context },
                    FriendsViewModel context => new FriendsDesktopView { DataContext = context },
                    PlayViewModel context => new PlayDesktopView { DataContext = context },
                    AboutUsViewModel context => new AboutUsDesktopView { DataContext = context },
                    ForgotPasswordViewModel context => new ForgotPasswordDesktopView { DataContext = context },
                    AuthenticationViewModel context => new AuthenticationDesktopView { DataContext = context },
                    _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
                };
            }
        }
    }
}
