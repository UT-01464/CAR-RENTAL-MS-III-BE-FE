using AutoMapper;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Car;

namespace CAR_RENTAL_MS_III.Services
{
    public class CarService:ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository , IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarResponseDTO>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            return _mapper.Map<IEnumerable<CarResponseDTO>>(cars);
        }

        public async Task<CarResponseDTO> GetCarByIdAsync(int carId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            return _mapper.Map<CarResponseDTO>(car);
        }

        //public async Task<int> AddCarAsync(CarRequestDTO carDto)
        //{
        //    var car = _mapper.Map<Car>(carDto);
        //    await _carRepository.AddCarAsync(car);
        //    return car.CarId; // Return the newly created car's ID
        //}



        public async Task<int> AddCarAsync(CarRequestDTO carDto)
        {
            var car = _mapper.Map<Car>(carDto);

            // Auto-generate RegistrationNumber if it's not provided
            if (string.IsNullOrEmpty(car.RegistrationNumber))
            {
                car.RegistrationNumber = GenerateRegistrationNumber(); // Your logic to generate a registration number
            }

            await _carRepository.AddCarAsync(car);
            return car.CarId;
        }

        private string GenerateRegistrationNumber()
        {
            // You can implement a logic for auto-generating registration numbers, for example:
            return "CAR" + Guid.NewGuid().ToString().Substring(0, 6); // Example: CAR123ABC
        }








        public async Task UpdateCarAsync(int carId, CarRequestDTO carDto)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            if (car != null)
            {
                _mapper.Map(carDto, car);
                await _carRepository.UpdateCarAsync(car);
            }
        }

        public async Task DeleteCarAsync(int carId) => await _carRepository.DeleteCarAsync(carId);
    }



}

