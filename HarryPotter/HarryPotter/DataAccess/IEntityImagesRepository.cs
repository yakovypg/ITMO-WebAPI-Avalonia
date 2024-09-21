using System.Threading.Tasks;

namespace HarryPotter.DataAccess;

public interface IEntityImagesRepository
{
    Task<byte[]> FindImageAsync(string entityId);
}