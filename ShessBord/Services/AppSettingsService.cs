using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using ShessBord.Interfaces;
using ReactiveUI;
using ShessBord.Models;

namespace ShessBord.Services;

public class AppSettingsService : IAppSettingsService
{
    private const string SettingsFileName = "appsettings.json";
    private readonly string _settingsPath;
    
    public bool IsSidePanelOpen { get; set; } = true; 
    public string Theme { get; set; } = "Light"; 
    
    public AppSettingsService()
    {
        // Определяем путь к файлу настроек в папке данных приложения
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appName = Assembly.GetEntryAssembly()?.GetName().Name ?? "ShessBord";
        var appFolder = Path.Combine(appData, appName);
        
        Directory.CreateDirectory(appFolder);
        _settingsPath = Path.Combine(appFolder, SettingsFileName);
    }
    
    public void Save()
    {
        var settings = new
        {
            IsSidePanelOpen,
            Theme
        };
        
        var json = JsonSerializer.Serialize(settings);
        File.WriteAllText(_settingsPath, json);
    }
    
    public void Load()
    {
        if (!File.Exists(_settingsPath)) return;
        
        var json = File.ReadAllText(_settingsPath);
        var settings = JsonSerializer.Deserialize<SettingsModel>(json);
        
        if (settings != null)
        {
            IsSidePanelOpen = settings.IsSidePanelOpen;
            Theme = settings.Theme;
        }
    }
    
}