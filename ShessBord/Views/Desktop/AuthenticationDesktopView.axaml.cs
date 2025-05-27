using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord;

public partial class AuthenticationDesktopView : ReactiveUserControl<AuthenticationViewModel>
{
    public AuthenticationDesktopView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}