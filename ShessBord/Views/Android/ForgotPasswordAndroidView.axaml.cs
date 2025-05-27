using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord;

public partial class ForgotPasswordAndroidView : ReactiveUserControl<ForgotPasswordViewModel>
{
    public ForgotPasswordAndroidView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}