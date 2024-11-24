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

        Task<IEnumerable<Car>> GetByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Car>> GetAllWithDetailsAsync();

        Task<Car> GetCarByRegistrationNumberAsync(string registrationNumber);



        //model

        Task<Model> GetModelByIdAsync(int modelId);
        Task<IEnumerable<Model>> GetAllModelsAsync();
        Task<IEnumerable<Car>> GetAllCarsWithDetailsAsync();
        Task<Model> CreateModelAsync(Model model);
        Task<Model> UpdateModelAsync(Model model);
        Task<bool> DeleteModelAsync(int modelId);




        // Brand 

        Task<IEnumerable<Brand>> GetAllBrandsAsync();  // Get all brands
        Task<Brand> GetBrandByIdAsync(int brandId);    // Get a brand by ID
        Task<Brand> CreateBrandAsync(Brand brand);     // Create a new brand
        Task<Brand> UpdateBrandAsync(Brand brand);     // Update an existing brand
        Task DeleteBrandAsync(int brandId);            // Delete a brand
    }
}
