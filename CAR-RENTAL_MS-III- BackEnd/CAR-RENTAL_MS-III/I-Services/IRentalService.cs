using CAR_RENTAL_MS_III.Models.Rentals;

namespace CAR_RENTAL_MS_III.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalResponseDto>> GetAllRentalsAsync();
        Task<RentalResponseDto> GetRentalByIdAsync(int rentalId);
        Task<int> CreateRentalAsync(RentalRequestDto rentalRequest);
        Task CompleteRentalAsync(int rentalId, RentalReturnDto rentalReturn);
    }
}
