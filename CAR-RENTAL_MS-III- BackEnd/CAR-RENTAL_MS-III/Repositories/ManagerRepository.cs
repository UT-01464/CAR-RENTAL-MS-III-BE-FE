using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class ManagerRepository:IManagerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ManagerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }


        public async Task<IEnumerable<CarCategory>> GetCategoriesAsync()
        {
            return await _dbContext.CarCategories.Include(c => c.Cars).ToListAsync();
        }

        public async Task<CarCategory> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.CarCategories.Include(c => c.Cars).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateCategoryAsync(CarCategory category)
        {
            await _dbContext.CarCategories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(CarCategory category)
        {
            _dbContext.CarCategories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(CarCategory category)
        {
            _dbContext.CarCategories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

    }
}
