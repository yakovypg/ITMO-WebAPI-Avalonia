using StarWars.Models.Serialization;
using System.Threading.Tasks;

namespace StarWars.DataAccess
{
    public interface IEntityDataRepository<T> where T : EntityBaseData
    {
        Task<T> FindEntityByUrlAsync(string url);
        Task<T> FindEntityByIdAsync(int id);
        Task<EntityPage<T>> FindEntitiesAsync(int page);
    }
}
