using System;
using System.Reactive;

namespace ShessBord.Interfaces;


public interface IAppLocalizationService
{
    string this[string key] { get; }
    IObservable<Unit> LanguageChanged { get; }
    void SetLanguage(string cultureCode);
}