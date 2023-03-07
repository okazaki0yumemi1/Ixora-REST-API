using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.Serialization;

namespace Ixora_REST_API.Persistence
{
    public class OrderDetailsDbOperations : IDbOperations<OrderDetails>
    {
        private readonly DatabaseContext _dbContext;
        public async Task<bool> CreateAsync(OrderDetails obj)
        {
            var order = await _dbContext.Orders.FindAsync(obj.OrderId);
            if (order != null) order.OrderDetails.Append(obj);
            else return false;
            var createdOrders = await _dbContext.SaveChangesAsync();
            return (createdOrders > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderDetails.Any(y => y.Id == ID) == true);
            if (order != null)
            {
                Order newOrder = new Order
                {
                    //Client = order.Client,
                    //ClientId = order.ClientId,
                    CreationDate = order.CreationDate,
                    //ID = order.ID,
                    //IsComplete = order.IsComplete,
                    //OrderDetails = order.OrderDetails.Where(x => x.Id != ID)
                };
                await _dbContext.Orders.AddAsync(newOrder);
                _dbContext.Orders.Remove(order);
                var changes = await _dbContext.SaveChangesAsync();
                return (changes > 1);
            }
            else return false;
        }

        public async Task<List<OrderDetails>> GetAllAsync()
        {
            throw new NotImplementedException(); //Calling all details in a database does not make any sense.
        }
        public async Task<List<OrderDetails>> GetAllAsync(int orderId)
        {
            return await _dbContext.OrderDetails.Where(f => f.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderDetails> GetByIDAsync(int ID)
        {
            return await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == ID);
        }

        public async Task<bool> UpdateAsync(OrderDetails obj)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderDetails.Any(y => y.Id == obj.Id) == true);
            if (order != null)
            {
                order.UpdateDetails(obj);
                _dbContext.Orders.Update(order);
                var changes = await _dbContext.SaveChangesAsync();
                return (changes > 1);
            }
            else return false;
        }
    }
}
