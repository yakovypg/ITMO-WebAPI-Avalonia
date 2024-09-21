using StarWars.Models.Entities.Containers;
using System;
using System.Collections.Generic;

namespace StarWars.Models.Entities
{
    public abstract record Entity(
        DateTime? CreationDate,
        DateTime? EditedDate,
        string Url)
        : ReactiveModel(), IFieldValuesContainer
    {
        private bool _isFavorite;
        private bool _isReady = true;

        public bool IsFavorite
        {
            get => _isFavorite;
            set => RaiseAndSetIfChanged(ref _isFavorite, value);
        }

        public bool IsReady
        {
            get => _isReady;
            set => RaiseAndSetIfChanged(ref _isReady, value);
        }

        public abstract string IconName { get; }
        public abstract string Presenter { get; }
        public abstract IReadOnlyCollection<FieldValue> FieldValues { get; }

        public override string ToString()
        {
            return Url;
        }
    }
}
