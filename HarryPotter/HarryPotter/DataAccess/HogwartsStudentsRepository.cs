using HarryPotter.Models;

namespace HarryPotter.DataAccess;

public class HogwartsStudentsRepository : HogwartsEntityRepository<Character>
{
    public HogwartsStudentsRepository()
        : base("https://hp-api.onrender.com/api/characters/students")
    {
    }
}
