using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Models.Rentals
{
    public class RentalRequestDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int CarId { get; set; }


        [Required]
        public DateTime RentalDate { get; set; }

    }
}
