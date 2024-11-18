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

        public string Status { get; set; }
        //public ICollection<Payment> Payments { get; set; }



        // Overdue flag derived from the current date and the ReturnDate
        public bool IsOverdue
        {
            get
            {
                return ReturnDate.HasValue && ReturnDate.Value < DateTime.Now && Status != "Completed";
            }
        }

    }
}
