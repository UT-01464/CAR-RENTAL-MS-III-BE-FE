using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
           _managerService = managerService;
        }


        // GET: api/CarCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarCategory>>> GetCategories()
        {
            var categories = await _managerService.GetCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/CarCategory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarCategory>> GetCategory(int id)
        {
            var category = await _managerService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/CarCategory
        [HttpPost]
        public async Task<ActionResult<CarCategory>> CreateCategory(CarCategoryDTO categoryDto)
        {
            var newCategory = await _managerService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/CarCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CarCategoryDTO categoryDto)
        {
            try
            {
                await _managerService.UpdateCategoryAsync(id, categoryDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/CarCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _managerService.DeleteCategoryAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

