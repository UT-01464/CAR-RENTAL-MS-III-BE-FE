using CAR_RENTAL_MS_III.Entities.Enums;

namespace CAR_RENTAL_MS_III.Models.Car
{
    public class CarRequestDTO
    {



        //public string RegistrationNumber { get; set; }
        //public string Model { get; set; }
        //public string Brand { get; set; }
        //public int Year { get; set; }
        //public int CategoryId { get; set; }
        //public string AvailabilityStatus { get; set; }
        //public int UnitsAvailable { get; set; }
        //public IFormFile ImageFile { get; set; }



        public string RegistrationNumber { get; set; }
        public int ModelId { get; set; }
        public int CategoryId { get; set; }
        public int Year { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
        public int UnitsAvailable { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
