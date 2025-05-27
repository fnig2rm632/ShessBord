using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord;

public partial class MenuAndroidView : ReactiveUserControl<MenuViewModel>
{
    public MenuAndroidView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}