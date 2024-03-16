using GrotHotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GrotHotel.HotelRepository.IServices
{
    public interface IHotelListService
    {
        Task<List<Hotel>> GetHotels();
        Task<Hotel> GetRooms(int id);
        Task<HotelRoom> GetRates(int id);
        Task<HttpResponseMessage> AddHotel(Hotel hotel);
        Task<HttpResponseMessage> AddRoom(HotelRoom hotelRoom);
        Task<HttpResponseMessage> AddRate(RoomRate roomRate);
        Task<Hotel> EditHotel(int id);
        Task<HttpResponseMessage> EditHotel(Hotel hotel);
        Task<HttpResponseMessage> EditHotelImage(Hotel hotel);
        Task<HotelRoom> EditRoom(int id);
        Task<HttpResponseMessage> EditRoom(HotelRoom hotelRoom);
        Task<HttpResponseMessage> EditRoomImage(HotelRoom hotelRoom);
        Task<RoomRate> EditRate(int id);
        Task<HttpResponseMessage> EditRate(RoomRate roomRate);
        Task<HttpResponseMessage> Delete(int id);
        Task<HttpResponseMessage> DeleteRoom(int id);
        Task<HttpResponseMessage> DeleteRate(int id);


        //Serialization
        Task<HotelUpdate> HotelSerialize(Hotel hotel);
        Task<HotelUpdate> HotelSerializeWithImage(Hotel hotel);
        Task<HotelRoomUpdate> RoomSerialize(HotelRoom hotelRoom);
        Task<HotelRoomUpdate> RoomSerializeWithImage(HotelRoom hotelRoom);
    }
}