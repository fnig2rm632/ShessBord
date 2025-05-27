using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using ShessBord.Services;
using ShessBord.ViewModels;
using ShessBord.Views;
using ShessBord.Views.Android;

namespace ShessBord;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        _serviceProvider = AppServices.ConfigureServices();
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = mainViewModel };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView { DataContext = mainViewModel };
        }

        base.OnFrameworkInitializationCompleted();
    }

   
}