using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Car;
using CAR_RENTAL_MS_III.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/Car
        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        // GET: api/Car/5
        [HttpGet("GetCarById{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                return Ok(car);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Car with ID {id} not found.");
            }
        }

        //POST: api/Car
       [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromForm] CarRequestDTO carRequest)
        {
            try
            {
                var createdCar = await _carService.AddCarAsync(carRequest);
                return CreatedAtAction(nameof(GetCarById), new { id = createdCar.CarId }, createdCar);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

      


        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No image file provided.");
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName); // Adjust the path as needed

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { imagePath = filePath }); // Return the image path to the frontend
        }






        // PUT: api/Car/5
        [HttpPut("UpdateCar{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromForm] CarRequestDTO carRequest)
        {
            try
            {
                var updatedCar = await _carService.UpdateCarAsync(id, carRequest);
                return Ok(updatedCar);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Car with ID {id} not found.");
            }
        }

        // DELETE: api/Car/5
        [HttpDelete("DeleteCar{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carService.DeleteCarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Car with ID {id} not found.");
            }
        }


        //get by Category

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetCarsByCategory(int categoryId)
        {
            try
            {
                var cars = await _carService.GetCarsByCategoryIdAsync(categoryId);
                if (cars == null || !cars.Any())
                {
                    return NotFound("No cars found for the specified category.");
                }
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET: api/Model/Cars
        [HttpGet("GetAllCarsWithDetails")]
        public async Task<IActionResult> GetAllCarsWithDetails()
        {
            var cars = await _carService.GetAllCarsWithDetailsAsync();
            return Ok(cars);
        }





        //----------------------------------------------------------- Model--------------------------------

        // GET: api/Model/{id}
        [HttpGet("GetModelById{id}")]
        public async Task<IActionResult> GetModelById(int id)
        {
            var modelDto = await _carService.GetModelByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound(new { Message = $"Model with ID {id} not found." });
            }

            return Ok(modelDto);
        }

        // GET: api/Model
        [HttpGet("GetAllModels")]
        public async Task<IActionResult> GetAllModels()
        {
            var models = await _carService.GetAllModelsAsync();
            return Ok(models);
        }

        // POST: api/Model
        [HttpPost("CreateModel")]
        public async Task<IActionResult> CreateModel([FromBody] ModelDto modelDto)
        {
            if (modelDto == null)
                return BadRequest(new { Message = "Model data is required." });

            var createdModelDto = await _carService.CreateModelAsync(modelDto);
            return CreatedAtAction(nameof(GetModelById), new { id = createdModelDto.ModelId }, createdModelDto);
        }

        // PUT: api/Model/{id}
        [HttpPut("UpdateModel{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] ModelDto updatedModelDto)
        {
            if (updatedModelDto == null)
                return BadRequest(new { Message = "Updated model data is required." });

            try
            {
                var updatedModelDtoResponse = await _carService.UpdateModelAsync(id, updatedModelDto);
                return Ok(updatedModelDtoResponse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/Model/{id}
        [HttpDelete("DeleteModel{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var success = await _carService.DeleteModelAsync(id);
            if (!success)
                return NotFound(new { Message = $"Model with ID {id} not found." });

            return NoContent();
        }







        //-----------------------------------------------------Brand

        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await _carService.GetAllBrandsAsync();  // Get all brands as BrandDto
            return Ok(brands);
        }

        // GET: api/brand/5
        [HttpGet("GetBrand/{id}")]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var brand = await _carService.GetBrandByIdAsync(id);  // Get brand by ID as BrandDto
            if (brand == null)
            {
                return NotFound();  // If brand not found, return NotFound
            }
            return Ok(brand);  // Return the brand as BrandDto
        }

        // POST: api/brand
        [HttpPost("CreateBrand")]
        public async Task<ActionResult<BrandDto>> CreateBrand(BrandDto brandDto)
        {
            if (brandDto == null)
            {
                return BadRequest();  // If the request body is invalid, return BadRequest
            }

            var createdBrand = await _carService.CreateBrandAsync(brandDto);  // Create the brand
            return CreatedAtAction(nameof(GetBrand), new { id = createdBrand.BrandId }, createdBrand);  // Return the created brand
        }

        // PUT: api/brand/5
        [HttpPut("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(int id, BrandDto brandDto)
        {
            if (id != brandDto.BrandId)
            {
                return BadRequest();  // If IDs don't match, return BadRequest
            }

            var updatedBrand = await _carService.UpdateBrandAsync(brandDto);  // Update the brand
            if (updatedBrand == null)
            {
                return NotFound();  // If brand was not found, return NotFound
            }

            return NoContent();  // If update was successful, return NoContent
        }

        // DELETE: api/brand/5
        [HttpDelete("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _carService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();  // If brand not found, return NotFound
            }

            await _carService.DeleteBrandAsync(id);  // Delete the brand
            return NoContent();  // Return NoContent after successful deletion
        }




    }


}
