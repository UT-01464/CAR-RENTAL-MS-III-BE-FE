using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class CarRepository:ICarRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync() => await _dbContext.Cars.Include(c => c.Category).ToListAsync();

        public async Task<Car> GetCarByIdAsync(int carId) => await _dbContext.Cars.Include(c => c.Category).FirstOrDefaultAsync(c => c.CarId == carId);

        public async Task AddCarAsync(Car car)
        {
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            _dbContext.Cars.Update(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int carId)
        {
            var car = await GetCarByIdAsync(carId);
            if (car != null)
            {
                _dbContext.Cars.Remove(car);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
