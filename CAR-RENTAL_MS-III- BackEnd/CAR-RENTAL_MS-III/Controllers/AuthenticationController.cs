using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterDto customerDto)
        {
            var result = await _authService.RegisterUser(customerDto);

            if (result == "Username already exists.")
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var token = await _authService.LoginUser(userDto);

            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }







    }
}
