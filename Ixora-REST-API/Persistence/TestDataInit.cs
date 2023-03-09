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
                _dbContext.Database.EnsureCreated();
                //seed the DB with data later!
            }
        }
    }
}
