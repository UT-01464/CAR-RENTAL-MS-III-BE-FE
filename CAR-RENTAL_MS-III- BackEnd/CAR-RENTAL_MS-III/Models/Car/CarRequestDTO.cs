namespace CAR_RENTAL_MS_III.Models.Car
{
    public class CarRequestDTO
    {
        
  

        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public string AvailabilityStatus { get; set; }
        public int UnitsAvailable { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
