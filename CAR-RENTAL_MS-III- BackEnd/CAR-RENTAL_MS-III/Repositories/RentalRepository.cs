using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _context.Rentals.Include(r => r.Customer).Include(r => r.Car).ToListAsync();
        }

        public async Task<Rental> GetRentalByIdAsync(int rentalId)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.RentalId == rentalId);
        }

        public async Task AddRentalAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRentalAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRentalAsync(int rentalId)
        {
            var rental = await GetRentalByIdAsync(rentalId);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
        }
    }

}
