using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface IAuthRepository
    {
        Task<bool> UserExists(string username);
        Task AddUser(User user);
        Task<User> GetUserByUsername(string username);
    }
}
