using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models;
using CAR_RENTAL_MS_III.Models.Customer;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task RegisterCustomerAsync(CustomerResponseDTO customerResponseDTO);
        Task UpdateCustomerAsync(int id, CustomerResponseDTO customerResponseDTO);
        Task DeleteCustomerAsync(int id);


    }
}
