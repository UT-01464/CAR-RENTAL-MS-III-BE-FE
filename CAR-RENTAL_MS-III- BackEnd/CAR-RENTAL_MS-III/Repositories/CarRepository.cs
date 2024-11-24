using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<Car> AddAsync(Car car)
        {
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();
            return car;
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _dbContext.Cars.FindAsync(id);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _dbContext.Cars.ToListAsync();
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            _dbContext.Cars.Update(car);
            await _dbContext.SaveChangesAsync();
            return car;
        }

        public async Task DeleteAsync(Car car)
        {
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<Car>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Cars
                .Include(c => c.Category)
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
        }



        //--------------------------------------------------MODEL---------------------------------



        public async Task<IEnumerable<Car>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Cars
                .Include(car => car.Model)
                .ThenInclude(model => model.Brand)
                .Include(car => car.Category)
                .ToListAsync();
        }






        public async Task<Model> GetModelByIdAsync(int modelId)
        {
            // Use a more explicit check for null (modelId <= 0) if needed.
            if (modelId <= 0) throw new ArgumentException("Invalid model ID.");

            return await _dbContext.Models
                .Include(m => m.Brand)  // Include the related Brand entity
                .FirstOrDefaultAsync(m => m.ModelId == modelId);  // Fetch the model by ID
        }

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
            return await _dbContext.Models
                .Include(m => m.Brand)  // Ensure we also get the related Brand entity
                .ToListAsync();  // Return all models in the database
        }

        public async Task<IEnumerable<Car>> GetAllCarsWithDetailsAsync()
        {
            return await _dbContext.Cars
                .Include(c => c.Model)  // Include the Model related to the Car
                .ThenInclude(m => m.Brand)  // Then include the Brand related to the Model
                .Include(c => c.Category)  // Include the Category related to the Car
                .ToListAsync();  // Return all cars with related details
        }

        public async Task<Model> CreateModelAsync(Model model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));  // Ensure model is not null
            if (model.BrandId <= 0) throw new ArgumentException("Brand ID must be valid.", nameof(model.BrandId));

            _dbContext.Models.Add(model);  // Add the model to the DbContext
            await _dbContext.SaveChangesAsync();  // Save changes asynchronously

            return model;  // Return the created model
        }

        public async Task<Model> UpdateModelAsync(Model model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));  // Ensure model is not null
            if (model.BrandId <= 0) throw new ArgumentException("Brand ID must be valid.", nameof(model.BrandId));

            // Check if the model exists before updating
            var existingModel = await _dbContext.Models.FindAsync(model.ModelId);
            if (existingModel == null) throw new KeyNotFoundException($"Model with ID {model.ModelId} not found.");

            // Update the model in the DbContext
            _dbContext.Models.Update(model);
            await _dbContext.SaveChangesAsync();  // Save changes asynchronously

            return model;  // Return the updated model
        }

        public async Task<bool> DeleteModelAsync(int modelId)
        {
            if (modelId <= 0) throw new ArgumentException("Invalid model ID.");  // Ensure modelId is valid

            var model = await _dbContext.Models.FindAsync(modelId);
            if (model == null) return false;  // Return false if the model doesn't exist

            _dbContext.Models.Remove(model);  // Remove the model from DbContext
            await _dbContext.SaveChangesAsync();  // Save changes asynchronously

            return true;  // Return true if the model was successfully deleted
        }





        //--------------------------------------------------------------- brand

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _dbContext.Brands.Include(b => b.Models).ToListAsync();  // Fetch all brands with their models
        }

        public async Task<Brand> GetBrandByIdAsync(int brandId)
        {
            return await _dbContext.Brands.Include(b => b.Models)
                                         .FirstOrDefaultAsync(b => b.BrandId == brandId);  // Fetch brand by ID
        }

        public async Task<Brand> CreateBrandAsync(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();
            return brand;  // Return the created brand
        }

        public async Task<Brand> UpdateBrandAsync(Brand brand)
        {
            _dbContext.Brands.Update(brand);
            await _dbContext.SaveChangesAsync();
            return brand;  // Return the updated brand
        }

        public async Task DeleteBrandAsync(int brandId)
        {
            var brand = await _dbContext.Brands.FindAsync(brandId);
            if (brand != null)
            {
                _dbContext.Brands.Remove(brand);
                await _dbContext.SaveChangesAsync();
            }
        }



        public async Task<Car> GetCarByRegistrationNumberAsync(string registrationNumber)
        {
            return await _dbContext.Cars
                .FirstOrDefaultAsync(c => c.RegistrationNumber == registrationNumber);
        }









    }
}
