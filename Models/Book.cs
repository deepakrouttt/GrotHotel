using Microsoft.Build.Framework;

namespace GrotHotel.Models
{
    public class Book
    {
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public int Adult { get; set; }
        [Required]
        public int Children { get; set; }
    }
}
