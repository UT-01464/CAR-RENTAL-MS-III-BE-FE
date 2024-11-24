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
        Task<IEnumerable<Car>> GetCarsByCategoryIdAsync(int categoryId);

        Task<IEnumerable<CarResponseDTO>> GetAllCarsWithDetailsAsync();

        // Methods related to Model
        Task<ModelDto> GetModelByIdAsync(int modelId);
        Task<IEnumerable<ModelDto>> GetAllModelsAsync();
        Task<ModelDto> CreateModelAsync(ModelDto modelDto);
        Task<ModelDto> UpdateModelAsync(int modelId, ModelDto updatedModelDto);
        Task<bool> DeleteModelAsync(int modelId);


        // Brand 

        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();  // Get all brands as BrandDto
        Task<BrandDto> GetBrandByIdAsync(int brandId);    // Get a brand by ID as BrandDto
        Task<BrandDto> CreateBrandAsync(BrandDto brandDto); // Create a new brand using BrandDto
        Task<BrandDto> UpdateBrandAsync(BrandDto brandDto); // Update an existing brand using BrandDto
        Task DeleteBrandAsync(int brandId);                 // Delete a brand by ID

    }
}
