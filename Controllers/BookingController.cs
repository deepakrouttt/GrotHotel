using GrotHotel.HotelRepository.IServices;
using GrotHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrotHotel.Controllers
{
    [Authorize(Roles = "Consumer")]
    public class BookingController : Controller
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> FilterHotels(Booking booking)
        {
            if (booking.DateFrom == DateTime.MinValue || booking.DateTo == DateTime.MinValue || booking.DateFrom > booking.DateTo)
            {
                ModelState.AddModelError(string.Empty, "Invalid date range. Please provide valid dates.");
                return View("Index");
            }

            var hotels = await _service.FilterHotels(booking);
            return View(hotels);
        }


        public async Task<ActionResult> FilterRooms(int id)
        {
            var HotelRooms = await _service.FilterHotelRooms(id);
            return View(HotelRooms);
        }

    }
}
