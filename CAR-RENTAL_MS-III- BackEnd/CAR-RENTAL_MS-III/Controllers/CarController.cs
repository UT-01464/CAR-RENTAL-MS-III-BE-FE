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

        // POST: api/Car
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




    }


}
