using Ixora_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ixora_REST_API.Persistence
{
    public class GoodsTypeDbOperations : IDbOperations<GoodsType>
    {
        private readonly DatabaseContext _dbContext;
        public GoodsTypeDbOperations(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(GoodsType obj)
        {
            await _dbContext.GoodsTypes.AddAsync(obj);
            var createdGroups = await _dbContext.SaveChangesAsync();
            return (createdGroups > 0);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            var exist = await GetByIDAsync(ID);
            _dbContext.GoodsTypes.Remove(exist);
            var deletedGroups = await _dbContext.SaveChangesAsync();
            return (deletedGroups > 0);
        }

        public async Task<List<GoodsType>> GetAllAsync()
        {
            return await _dbContext.GoodsTypes.ToListAsync();
        }

        public async Task<GoodsType> GetByIDAsync(int ID)
        {
            return await _dbContext.GoodsTypes.SingleOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<bool> UpdateAsync(GoodsType obj)
        {
            _dbContext.GoodsTypes.Update(obj);
            var updatedGroups = await _dbContext.SaveChangesAsync();
            return (updatedGroups > 0);
        }
    }
}
