using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface ICarRepository
    {
        Task<Car> AddAsync(Car car);
        Task<Car> GetByIdAsync(int id);
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> UpdateAsync(Car car);
        Task DeleteAsync(Car car);
    }
}
