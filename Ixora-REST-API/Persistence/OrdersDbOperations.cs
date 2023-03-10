using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Persistence
{
    public class OrdersDbOperations : IDbOperations<Order>
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMemoryCache _cache;
        public OrdersDbOperations(DatabaseContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }
        public async Task<bool> CreateAsync(Order obj)
        {
            await _dbContext.Orders.AddAsync(obj);
            foreach (var detail in obj.OrderDetails)
            {
                var thing = await _dbContext.Goods.SingleOrDefaultAsync(x => x.Id == detail.GoodsId);
                thing.LeftInStock -= detail.Count;
                if (thing.LeftInStock < 0) return false; 
                //В данный момент можно сделать два OrderDetails по одному id, заказав сначала весь остаток, потом ещё больше, уведя в минус наличие. 
                //По идее, это должно валидироваться на frontend'е, но тут на всякий случай я всё же введу проверку.
                _dbContext.Goods.Update(thing);
            }
            var createdOrders = await _dbContext.SaveChangesAsync();
            return (createdOrders > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            var exist = await GetByIDAsync(ID);
            if (exist == null) return false;
            _dbContext.Orders.Remove(exist);
            var deletedOrders = await _dbContext.SaveChangesAsync();
            return (deletedOrders > 0);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetByIDAsync(int ID)
        {
            string key = $"OrderID={ID}";
            _cache.TryGetValue(key, out Models.Order cachedOrder);
            if (cachedOrder == null)
            {
                var order = await _dbContext.Orders.SingleOrDefaultAsync(x => x.ID == ID);
                _cache.Set(key, order, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                Console.WriteLine($"{DateTime.Now}: Order with Id={ID} with key={key} was added to cache.");
                return order;
            }
            else return cachedOrder;
            //return await _dbContext.Orders.SingleOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<bool> UpdateAsync(Order obj)
        {
            var key = $"OrderID={obj.ID}";
            _dbContext.Orders.Update(obj);
            var updatedOrders = await _dbContext.SaveChangesAsync();
            if (updatedOrders > 0)
            {
                _cache.Set(key, updatedOrders);
                Console.WriteLine($"{DateTime.Now}: Record with key={key} was updated in the cache due to updating record in DB.");
                return true;
            }
            else return false;
            //_dbContext.Orders.Update(obj);
            //var updatedOrders = await _dbContext.SaveChangesAsync();
            //return (updatedOrders > 0);
        }
    }
}
