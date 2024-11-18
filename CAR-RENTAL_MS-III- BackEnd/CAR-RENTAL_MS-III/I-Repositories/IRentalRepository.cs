using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental> GetRentalByIdAsync(int rentalId);
        Task AddRentalAsync(Rental rental);
        Task UpdateRentalAsync(Rental rental);
        Task DeleteRentalAsync(int rentalId);
    }
}
