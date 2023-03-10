using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Ixora_REST_API.Persistence
{
    public class GoodsTypeDbOperations : IDbOperations<GoodsType>
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMemoryCache _cache;
        public GoodsTypeDbOperations(DatabaseContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }
        public async Task<bool> CreateAsync(GoodsType obj)
        {
            await _dbContext.GoodsTypes.AddAsync(obj);
            var createdGroups = await _dbContext.SaveChangesAsync();
            return (createdGroups > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {

            var exist = await GetByIDAsync(ID);
            if (exist == null) return false;
            _dbContext.GoodsTypes.Remove(exist);
            var deletedClients = await _dbContext.SaveChangesAsync();
            if (deletedClients > 1)
            {
                string key = $"GoodsTypeID={ID}";
                _cache.Remove(key);
                Console.WriteLine($"{DateTime.Now}: Record with key={key} was removed from cache due to removal from DB.");
                return true;
            }
            else return false;
            //var exist = await GetByIDAsync(ID);
            //if (exist == null) return false;
            //_dbContext.GoodsTypes.Remove(exist);
            //var deletedGroups = await _dbContext.SaveChangesAsync();
            //return (deletedGroups > 0);
        }

        public async Task<List<GoodsType>> GetAllAsync()
        {
            return await _dbContext.GoodsTypes.ToListAsync();
        }

        public async Task<GoodsType> GetByIDAsync(int ID)
        {
            string key = $"GoodsTypeID={ID}";
            _cache.TryGetValue(key, out Models.GoodsType cachedGroup);
            if (cachedGroup == null)
            {
                var group = await _dbContext.GoodsTypes.SingleOrDefaultAsync(x => x.ID == ID);
                _cache.Set(key, group, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));
                Console.WriteLine($"{DateTime.Now}: Group with Id={ID} with key={key} was added to cache.");
                return group;
            }
            else return cachedGroup;
            //return await _dbContext.GoodsTypes.SingleOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<bool> UpdateAsync(GoodsType obj)
        {
            var key = $"GoodsTypeID={obj.ID}";
            _dbContext.GoodsTypes.Update(obj);
            var updatedGroup = await _dbContext.SaveChangesAsync();
            if (updatedGroup > 0)
            {
                _cache.Set(key, updatedGroup);
                Console.WriteLine($"{DateTime.Now}: Record with key={key} was updated in the cache due to updating record in DB.");
                return true;
            }
            else return false;
            //_dbContext.GoodsTypes.Update(obj);
            //var updatedGroups = await _dbContext.SaveChangesAsync();
            //return (updatedGroups > 0);
        }
    }
}
