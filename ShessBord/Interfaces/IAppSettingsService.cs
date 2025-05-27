namespace ShessBord.Interfaces;

public interface IAppSettingsService
{
    bool IsSidePanelOpen { get; set; }
    string Theme { get; set; }
    
    void Save();
    void Load();
}