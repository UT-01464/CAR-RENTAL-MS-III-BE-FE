using CAR_RENTAL_MS_III.Models.Rentals;
using CAR_RENTAL_MS_III.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {


        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentals()
        {
            return Ok(await _rentalService.GetAllRentalsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalById(int id)
        {
            try
            {
                return Ok(await _rentalService.GetRentalByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental([FromBody] RentalRequestDto rentalRequest)
        {
            try
            {
                var rentalId = await _rentalService.CreateRentalAsync(rentalRequest);
                return CreatedAtAction(nameof(GetRentalById), new { id = rentalId }, rentalId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> CompleteRental(int id, [FromBody] RentalReturnDto rentalReturn)
        {
            try
            {
                await _rentalService.CompleteRentalAsync(id, rentalReturn);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
