using HarryPotter.Models;

namespace HarryPotter.DataAccess;

public class CharactersRepository : HogwartsEntityRepository<Character>
{
    public CharactersRepository()
        : base("https://hp-api.onrender.com/api/characters")
    {
    }
}
