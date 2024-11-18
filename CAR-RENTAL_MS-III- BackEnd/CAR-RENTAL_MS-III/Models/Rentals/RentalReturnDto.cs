using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Models.Rentals
{
    public class RentalReturnDto
    {
        [Required]
        public DateTime ReturnDate { get; set; }
    }
}
