﻿using StarWars.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StarWars.Models
{
    public abstract record ReactiveModel : IReactiveModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public bool RaiseAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(backingField, newValue))
                return false;

            backingField = newValue;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
