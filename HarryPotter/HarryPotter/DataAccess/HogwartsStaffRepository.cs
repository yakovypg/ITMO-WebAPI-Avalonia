using HarryPotter.Models;

namespace HarryPotter.DataAccess;

public class HogwartsStaffRepository : HogwartsEntityRepository<Character>
{
    public HogwartsStaffRepository()
        : base("https://hp-api.onrender.com/api/characters/staff")
    {
    }
}
