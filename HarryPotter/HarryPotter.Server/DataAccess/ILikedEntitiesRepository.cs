using HarryPotter.ServerConnection.Models;
using System.Collections.Generic;

namespace HarryPotter.Server.DataAccess;

public interface ILikedEntitiesRepository
{
    IAsyncEnumerable<LikedItem> FindAllAsync();
}
