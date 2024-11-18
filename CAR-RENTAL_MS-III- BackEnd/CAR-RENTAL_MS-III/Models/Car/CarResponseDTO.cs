namespace CAR_RENTAL_MS_III.Models.Car
{
    public class CarResponseDTO
    {
        public int CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public string CategoryName { get; set; }
        public string AvailabilityStatus { get; set; }
        public int UnitsAvailable { get; set; }
    }
}
