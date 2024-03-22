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

        public async Task<List<dynamicHotelRate>> FilterHotels(Booking booking)
        {
            var url = $"{baseUrl}GetHotelsBySearch?DateFrom={booking.DateFrom.ToString("yyyy-MM-dd")}" +
                $"&DateTo={booking.DateTo.ToString("yyyy-MM-dd")}&Adult={booking.Adult}&Children={booking.Children}";
            var response = await _client.PostAsync(url,null);
            var Hotels = new List<dynamicHotelRate>();
            myVar.TempBooking = booking;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<HotelsWithRate>(result);
                if (data != null)
                {
                    Hotels = data.Hotels;
                    myVar.numberAdults = data.numberAdults;
                }
            }
            return Hotels;
        }
        public async Task<dynamicHotelRate> FilterHotelRooms(int id)
        {
            var Hotel = await FilterHotels(myVar.TempBooking);

            var filterHotel = Hotel.FirstOrDefault(h => h.Hotel.HotelId == id);
            return filterHotel;
        }
    }
}
