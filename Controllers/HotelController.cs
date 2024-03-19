using GrotHotel.HotelRepository.IServices;
using GrotHotelApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GrotHotel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private readonly IHotelListService _service;

        public HotelController(IHotelListService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var Hotels = await _service.GetHotels();
            return View(Hotels);
        }

        public async Task<IActionResult> IndexRoom(int id)
        {
            var hotel = await _service.GetRooms(id);
            return View(hotel);
        }

        public async Task<IActionResult> IndexRoomRate(int id)
        {
            var hotelroom = await _service.GetRates(id);
            return View(hotelroom);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            var response = await _service.AddHotel(hotel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> CreateRoom(int id)
        {
            ViewData["HotelId"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(HotelRoom hotelRoom)
        {
            var response = await _service.AddRoom(hotelRoom);
            if (response.IsSuccessStatusCode)
            {
                return Redirect("/Hotel/IndexRoom/" + hotelRoom.HotelId);
            }
            return View();
        }

        public async Task<IActionResult> CreateRate(int id)
        {
            ViewData["HotelRoomId"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRate(RoomRate roomRate)
        {
            var response = await _service.AddRate(roomRate);
            if (response.IsSuccessStatusCode)
            {
                return Redirect("/Hotel/IndexRoomRate/" + roomRate.HotelRoomId);
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Hotel = await _service.EditHotel(id);
            if (Hotel != null) { return View(Hotel); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Hotel hotel)
        {
            var response = await _service.EditHotel(hotel);
            if (response.IsSuccessStatusCode) { return RedirectToAction("Index"); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditImage(Hotel hotel)
        {
            if (hotel.HotelImageFile == null) { return Redirect("Edit/" + hotel.HotelId); }
            var response = await _service.EditHotelImage(hotel);
            if (response.IsSuccessStatusCode) { return RedirectToAction("Index"); }
            return View("Edit");
        }

        public async Task<IActionResult> EditRoom(int id)
        {
            var hotelRoom = await _service.EditRoom(id);
            if (hotelRoom != null) { return View(hotelRoom); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(HotelRoom hotelRoom)
        {
            var response = await _service.EditRoom(hotelRoom);
            if (response.IsSuccessStatusCode) {
                return Redirect("/Hotel/IndexRoom/" + hotelRoom.HotelId);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRoomImage(HotelRoom hotelRoom)
        {
            if (hotelRoom.RoomPictureFile == null) { return Redirect("EditRoom/" + hotelRoom.HotelRoomId); }
            var response = await _service.EditRoomImage(hotelRoom);
            if (response.IsSuccessStatusCode) {
                return Redirect("/Hotel/IndexRoom/" + hotelRoom.HotelId);
            }
            return View("EditRoom");
        }

        public async Task<IActionResult> EditRate(int id)
        {
            var Rate = await _service.EditRate(id);
            if (Rate != null) { return View(Rate); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRate(RoomRate roomRate)
        {
            var response = await _service.EditRate(roomRate);
            if (response.IsSuccessStatusCode)
            {
                return Redirect("/Hotel/IndexRoomRate/" + roomRate.HotelRoomId);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Delete(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var response = await _service.DeleteRoom(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRate(int id)
        {
            var response = await _service.DeleteRate(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}