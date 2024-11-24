using AutoMapper;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Car;

namespace CAR_RENTAL_MS_III.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly string _imageUploadPath = "wwwroot/images/cars";  // Path to store images

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        //public async Task<CarResponseDTO> AddCarAsync(CarRequestDTO carRequest)
        //{
        //    // Simple validation
        //    if (string.IsNullOrEmpty(carRequest.RegistrationNumber) ||
        //        string.IsNullOrEmpty(carRequest.Model) ||
        //        string.IsNullOrEmpty(carRequest.Brand))
        //    {
        //        throw new ArgumentException("Car registration number, model, and brand are required.");
        //    }

        //    var carEntity = _mapper.Map<Car>(carRequest);

        //    // Handling image upload
        //    if (carRequest.ImageFile != null && carRequest.ImageFile.Length > 0)
        //    {
        //        var fileName = Path.GetFileName(carRequest.ImageFile.FileName);
        //        var filePath = Path.Combine(_imageUploadPath, fileName);

        //        // Save the image to disk
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await carRequest.ImageFile.CopyToAsync(stream);
        //        }

        //        carEntity.Image = fileName;  // Save the image filename to the database
        //    }

        //    var createdCar = await _carRepository.AddAsync(carEntity);
        //    return _mapper.Map<CarResponseDTO>(createdCar);
        //}



        public async Task<CarResponseDTO> AddCarAsync(CarRequestDTO carRequest)
        {
            // Validate model existence
            var model = await _carRepository.GetModelByIdAsync(carRequest.ModelId);
            if (model == null)
                throw new ArgumentException("Invalid Model ID.");

            // Check if the registration number already exists
            var existingCar = await _carRepository.GetCarByRegistrationNumberAsync(carRequest.RegistrationNumber);
            if (existingCar != null)
                throw new ArgumentException($"A car with registration number {carRequest.RegistrationNumber} already exists.");

            var carEntity = _mapper.Map<Car>(carRequest);
            carEntity.Model = model;

            // Handle image upload
            if (carRequest.ImageFile != null && carRequest.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(carRequest.ImageFile.FileName);
                var filePath = Path.Combine(_imageUploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await carRequest.ImageFile.CopyToAsync(stream);
                }

                carEntity.Image = fileName;
            }

            var createdCar = await _carRepository.AddAsync(carEntity);
            return _mapper.Map<CarResponseDTO>(createdCar);
        }







        //public async Task<IEnumerable<CarResponseDTO>> GetAllCarsAsync()
        //{
        //    var cars = await _carRepository.GetAllAsync();
        //    return _mapper.Map<IEnumerable<CarResponseDTO>>(cars);
        //}


        public async Task<IEnumerable<CarResponseDTO>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllWithDetailsAsync();
            return cars.Select(car => new CarResponseDTO
            {
                CarId = car.CarId,
                RegistrationNumber = car.RegistrationNumber,
                ModelName = car.Model.Name,
                BrandName = car.Model.Brand.Name,
                CategoryName = car.Category.Name,
                Year = car.Year,
                AvailabilityStatus = car.AvailabilityStatus,
                UnitsAvailable = car.UnitsAvailable,
                ImageUrl = $"/images/cars/{car.Image}"
            });
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

        //public async Task<CarResponseDTO> UpdateCarAsync(int id, CarRequestDTO carRequest)
        //{
        //    var car = await _carRepository.GetByIdAsync(id);
        //    if (car == null)
        //    {
        //        throw new KeyNotFoundException($"Car with ID {id} not found.");
        //    }

        //    // Update car details
        //    car.RegistrationNumber = carRequest.RegistrationNumber;
        //    car.Model = carRequest.Model;
        //    car.Brand = carRequest.Brand;
        //    car.Year = carRequest.Year;
        //    car.CategoryId = carRequest.CategoryId;
        //    car.AvailabilityStatus = carRequest.AvailabilityStatus;
        //    car.UnitsAvailable = carRequest.UnitsAvailable;

        //    // Handle image update
        //    if (carRequest.ImageFile != null && carRequest.ImageFile.Length > 0)
        //    {
        //        var fileName = Path.GetFileName(carRequest.ImageFile.FileName);
        //        var filePath = Path.Combine(_imageUploadPath, fileName);

        //        // Save the image to disk
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await carRequest.ImageFile.CopyToAsync(stream);
        //        }

        //        car.Image = fileName;  // Update image in the database
        //    }

        //    var updatedCar = await _carRepository.UpdateAsync(car);
        //    return _mapper.Map<CarResponseDTO>(updatedCar);
        //}

        public async Task<CarResponseDTO> UpdateCarAsync(int id, CarRequestDTO carRequest)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
                throw new KeyNotFoundException($"Car with ID {id} not found.");

            var model = await _carRepository.GetModelByIdAsync(carRequest.ModelId);
            if (model == null)
                throw new ArgumentException("Invalid Model ID.");

            // Update car details
            car.RegistrationNumber = carRequest.RegistrationNumber;
            car.Model = model;
            car.Year = carRequest.Year;
            car.CategoryId = carRequest.CategoryId;
            car.AvailabilityStatus = carRequest.AvailabilityStatus;
            car.UnitsAvailable = carRequest.UnitsAvailable;

            // Handle image update
            if (carRequest.ImageFile != null && carRequest.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(carRequest.ImageFile.FileName);
                var filePath = Path.Combine(_imageUploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await carRequest.ImageFile.CopyToAsync(stream);
                }

                car.Image = fileName;
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

        public async Task<IEnumerable<Car>> GetCarsByCategoryIdAsync(int categoryId)
        {
            return await _carRepository.GetByCategoryIdAsync(categoryId);
        }





        public async Task<IEnumerable<CarResponseDTO>> GetAllCarsWithDetailsAsync()
        {
            var cars = await _carRepository.GetAllWithDetailsAsync();
            return cars.Select(car => new CarResponseDTO
            {
                CarId = car.CarId,
                RegistrationNumber = car.RegistrationNumber,
                ModelName = car.Model.Name,
                BrandName = car.Model.Brand.Name,
                CategoryName = car.Category.Name,
                Year = car.Year,
                AvailabilityStatus = car.AvailabilityStatus,
                UnitsAvailable = car.UnitsAvailable,
                ImageUrl = $"/images/cars/{car.Image}"
            });
        }








        // Model




        public async Task<ModelDto> GetModelByIdAsync(int modelId)
        {
            if (modelId <= 0)
                throw new ArgumentException("Invalid model ID.", nameof(modelId));  // Validation for modelId

            var model = await _carRepository.GetModelByIdAsync(modelId)
                        ?? throw new KeyNotFoundException($"Model with ID {modelId} not found.");  // Throw an exception if not found

            return new ModelDto(model.ModelId, model.Name, model.BrandId);
        }

        public async Task<IEnumerable<ModelDto>> GetAllModelsAsync()
        {
            var models = await _carRepository.GetAllModelsAsync();  // Fetch all models
            return models.Select(model => new ModelDto(model.ModelId, model.Name, model.BrandId));  // Pass properties to the constructor
        }


        public async Task<ModelDto> CreateModelAsync(ModelDto modelDto)
        {
            if (modelDto == null)
                throw new ArgumentNullException(nameof(modelDto));  // Ensure modelDto is not null

            // Validate ModelDto properties
            if (string.IsNullOrWhiteSpace(modelDto.Name))
                throw new ArgumentException("Model name is required.", nameof(modelDto.Name));  // Ensure name is valid
            if (modelDto.BrandId <= 0)
                throw new ArgumentException("Brand ID must be valid.", nameof(modelDto.BrandId));  // Ensure valid BrandId

            // Convert ModelDto to Model entity before saving
            var model = new Model
            {
                Name = modelDto.Name,
                BrandId = modelDto.BrandId
            };

            var createdModel = await _carRepository.CreateModelAsync(model);  // Add model to the repository

            // Assuming createdModel has ModelId and BrandId, pass them to the ModelDto constructor
            return new ModelDto(createdModel.ModelId, createdModel.Name, createdModel.BrandId);  // Pass ModelId, Name, and BrandId
        }


        public async Task<ModelDto> UpdateModelAsync(int modelId, ModelDto updatedModelDto)
        {
            if (updatedModelDto == null)
                throw new ArgumentNullException(nameof(updatedModelDto));  // Ensure updatedModelDto is not null

            // Validate ModelDto properties
            if (string.IsNullOrWhiteSpace(updatedModelDto.Name))
                throw new ArgumentException("Model name is required.", nameof(updatedModelDto.Name));  // Ensure name is valid
            if (updatedModelDto.BrandId <= 0)
                throw new ArgumentException("Brand ID must be valid.", nameof(updatedModelDto.BrandId));  // Ensure valid BrandId

            var existingModel = await _carRepository.GetModelByIdAsync(modelId);
            if (existingModel == null)
            {
                throw new KeyNotFoundException($"Model with ID {modelId} not found.");
            }

            // Update properties based on ModelDto
            existingModel.Name = updatedModelDto.Name;
            existingModel.BrandId = updatedModelDto.BrandId;

            var updatedModel = await _carRepository.UpdateModelAsync(existingModel);  // Update model in the repository

            // Explicitly pass the required arguments to the ModelDto constructor
            return new ModelDto(updatedModel.ModelId, updatedModel.Name, updatedModel.BrandId);  // Pass ModelId, Name, and BrandId
        }



        public async Task<bool> DeleteModelAsync(int modelId)
        {
            if (modelId <= 0)
                throw new ArgumentException("Invalid model ID.", nameof(modelId));  // Validation for modelId

            return await _carRepository.DeleteModelAsync(modelId);  // Delete the model from the repository
        }









        //----------------------------------------------- Brand


        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _carRepository.GetAllBrandsAsync();  // Get all brands from repository
            return brands.Select(b => new BrandDto
            {
                BrandId = b.BrandId,
                Name = b.Name
            });  // Convert to BrandDto and return
        }

        public async Task<BrandDto> GetBrandByIdAsync(int brandId)
        {
            var brand = await _carRepository.GetBrandByIdAsync(brandId);  // Fetch the brand by ID
            if (brand == null) return null;  // If brand is not found, return null
            return new BrandDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name
            };  // Return as BrandDto
        }

        public async Task<BrandDto> CreateBrandAsync(BrandDto brandDto)
        {
            var brand = new Brand
            {
                Name = brandDto.Name
            };

            var createdBrand = await _carRepository.CreateBrandAsync(brand);  // Create the brand in repository
            return new BrandDto
            {
                BrandId = createdBrand.BrandId,
                Name = createdBrand.Name
            };  // Return the created brand as BrandDto
        }

        public async Task<BrandDto> UpdateBrandAsync(BrandDto brandDto)
        {
            var existingBrand = await _carRepository.GetBrandByIdAsync(brandDto.BrandId);
            if (existingBrand == null)
            {
                return null;  // If brand not found, return null
            }

            existingBrand.Name = brandDto.Name;  // Update the name
            var updatedBrand = await _carRepository.UpdateBrandAsync(existingBrand);  // Save updated brand

            return new BrandDto
            {
                BrandId = updatedBrand.BrandId,
                Name = updatedBrand.Name
            };  // Return updated brand as BrandDto
        }

        public async Task DeleteBrandAsync(int brandId)
        {
            await _carRepository.DeleteBrandAsync(brandId);  // Delete the brand from repository
        }

















    }





}




