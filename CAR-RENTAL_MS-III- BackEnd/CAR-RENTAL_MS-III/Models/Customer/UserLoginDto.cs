using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Models.Customer
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
