using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ShessBord.ViewModels;

namespace ShessBord.Views.Android;

public partial class MainView : ReactiveUserControl<MainWindowViewModel>
{
    public MainView()
    {
        Console.WriteLine(@"Сработало");
        this.InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}