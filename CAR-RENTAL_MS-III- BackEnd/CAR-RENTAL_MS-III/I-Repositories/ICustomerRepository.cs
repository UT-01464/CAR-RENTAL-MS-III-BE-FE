using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateUserAccount(Customer customer);
    }
}
