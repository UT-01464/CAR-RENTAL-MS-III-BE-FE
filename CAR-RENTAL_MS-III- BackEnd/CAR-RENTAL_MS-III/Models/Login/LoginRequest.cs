using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Models.Login
{
    public class LoginRequest
    {

        //[Required]
        //[StringLength(50)]
        //public string Username { get; set; }

        //[Required]
        //[EmailAddress]
        //[StringLength(100)]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string PasswordHash { get; set; }

        //public string Role { get; set; }



       
        [Required]
        [StringLength(20)]
        public string NicNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string ConformPassword { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

       
        public string Role { get; set; } = "Customer";



    }
}
