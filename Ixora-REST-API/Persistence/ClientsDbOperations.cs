using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Persistence
{
    public class ClientsDbOperations : IDbOperations<Client>
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMemoryCache _cache;

        public ClientsDbOperations(DatabaseContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }
        public async Task<bool> CreateAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            var createdClients = await _dbContext.SaveChangesAsync();
            return (createdClients > 1);
        }
        public async Task<List<Client>> GetAllAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }
        public async Task<Client> GetByIDAsync(int ID)
        {
            string key = $"ClientID={ID}";
            _cache.TryGetValue(key, out Models.Client? cachedClient);
            if (cachedClient == null)
            {
                var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == ID);
                _cache.Set(key, client, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(7)));
                Console.WriteLine($"{DateTime.Now}: Client with Id={ID} was added to cache with key={key} was added to cache.");
                return client;
            }
            else return cachedClient;
        }
        public async Task<List<Models.Order>> GetClientOrders(int clientId)
        {
            _cache.TryGetValue(clientId, out List<Models.Order>? cachedOrders);
            if (cachedOrders == null)
            {
                var orders = await _dbContext.Orders.Where(x => x.ClientId == clientId).ToListAsync();
                string key = clientId.ToString() + ".orders.amount." + orders.Count.ToString();
                _cache.Set(key, orders, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                Console.WriteLine($"{DateTime.Now}: Client orders with Id={clientId} was added to cache with key={key} was added to cache.");
                return orders;
            }
            else return cachedOrders;
            //return await _dbContext.Orders.Where(x => x.ClientId == clientId).ToListAsync();
        }
        public async Task<bool> UpdateAsync(Client newClient)
        {
            var key = $"ClientID={newClient.Id}";
            _dbContext.Clients.Update(newClient);
            var updatedClients = await _dbContext.SaveChangesAsync();
            if (updatedClients > 0)
            {
                _cache.Set(key, updatedClients);
                Console.WriteLine($"{DateTime.Now}: Record with key={key} was updated in the cache due to updating record in DB.");
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteAsync(int ID)
        {
            var exist = await GetByIDAsync(ID);
            if (exist == null) return false;
            _dbContext.Clients.Remove(exist);
            var deletedClients = await _dbContext.SaveChangesAsync();
            if (deletedClients > 1)
            {
                string key = $"ClientID={ID}";
                _cache.Remove(key);
                Console.WriteLine($"{DateTime.Now}: Record with key={key} was removed from cache due to removal from DB.");
                return true;
            }
            else return false;
        }
    }
}
