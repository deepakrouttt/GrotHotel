using GrotHotel.Models;
using GrotHotelApi.Models;

namespace GrotHotel.HotelRepository.IServices
{
    public interface IBookingService
    {
        Task<List<Hotel>> FilterHotels(Booking booking);
        Task<Hotel> FilterHotelRooms(int id);
    }
}