using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface IManagerService
    {

        Task<IEnumerable<CarCategory>> GetCategoriesAsync();
        Task<CarCategory> GetCategoryByIdAsync(int id);
        Task<CarCategory> CreateCategoryAsync(CarCategoryDTO categoryDto);
        Task UpdateCategoryAsync(int id, CarCategoryDTO categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
