using System.Runtime.CompilerServices;

namespace StarWars.Models
{
    public interface IReactiveModel
    {
        void OnPropertyChanged([CallerMemberName] string? property = "");
        bool RaiseAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string? propertyName = null);
    }
}
