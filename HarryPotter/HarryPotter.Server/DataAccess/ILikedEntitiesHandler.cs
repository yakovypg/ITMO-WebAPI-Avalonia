using HarryPotter.ServerConnection.Models;
using System.Threading.Tasks;

namespace HarryPotter.Server.DataAccess;

public interface ILikedEntitiesHandler
{
    Task AddAsync(LikedItem entity);
    Task DeleteAsync(string entityId);
}
