using System;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using ReactiveUI;
using ShessBord.Interfaces;

namespace ShessBord.Services;

public class AppLocalizationService : ReactiveObject, IAppLocalizationService
{
    private CultureInfo _currentCulture = new CultureInfo("ru-RU");
    
    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set => this.RaiseAndSetIfChanged(ref _currentCulture, value);
    }
    
    public IObservable<Unit> LanguageChanged { get; }
    
    public AppLocalizationService()
    {
        LanguageChanged = this.WhenAnyValue(x => x.CurrentCulture)
            .Select(_ => Unit.Default)
            .Publish()
            .RefCount();
    }
    
    public string this[string key] 
        => Resources.Strings.ResourceManager.GetString(key, _currentCulture) ?? $"#{key}";
    
    public void SetLanguage(string cultureCode)
    {
        CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentCulture = CurrentCulture;
        Thread.CurrentThread.CurrentUICulture = CurrentCulture;
    }
}