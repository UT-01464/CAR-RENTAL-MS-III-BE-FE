using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models.Car;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface ICarService
    {

        Task<IEnumerable<CarResponseDTO>> GetAllCarsAsync();
        Task<CarResponseDTO> GetCarByIdAsync(int carId);
        Task<int> AddCarAsync(CarRequestDTO carDto);
        Task UpdateCarAsync(int carId, CarRequestDTO carDto);
        Task DeleteCarAsync(int carId);

    }
}
