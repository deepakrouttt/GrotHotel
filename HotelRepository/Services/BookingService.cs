using GrotHotel.HotelRepository.IServices;
using GrotHotel.Models;
using GrotHotelApi.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace GrotHotel.HotelRepository.Services
{
    public class BookingService : IBookingService
    {
        private readonly string baseUrl = "https://localhost:44309/api/BookingApi/";
        private readonly HttpClient _client;

        public BookingService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Hotel>> FilterHotels(Booking booking)
        {
            var url = $"{baseUrl}GetHotelsBySearch?DateFrom={booking.DateFrom.ToString("yyyy-MM-dd")}" +
                $"&DateTo={booking.DateTo.ToString("yyyy-MM-dd")}&Adult={booking.Adult}";
            var response = await _client.GetAsync(url);
            var Hotels = new List<Hotel>();
            myVar.TempBooking = booking;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Hotel>>(result);
                if (data != null)
                {
                    Hotels = data;
                }
            }
            return Hotels;
        }
        public async Task<Hotel> FilterHotelRooms(int id)
        {
            var Hotel =await FilterHotels(myVar.TempBooking);
            var filterHotel = Hotel.FirstOrDefault(h => h.HotelId == id);
            return filterHotel;
        }
    }
}
