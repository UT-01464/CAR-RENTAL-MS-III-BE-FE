namespace CAR_RENTAL_MS_III.Models.Rentals
{
    public class RentalResponseDto
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
        public double? OverdueAmount { get; set; }
        public double DailyRate { get; set; }
    }
}
