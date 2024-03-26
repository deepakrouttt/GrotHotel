using GrotHotel.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;

namespace GrotHotel
{
    public class myVar
    {
        public static string Role {get;set;}
        public static Booking TempBooking { get; set; }
        public static int numberAdults { get; set; }
        public static int numberChild { get; set; }

        public static int Index { get; set; }

        public static string NumberFormeting(decimal price)
        {
            string formatted = price.ToString("#,##0.00");
            return formatted;
        }

        public static List<string> DiscriptionFormat(string discription)
        {
            var ListDiscription = discription.Split(',').ToList();
            return ListDiscription;
        }

        public static async Task<BlackoutData> ListOfBlackOutDates(int id) {

            var url = "https://localhost:44309/api/HotelApi/GetBlackOutDate/";
            var client = new HttpClient();
            var response = await client.GetAsync(url + id);

            var content =  await response.Content.ReadAsStringAsync();
            var datelist = JsonConvert.DeserializeObject<BlackoutData>(content);

            return datelist;
        }
    }
}
