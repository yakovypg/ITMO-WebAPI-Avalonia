using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HarryPotter.Models;

public abstract class ReactiveModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool RaiseAndSetIfChanged<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, newValue))
            return false;

        field = newValue;
        OnPropertyChanged(propertyName);

        return true;
    }

    public void OnPropertyChanged([CallerMemberName] string? property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
