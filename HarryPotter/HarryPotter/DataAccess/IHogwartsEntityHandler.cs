using HarryPotter.Models;
using System.Threading.Tasks;

namespace HarryPotter.DataAccess
{
    public interface IHogwartsEntityHandler<T>
        where T : HogwartsEntity
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
