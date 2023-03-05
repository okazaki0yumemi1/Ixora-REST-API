using Ixora_REST_API.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ixora_REST_API.Persistence
{
    public interface IDbOperations<T> where T : Models.Entity
    {
        Task<bool> CreateAsync(T obj);
        Task<bool> DeleteAsync(int ID);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(int ID);
        Task<bool> UpdateAsync(T obj);
    }
}