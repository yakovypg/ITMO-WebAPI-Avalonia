using System.Collections.Generic;

namespace StarWars.Models.Entities.Containers
{
    public interface ISpeciesContainer
    {
        List<string> Species { get; }
    }
}
