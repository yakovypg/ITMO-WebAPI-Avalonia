using StarWars.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.DataAccess
{
    public interface IEntityRepository
    {
        IAsyncEnumerable<Entity> FindAllEntitiesAsync();
        Task AddAsync(Entity entity);
        Task RemoveAsync(Entity entity);
    }
}
