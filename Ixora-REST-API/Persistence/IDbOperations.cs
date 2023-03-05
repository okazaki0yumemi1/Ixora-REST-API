using Ixora_REST_API.Models;

namespace Ixora_REST_API.Persistence
{
    public interface IDbOperations
    {
        Task<bool> CreateAsync<T>(T obj) where T : Models.Entity;
        Task<bool> DeleteAsync(int ID);
        Task<List<Client>> GetAllAsync();
        Task<Client> GetByIDAsync(int ID);
        Task<bool> UpdateAsync<T>(T obj) where T : Models.Entity;
    }
}