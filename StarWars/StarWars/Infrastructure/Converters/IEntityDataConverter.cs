using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Infrastructure.Converters
{
    public interface IEntityDataConverter<TIn, TOut>
        where TIn : EntityBaseData
        where TOut : Entity
    {
        TOut Convert(TIn entityData);
        bool TryConvert(TIn entityData, [NotNullWhen(true)] out TOut? entity);
    }
}
