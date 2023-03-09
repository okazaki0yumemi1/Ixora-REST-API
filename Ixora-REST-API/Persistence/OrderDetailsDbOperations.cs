using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Ixora_REST_API.Persistence
{
    public class OrderDetailsDbOperations : IDbOperations<OrderDetails>
    {
        private readonly DatabaseContext _dbContext;
        public OrderDetailsDbOperations (DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(OrderDetails obj)
        {
            var order = await _dbContext.Orders.FindAsync(obj.OrderId);
            if (order != null) await _dbContext.OrderDetails.AddAsync(obj);
            else return false;
            var created = await _dbContext.SaveChangesAsync();
            return (created > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            var orderDetails = await _dbContext.OrderDetails.Where(x => x.Id == ID).FirstOrDefaultAsync();
            if (orderDetails != null)
            {
                var toDelete = orderDetails;
                _dbContext.Remove(toDelete);
                var changes = await _dbContext.SaveChangesAsync();
                return (changes > 0);
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
            var orderDetails = await _dbContext.OrderDetails.Where(x => x.Id == obj.Id).FirstOrDefaultAsync();
            if (orderDetails != null)
            {
                orderDetails.ItemPrice = obj.ItemPrice;
                orderDetails.Count = obj.Count;
                orderDetails.GoodsId = obj.GoodsId;
                _dbContext.OrderDetails.Update(orderDetails);
                var changes = await _dbContext.SaveChangesAsync();
                return (changes > 0);
            }
            else return false;
        }
    }
}
