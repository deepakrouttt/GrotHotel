using GrotHotel.Models;
using GrotHotelApi.Models;

namespace GrotHotel.HotelRepository.IServices
{
    public interface IBookingService
    {
        Task<List<dynamicHotelRate>> FilterHotels(Booking booking);
        Task<dynamicHotelRate> FilterHotelRooms(int id);
    }
}