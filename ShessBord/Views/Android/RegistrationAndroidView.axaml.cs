using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord.Views.Android;

public partial class RegistrationAndroidView : ReactiveUserControl<RegistrationViewModel>
{
    public RegistrationAndroidView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}