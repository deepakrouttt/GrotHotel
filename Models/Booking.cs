using Microsoft.Build.Framework;

namespace GrotHotel.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public int Adult { get; set; }

        public int Children { get; set; }
        [Required]
        public decimal Rate { get; set; }
    }

}
