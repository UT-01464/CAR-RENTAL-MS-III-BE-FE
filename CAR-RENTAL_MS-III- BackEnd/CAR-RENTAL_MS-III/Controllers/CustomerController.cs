using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Customer;
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

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return Ok(customer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer(CustomerResponseDTO customerResponseDTO)
        {
            await _customerService.RegisterCustomerAsync(customerResponseDTO);
            return Ok(new { Message = "Customer registered successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerResponseDTO customerResponseDTO)
        {
            await _customerService.UpdateCustomerAsync(id, customerResponseDTO);
            return Ok(new { Message = "Customer updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok(new { Message = "Customer deleted successfully." });
        }





    }
}
