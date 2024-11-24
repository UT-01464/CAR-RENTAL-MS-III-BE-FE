using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CAR_RENTAL_MS_III.Entities.Enums;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Car
    {
        //[Key]
        //public int CarId { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string RegistrationNumber { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string Model { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Brand { get; set; }

        //public int Year { get; set; }

        //[ForeignKey("Category")]
        //public int CategoryId { get; set; }
        //public CarCategory Category { get; set; }

        //[Required]
        //public string AvailabilityStatus { get; set; }

        //public int UnitsAvailable { get; set; }

        //public string Image { get; set; }

        //public ICollection<Rental> Rentals { get; set; }
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }
        public Model Model { get; set; }

        public int Year { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public CarCategory Category { get; set; }

        [Required]
        public AvailabilityStatus AvailabilityStatus { get; set; }

        public int UnitsAvailable { get; set; }

        public string Image { get; set; }

        public ICollection<Rental> Rentals { get; set; }

    }
}
