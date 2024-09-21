using HarryPotter.Models;

namespace HarryPotter.DataAccess;

public class SpellsRepository : HogwartsEntityRepository<Spell>
{
    public SpellsRepository()
        : base("https://hp-api.onrender.com/api/spells")
    {
    }
}
