using System.Collections.Generic;

namespace StarWars.Models.Entities.Containers
{
    public interface IPilotsContainer
    {
        List<string> Pilots { get; }
    }
}
