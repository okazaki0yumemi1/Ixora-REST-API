using Microsoft.EntityFrameworkCore;
using Ixora_REST_API.Models;

namespace Ixora_REST_API.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.Goods> Goods { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<Models.OrderDetails> OrderDetails { get; set; }
        public DbSet<Models.GoodsType> GoodsTypes { get; set; }
    }
}