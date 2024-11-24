using CAR_RENTAL_MS_III.Entities.Enums;

namespace CAR_RENTAL_MS_III.Models.Car
{
    public class CarResponseDTO
    {
        //public int CarId { get; set; }
        //public string RegistrationNumber { get; set; }
        //public string Model { get; set; }
        //public string Brand { get; set; }
        //public int Year { get; set; }
        //public string CategoryName { get; set; }
        //public string AvailabilityStatus { get; set; }
        //public int UnitsAvailable { get; set; }
        //public string Image { get; set; }

        public int CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public int Year { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
        public int UnitsAvailable { get; set; }
        public string ImageUrl { get; set; }
    }
}
