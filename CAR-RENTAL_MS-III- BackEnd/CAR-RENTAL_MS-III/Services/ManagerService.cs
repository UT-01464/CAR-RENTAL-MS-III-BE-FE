using AutoMapper;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models;

namespace CAR_RENTAL_MS_III.Services
{
    public class ManagerService:IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public ManagerService(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;


        }

        public async Task<IEnumerable<CarCategory>> GetCategoriesAsync()
        {
            return await _managerRepository.GetCategoriesAsync();
        }

        public async Task<CarCategory> GetCategoryByIdAsync(int id)
        {
            return await _managerRepository.GetCategoryByIdAsync(id);
        }

        public async Task<CarCategory> CreateCategoryAsync(CarCategoryDTO categoryDto)
        {
            var newCategory = new CarCategory
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            await _managerRepository.CreateCategoryAsync(newCategory);
            return newCategory;
        }

        public async Task UpdateCategoryAsync(int id, CarCategoryDTO categoryDto)
        {
            var existingCategory = await _managerRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            existingCategory.Name = categoryDto.Name;
            existingCategory.Description = categoryDto.Description;

            await _managerRepository.UpdateCategoryAsync(existingCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _managerRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            await _managerRepository.DeleteCategoryAsync(category);
        }
    }
}
