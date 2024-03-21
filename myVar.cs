using GrotHotel.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GrotHotel
{
    public class myVar
    {
        public static string Role {get;set;}
        public static Booking TempBooking { get; set; }
        public static int numberAdults { get; set; }

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
    }
}
