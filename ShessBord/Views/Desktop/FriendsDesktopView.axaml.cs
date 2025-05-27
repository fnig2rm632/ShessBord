using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord;

public partial class FriendsDesktopView : ReactiveUserControl<FriendsViewModel>
{
    public FriendsDesktopView()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}