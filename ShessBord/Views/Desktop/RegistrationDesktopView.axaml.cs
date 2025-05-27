using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord.Views.Desktop;

public partial class RegistrationDesktopView : ReactiveUserControl<RegistrationViewModel>
{
    public RegistrationDesktopView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}