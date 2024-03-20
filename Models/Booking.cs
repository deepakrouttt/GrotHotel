using System.ComponentModel.DataAnnotations;

namespace GrotHotel.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select a date.")]
        public DateTime DateTo { get; set; }
        [Required]
        public int Adult { get; set; }

        public int Children { get; set; }
        [Required]
        public decimal Rate { get; set; }   
    }

}
