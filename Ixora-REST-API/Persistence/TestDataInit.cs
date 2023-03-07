using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Ixora_REST_API.Persistence
{
    public static class TestDataInit
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var _dbContext = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>()))
            {

                _dbContext.Clients.Add(new Models.Client { ClientName = "Meow", PhoneNumber = "001" });
                _dbContext.GoodsTypes.Add(new Models.GoodsType { GroupName = "Wheel"});
                _dbContext.GoodsTypes.Add(new Models.GoodsType { GroupName = "Paint"});
                _dbContext.Goods.Add(new Models.Goods { LeftInStock = 2, Name = "Blue", Price = 100}); ;
                _dbContext.Goods.Add(new Models.Goods { LeftInStock = 0, Name = "Red", Price = 200});
                _dbContext.Goods.Add(new Models.Goods { LeftInStock = 1, Name = "Round", Price = 90});
                _dbContext.Goods.Add(new Models.Goods { LeftInStock = 1, Name = "Square", Price = 90});
                _dbContext.SaveChanges();
            }
        }
    }
}
