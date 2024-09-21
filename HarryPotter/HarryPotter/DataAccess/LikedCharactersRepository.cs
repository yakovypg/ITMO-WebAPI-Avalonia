using HarryPotter.Models;
using HarryPotter.ServerConnection.Services;

namespace HarryPotter.DataAccess;

public class LikedCharactersRepository : HogwartsEntityRepository<Character>
{
    public LikedCharactersRepository()
        : base(ServerConnectionService.LikedCharactersUrl)
    {
    }
}
