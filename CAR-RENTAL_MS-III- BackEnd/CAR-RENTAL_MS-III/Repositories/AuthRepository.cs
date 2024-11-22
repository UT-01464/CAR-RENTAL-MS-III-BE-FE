using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
