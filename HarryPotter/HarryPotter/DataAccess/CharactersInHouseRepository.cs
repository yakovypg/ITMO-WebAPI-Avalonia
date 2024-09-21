using HarryPotter.Models;

namespace HarryPotter.DataAccess;

public class CharactersInHouseRepository : HogwartsEntityRepository<Character>
{
    public CharactersInHouseRepository()
        : base("https://hp-api.onrender.com/api/characters/house/gryffindor")
    {
    }
}
