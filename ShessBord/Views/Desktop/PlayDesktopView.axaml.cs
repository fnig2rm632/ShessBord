using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord;

public partial class PlayDesktopView : ReactiveUserControl<PlayViewModel>
{
    public PlayDesktopView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}