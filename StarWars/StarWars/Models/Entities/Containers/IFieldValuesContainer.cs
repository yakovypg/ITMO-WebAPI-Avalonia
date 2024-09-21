using System.Collections.Generic;

namespace StarWars.Models.Entities.Containers
{
    public interface IFieldValuesContainer
    {
        IReadOnlyCollection<FieldValue> FieldValues { get; }
    }
}
