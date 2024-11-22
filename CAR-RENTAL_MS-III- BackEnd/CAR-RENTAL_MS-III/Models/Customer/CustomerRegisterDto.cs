using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Models.Customer
{
    public class CustomerRegisterDto
    {
        public string NicNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
    }

}
