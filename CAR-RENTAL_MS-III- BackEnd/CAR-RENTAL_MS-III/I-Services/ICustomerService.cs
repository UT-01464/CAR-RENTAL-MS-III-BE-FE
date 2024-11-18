using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models;
using CAR_RENTAL_MS_III.Models.Login;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface ICustomerService
    {
        Task<LoginRequest> CreateUserAccount(LoginRequest loginRequest);
    }
}
