using System.Collections.Generic;

namespace StarWars.Models.Entities.Containers
{
    public interface IPlanetsContainer
    {
        List<string> Planets { get; }
    }
}
