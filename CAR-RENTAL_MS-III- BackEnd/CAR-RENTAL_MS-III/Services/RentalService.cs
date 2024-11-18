using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.Models.Rentals;

namespace CAR_RENTAL_MS_III.Services
{
    public class RentalService: IRentalService
    {

        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;

        public RentalService(IRentalRepository rentalRepository, ICarRepository carRepository)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<RentalResponseDto>> GetAllRentalsAsync()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            return rentals.Select(r => new RentalResponseDto
            {
                RentalId = r.RentalId,
                CustomerId = r.CustomerId,
                CarId = r.CarId,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                Status = r.Status,
                OverdueAmount = r.OverdueAmount,
                DailyRate = r.DailyRate
            });
        }

        public async Task<RentalResponseDto> GetRentalByIdAsync(int rentalId)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) throw new Exception($"Rental with ID {rentalId} not found.");

            return new RentalResponseDto
            {
                RentalId = rental.RentalId,
                CustomerId = rental.CustomerId,
                CarId = rental.CarId,
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                Status = rental.Status,
                OverdueAmount = rental.OverdueAmount,
                DailyRate = rental.DailyRate
            };
        }

        public async Task<int> CreateRentalAsync(RentalRequestDto rentalRequest)
        {
            var car = await _carRepository.GetByIdAsync(rentalRequest.CarId);
            if (car == null || car.AvailabilityStatus != "Available") throw new Exception("Car is not available for rent.");

            var rental = new Rental
            {
                CustomerId = rentalRequest.CustomerId,
                CarId = rentalRequest.CarId,
               
                RentalDate = rentalRequest.RentalDate,
                Status = "Rented",
                
            };

            car.AvailabilityStatus = "Rented";
            car.UnitsAvailable--;

            await _carRepository.UpdateAsync(car);
            await _rentalRepository.AddRentalAsync(rental);

            return rental.RentalId;
        }

        public async Task CompleteRentalAsync(int rentalId, RentalReturnDto rentalReturn)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) throw new Exception($"Rental with ID {rentalId} not found.");

            if (rental.Status != "Rented") throw new Exception("Rental is not in progress.");

            rental.ReturnDate = rentalReturn.ReturnDate;
            rental.Status = "Returned";

            if (rental.IsOverdue)
            {
                var overdueDays = (rentalReturn.ReturnDate - rental.ReturnDate.Value).TotalDays;
                rental.OverdueAmount = Math.Ceiling(overdueDays) * (rental.OverdueRatePerDay ?? rental.DailyRate);
            }

            var car = await _carRepository.GetByIdAsync(rental.CarId);
            car.AvailabilityStatus = "Available";
            car.UnitsAvailable++;

            await _rentalRepository.UpdateRentalAsync(rental);
            await _carRepository.UpdateAsync(car);
        }
    }
}

