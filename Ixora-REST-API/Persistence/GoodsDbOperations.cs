using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Persistence
{
    public class GoodsDbOperations : IDbOperations<Models.Goods>
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMemoryCache _cache;
        public GoodsDbOperations(DatabaseContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }
        public async Task<bool> CreateAsync(Models.Goods obj)
        {
            await _dbContext.Goods.AddAsync(obj);
            var createdClients = await _dbContext.SaveChangesAsync();
            return (createdClients > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            string key = $"GoodsID={ID}";
            var exist = await GetByIDAsync(ID);
            if (exist == null) return false;
            _dbContext.Goods.Remove(exist);
            var deletedGoods = await _dbContext.SaveChangesAsync();
            if (deletedGoods > 0)
            {
                _cache.Remove(key);
                Console.WriteLine($"{DateTime.Now}: Record with key={key} was removed from cache due to removal from DB.");
                return true;
            }
            else return false;
        }

        public async Task<List<Models.Goods>> GetAllAsync()
        {
            return await _dbContext.Goods.ToListAsync();
        }

        public async Task<Models.Goods> GetByIDAsync(int ID)
        {
            string key = $"GoodsID={ID}";
            _cache.TryGetValue(key, out Models.Goods cachedThing);
            if (cachedThing == null)
            {
                var thing = await _dbContext.Goods.SingleOrDefaultAsync(x => x.Id == ID);
                _cache.Set(key, thing, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                Console.WriteLine($"{DateTime.Now}: Part with Id={ID} with key={key} was added to cache.");
                return thing;
            }
            else return cachedThing;
            //var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == ID);
            //string key = $"GoodsID={ID}";
            //_cache.Set(key, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(7)));
            //return await _dbContext.Goods.SingleOrDefaultAsync(x => x.Id == ID);
        }

        public async Task<bool> UpdateAsync(Models.Goods obj)
        {
            var key = $"GoodsID={obj.Id}";
            _dbContext.Goods.Update(obj);
            var updatedGoods = await _dbContext.SaveChangesAsync();
            if (updatedGoods > 0)
            {
                _cache.Set(key, updatedGoods);
                Console.WriteLine($"{DateTime.Now}: Record with key={key} was updated in the cache due to updating record in DB.");
                return true;
            }
            else return false;
        }
    }
}
