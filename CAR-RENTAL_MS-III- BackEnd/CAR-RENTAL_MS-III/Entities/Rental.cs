using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Rental
    {

        //[Key]
        //public int RentalId { get; set; }

        //[ForeignKey("Customer")]
        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }

        //[ForeignKey("Car")]
        //public int CarId { get; set; }
        //public Car Car { get; set; }

        //[ForeignKey("Manager")]
        //public int ManagerId { get; set; }
        //public Manager Manager { get; set; }

        //public DateTime RentalDate { get; set; }

        //public DateTime? ReturnDate { get; set; }

        //[Required]
        //public string Status { get; set; } // "Rented", "Returned"

        //public double? OverdueAmount { get; set; } // Overdue charges

        //[Required]
        //public double DailyRate { get; set; } // Rate per day for the rental

        //public double? OverdueRatePerDay { get; set; } // Optional penalty rate per day

        //// Determines if the rental is overdue
        //public bool IsOverdue => ReturnDate.HasValue && ReturnDate.Value < DateTime.Now && Status == "Rented";




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

        // Rental date now includes time to track hourly rentals
        public DateTime RentalDate { get; set; }

        // Return date now includes time
        public DateTime? ReturnDate { get; set; }

        [Required]
        public RentalStatus Status { get; set; } // Using Enum for status

        // Optional, for storing the overdue charges
        public double? OverdueAmount { get; set; } // Overdue charges

        [Required]
        public double DailyRate { get; set; } // Rate per day for the rental

        public double? OverdueRatePerDay { get; set; } // Optional penalty rate per day

        // Determines if the rental is overdue based on the return date and current time
        public bool IsOverdue
        {
            get
            {
                if (ReturnDate.HasValue && Status == RentalStatus.Rented)
                {
                    var overdueDuration = DateTime.Now - ReturnDate.Value;
                    return overdueDuration.TotalMinutes > 0; // Overdue if any minutes have passed
                }
                return false;
            }
        }

        // Calculate the total rental duration in hours, days, or weeks
        public double GetRentalDuration()
        {
            DateTime endDate = ReturnDate ?? DateTime.Now; // If no return date, use the current time
            var duration = endDate - RentalDate;
            return duration.TotalHours; // You can return TotalDays or TotalWeeks based on your requirements
        }

        // Optional method to calculate overdue charges based on the overdue period
        public double CalculateOverdueCharges()
        {
            if (OverdueRatePerDay.HasValue && IsOverdue)
            {
                var overdueDuration = DateTime.Now - ReturnDate.Value;
                var overdueDays = (int)overdueDuration.TotalDays;

                if (overdueDays > 0)
                {
                    // Calculate the overdue amount based on days
                    return overdueDays * OverdueRatePerDay.Value;
                }
            }
            return 0;
        }
    }

    // Enum to better manage rental status
    public enum RentalStatus
    {
        Rented,
        Returned
    }
}



