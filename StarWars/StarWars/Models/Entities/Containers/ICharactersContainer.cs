using System.Collections.Generic;

namespace StarWars.Models.Entities.Containers
{
    public interface ICharactersContainer
    {
        List<string> Characters { get; }
    }
}
