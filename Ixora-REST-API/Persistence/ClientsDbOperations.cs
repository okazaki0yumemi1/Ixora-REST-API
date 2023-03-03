using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ixora_REST_API.Persistence
{
    public class ClientsDbOperations
    {
        private readonly DatabaseContext _dbContext;
        public ClientsDbOperations(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateClientAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            var createdClients = await _dbContext.SaveChangesAsync();
            return (createdClients > 0);
        }
        public async Task<List<Client>> GetClientsAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }
        public async Task<Client> GetClientByIDAsync(Guid ID)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == ID);
        }
        public async Task<bool> UpdateClientAsync(Client newClient)
        {
            _dbContext.Clients.Update(newClient);
            var updatedClients = await _dbContext.SaveChangesAsync();
            return (updatedClients > 0);
        }
        public async Task<bool> DeleteClientAsync(Guid ID)
        {
            var exist = await GetClientByIDAsync(ID);
            _dbContext.Clients.Remove(exist);
            var deletedClients = await _dbContext.SaveChangesAsync();
            return (deletedClients > 0);
        }
    }
}
