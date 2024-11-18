using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models.Car;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface ICarService
    {

        Task<CarResponseDTO> AddCarAsync(CarRequestDTO carRequest);
        Task<IEnumerable<CarResponseDTO>> GetAllCarsAsync();
        Task<CarResponseDTO> GetCarByIdAsync(int id);
        Task<CarResponseDTO> UpdateCarAsync(int id, CarRequestDTO carRequest);
        Task DeleteCarAsync(int id);



    }
}
