using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models.Customer;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface IAuthService
    {
        Task<string> RegisterUser(CustomerRegisterDto customerDto);
        Task<string> LoginUser(UserLoginDto userDto);
    }
}
