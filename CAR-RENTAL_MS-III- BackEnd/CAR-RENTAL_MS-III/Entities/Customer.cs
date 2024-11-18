using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }

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

        public string Address { get; set; }

        public string Role { get; set; } = "Customer";

        public DateTime RegistrationDate { get; set; }

        public ICollection<Rental> Rentals { get; set; }
       

    }
}
