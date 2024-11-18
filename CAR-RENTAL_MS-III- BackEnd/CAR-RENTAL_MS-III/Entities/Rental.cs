using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Rental
    {

        [Key]
        public int RentalId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        public string Status { get; set; } // "Rented", "Returned"

        public double? OverdueAmount { get; set; } // Overdue charges

        [Required]
        public double DailyRate { get; set; } // Rate per day for the rental

        public double? OverdueRatePerDay { get; set; } // Optional penalty rate per day

        // Determines if the rental is overdue
        public bool IsOverdue => ReturnDate.HasValue && ReturnDate.Value < DateTime.Now && Status == "Rented";
    }

}

