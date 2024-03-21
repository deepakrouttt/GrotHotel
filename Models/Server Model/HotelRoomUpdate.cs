using GrotHotelApi.Models;
using System.ComponentModel.DataAnnotations;

namespace GrotHotel.Models.Server_Model
{
    public class HotelRoomUpdate
    {
        [Key]
        public int HotelRoomId { get; set; }
        public string Title { get; set; }

        public string RoomPicture { get; set; }

        public string Description { get; set; }

        public int HotelId { get; set; }

        public ICollection<RoomRate>? RoomRates { get; set; }
    }

}
