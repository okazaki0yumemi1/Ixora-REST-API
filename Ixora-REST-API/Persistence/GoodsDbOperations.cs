using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Persistence
{
    public class GoodsDbOperations : IDbOperations<Models.Goods>
    {
        private readonly DatabaseContext _dbContext;
        public GoodsDbOperations(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Models.Goods obj)
        {
            await _dbContext.Goods.AddAsync(obj);
            var createdClients = await _dbContext.SaveChangesAsync();
            return (createdClients > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            var exist = await GetByIDAsync(ID);
            _dbContext.Goods.Remove(exist);
            var deletedGoods = await _dbContext.SaveChangesAsync();
            return (deletedGoods > 0);
        }

        public async Task<List<Models.Goods>> GetAllAsync()
        {
            return await _dbContext.Goods.ToListAsync();
        }

        public async Task<Models.Goods> GetByIDAsync(int ID)
        {
            return await _dbContext.Goods.SingleOrDefaultAsync(x => x.Id == ID);
        }

        public async Task<bool> UpdateAsync(Models.Goods obj)
        {
            _dbContext.Goods.Update(obj);
            var updatedGoods = await _dbContext.SaveChangesAsync();
            return (updatedGoods > 0);
        }
    }
}
