using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
    }
}
