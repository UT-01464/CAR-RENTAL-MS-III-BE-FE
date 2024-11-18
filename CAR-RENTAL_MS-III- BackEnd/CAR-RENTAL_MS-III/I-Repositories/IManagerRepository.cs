using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface IManagerRepository
    {

        Task<IEnumerable<CarCategory>> GetCategoriesAsync();
        Task<CarCategory> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CarCategory category);
        Task UpdateCategoryAsync(CarCategory category);
        Task DeleteCategoryAsync(CarCategory category);
    }
}
