using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Login;
using Microsoft.AspNetCore.Mvc;


namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("Login-user")]

        public async Task<IActionResult> CreateUserAccount(LoginRequest loginRequest)
        {
            try
            {
                var data = await _customerService.CreateUserAccount(loginRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);  // This will print the inner exception message
                return BadRequest("An error occurred: " + ex.Message);
            }
        }



    }
}
