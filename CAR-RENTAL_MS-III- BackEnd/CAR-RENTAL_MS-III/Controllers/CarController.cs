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


        [HttpGet("GetAllCars")]
        public async Task<ActionResult<IEnumerable<CarResponseDTO>>> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("GetCarById/{id}")]
        public async Task<ActionResult<CarResponseDTO>> GetCarById(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        [HttpPost("AddCar")]
        public async Task<ActionResult> AddCar(CarRequestDTO carDto)
        {
            var carId = await _carService.AddCarAsync(carDto);
            var createdCar = await _carService.GetCarByIdAsync(carId);
            return CreatedAtAction(nameof(GetCarById), new { id = createdCar.CarId }, createdCar);
        }

        [HttpPut("UpdateCar/{id}")]
        public async Task<ActionResult> UpdateCar(int id, CarRequestDTO carDto)
        {
            await _carService.UpdateCarAsync(id, carDto);
            return NoContent();
        }

        [HttpDelete("DeleteCar/{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return NoContent();
        }
    }


}
