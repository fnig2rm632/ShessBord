using System.Linq;
using ReactiveUI;

namespace ShessBord.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    private void UpdateLocalizedProperties()
    {
        var properties = GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(string));
    
        foreach (var prop in properties)
        {
            this.RaisePropertyChanged(prop.Name);
        }
    }
}
