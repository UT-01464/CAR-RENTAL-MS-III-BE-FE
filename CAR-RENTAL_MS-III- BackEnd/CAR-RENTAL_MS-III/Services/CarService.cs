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
        private readonly string _imageUploadPath = "wwwroot/images/cars";  // Path to store images

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarResponseDTO> AddCarAsync(CarRequestDTO carRequest)
        {
            // Simple validation
            if (string.IsNullOrEmpty(carRequest.RegistrationNumber) ||
                string.IsNullOrEmpty(carRequest.Model) ||
                string.IsNullOrEmpty(carRequest.Brand))
            {
                throw new ArgumentException("Car registration number, model, and brand are required.");
            }

            var carEntity = _mapper.Map<Car>(carRequest);

            // Handling image upload
            if (carRequest.ImageFile != null && carRequest.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(carRequest.ImageFile.FileName);
                var filePath = Path.Combine(_imageUploadPath, fileName);

                // Save the image to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await carRequest.ImageFile.CopyToAsync(stream);
                }

                carEntity.Image = fileName;  // Save the image filename to the database
            }

            var createdCar = await _carRepository.AddAsync(carEntity);
            return _mapper.Map<CarResponseDTO>(createdCar);
        }

        public async Task<IEnumerable<CarResponseDTO>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CarResponseDTO>>(cars);
        }

        public async Task<CarResponseDTO> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }

            return _mapper.Map<CarResponseDTO>(car);
        }

        public async Task<CarResponseDTO> UpdateCarAsync(int id, CarRequestDTO carRequest)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }

            // Update car details
            car.RegistrationNumber = carRequest.RegistrationNumber;
            car.Model = carRequest.Model;
            car.Brand = carRequest.Brand;
            car.Year = carRequest.Year;
            car.CategoryId = carRequest.CategoryId;
            car.AvailabilityStatus = carRequest.AvailabilityStatus;
            car.UnitsAvailable = carRequest.UnitsAvailable;

            // Handle image update
            if (carRequest.ImageFile != null && carRequest.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(carRequest.ImageFile.FileName);
                var filePath = Path.Combine(_imageUploadPath, fileName);

                // Save the image to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await carRequest.ImageFile.CopyToAsync(stream);
                }

                car.Image = fileName;  // Update image in the database
            }

            var updatedCar = await _carRepository.UpdateAsync(car);
            return _mapper.Map<CarResponseDTO>(updatedCar);
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }

            await _carRepository.DeleteAsync(car);
        }





    }



}

