using GrotHotelApi.Models;

namespace GrotHotel.Models
{
    public class HotelsWithRate
    {
        public List<dynamicHotelRate> Hotels { get; set; }
        public string numberAdults { get; set; }
    }

    public class dynamicHotelRate
    {
        public Hotel Hotel { get; set; }
        public IEnumerable<dynamicRoomRate> RoomRates { get; set; }
    }
    public class dynamicRoomRate
    {
        public decimal Rate { get; set; }
    }
}
