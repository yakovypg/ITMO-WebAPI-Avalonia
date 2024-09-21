using HarryPotter.Models;
using System.Collections.Generic;

namespace HarryPotter.DataAccess;

public interface IHogwartsEntityRepository<T>
    where T : HogwartsEntity
{
    IAsyncEnumerable<T> FindAllAsync();
}
