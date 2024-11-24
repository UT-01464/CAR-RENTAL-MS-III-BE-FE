using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Entities.Enums;
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
                Status = r.Status.ToString(),
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
                Status = rental.Status.ToString(),
                OverdueAmount = rental.OverdueAmount,
                DailyRate = rental.DailyRate
            };
        }

        //public async Task<int> CreateRentalAsync(RentalRequestDto rentalRequest)
        //{
        //    var car = await _carRepository.GetByIdAsync(rentalRequest.CarId);
        //    if (car == null || car.AvailabilityStatus != "Available") throw new Exception("Car is not available for rent.");

        //    var rental = new Rental
        //    {
        //        CustomerId = rentalRequest.CustomerId,
        //        CarId = rentalRequest.CarId,

        //        RentalDate = rentalRequest.RentalDate,
        //        Status = "Rented",

        //    };

        //    car.AvailabilityStatus = "Rented";
        //    car.UnitsAvailable--;

        //    await _carRepository.UpdateAsync(car);
        //    await _rentalRepository.AddRentalAsync(rental);

        //    return rental.RentalId;
        //}



        //public async Task CompleteRentalAsync(int rentalId, RentalReturnDto rentalReturn)
        //{
        //    var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
        //    if (rental == null) throw new Exception($"Rental with ID {rentalId} not found.");

        //    if (rental.Status != "Rented") throw new Exception("Rental is not in progress.");

        //    rental.ReturnDate = rentalReturn.ReturnDate;
        //    rental.Status = "Returned";

        //    if (rental.IsOverdue)
        //    {
        //        var overdueDays = (rentalReturn.ReturnDate - rental.ReturnDate.Value).TotalDays;
        //        rental.OverdueAmount = Math.Ceiling(overdueDays) * (rental.OverdueRatePerDay ?? rental.DailyRate);
        //    }

        //    var car = await _carRepository.GetByIdAsync(rental.CarId);
        //    car.AvailabilityStatus = "Available";
        //    car.UnitsAvailable++;

        //    await _rentalRepository.UpdateRentalAsync(rental);
        //    await _carRepository.UpdateAsync(car);
        //}

        public async Task<int> CreateRentalAsync(RentalRequestDto rentalRequest)
        {
            var car = await _carRepository.GetByIdAsync(rentalRequest.CarId);
            if (car == null || car.AvailabilityStatus != AvailabilityStatus.Available)
                throw new Exception("Car is not available for rent.");

            var rental = new Rental
            {
                CustomerId = rentalRequest.CustomerId,
                CarId = rentalRequest.CarId,
                RentalDate = rentalRequest.RentalDate,
                Status = RentalStatus.Rented, // Use enum
            };

            car.AvailabilityStatus = AvailabilityStatus.Rented; // Set to the enum value
            car.UnitsAvailable--;

            await _carRepository.UpdateAsync(car);
            await _rentalRepository.AddRentalAsync(rental);

            return rental.RentalId;
        }


        public async Task CompleteRentalAsync(int rentalId, RentalReturnDto rentalReturn)
        {
            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null) throw new Exception($"Rental with ID {rentalId} not found.");

            if (rental.Status != RentalStatus.Rented) throw new Exception("Rental is not in progress.");

            rental.ReturnDate = rentalReturn.ReturnDate;
            rental.Status = RentalStatus.Returned; // Use enum for the status

            // Calculate overdue amount if the car is overdue
            if (rental.IsOverdue)
            {
                // Calculate overdue days
                var overdueDuration = DateTime.Now - rental.ReturnDate.Value;
                var overdueDays = (int)Math.Ceiling(overdueDuration.TotalDays); // Round up if overdue for any part of a day

                // If overdue, calculate overdue charges
                rental.OverdueAmount = overdueDays * (rental.OverdueRatePerDay ?? rental.DailyRate);
            }

            var car = await _carRepository.GetByIdAsync(rental.CarId);

            // Correctly assign the AvailabilityStatus using the enum
            car.AvailabilityStatus = AvailabilityStatus.Available;

            // Increment the available units
            car.UnitsAvailable++;

            // Update the rental and car records
            await _rentalRepository.UpdateRentalAsync(rental);
            await _carRepository.UpdateAsync(car);
        }

    }
}

