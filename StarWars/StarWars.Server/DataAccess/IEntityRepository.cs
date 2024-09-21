namespace StarWars.Server.DataAccess
{
    public interface IEntityRepository
    {
        IAsyncEnumerable<string> FindAllEntitiesAsync();
        Task AddAsync(string entityUrl);
        Task RemoveAsync(string entityUrl);
    }
}
