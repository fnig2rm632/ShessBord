using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord;

public partial class ForgotPasswordDesktopView : ReactiveUserControl<ForgotPasswordViewModel>
{
    public ForgotPasswordDesktopView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}