using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Persistence
{
    public class OrdersDbOperations : IDbOperations<Order>
    {
        private readonly DatabaseContext _dbContext;
        public async Task<bool> CreateAsync(Order obj)
        {
            await _dbContext.Orders.AddAsync(obj);
            var createdOrders = await _dbContext.SaveChangesAsync();
            return (createdOrders > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            var exist = await GetByIDAsync(ID);
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
            return await _dbContext.Orders.SingleOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<bool> UpdateAsync(Order obj)
        {
            _dbContext.Orders.Update(obj);
            var updatedOrders = await _dbContext.SaveChangesAsync();
            return (updatedOrders > 0);
        }
    }
}
