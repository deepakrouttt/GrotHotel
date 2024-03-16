using System.ComponentModel.DataAnnotations;

namespace GrotHotelApi.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required][MaxLength(200)]
        public string Address { get; set; }
        
        public string ChildAgeRange { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public IFormFile HotelImageFile { get; set; }

        public string HotelImage { get; set; }
        [Required]
        public string Description { get; set; }
 
        public ICollection<HotelRoom>? HotelRooms { get; set; }
    }
    public class HotelUpdate
    {
        [Key]
        public int HotelId { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        public string ChildAgeRange { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public string HotelImage { get; set; }
        [Required]
        public string Description { get; set; }

        public ICollection<HotelRoom>? HotelRooms { get; set; }
    }
}
