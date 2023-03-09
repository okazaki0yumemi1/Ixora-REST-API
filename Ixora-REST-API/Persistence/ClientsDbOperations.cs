using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ixora_REST_API.Persistence
{
    public class ClientsDbOperations : IDbOperations<Client>
    {
        private readonly DatabaseContext _dbContext;
        public ClientsDbOperations(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
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
            return await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == ID);
        }
        public async Task<List<Models.Order>> GetClientOrders(int clientId)
        {
            return await _dbContext.Orders.Where(x => x.ClientId == clientId).ToListAsync();
        }
        public async Task<bool> UpdateAsync(Client newClient)
        {
            _dbContext.Clients.Update(newClient);
            var updatedClients = await _dbContext.SaveChangesAsync();
            return (updatedClients > 0);
        }
        public async Task<bool> DeleteAsync(int ID)
        {
            var exist = await GetByIDAsync(ID);
            if (exist == null) return false;
            _dbContext.Clients.Remove(exist);
            var deletedClients = await _dbContext.SaveChangesAsync();
            return (deletedClients > 1);
        }
    }
}
