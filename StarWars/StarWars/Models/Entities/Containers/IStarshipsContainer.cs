using System.Collections.Generic;

namespace StarWars.Models.Entities.Containers
{
    public interface IStarshipsContainer
    {
        List<string> Starships { get; }
    }
}
