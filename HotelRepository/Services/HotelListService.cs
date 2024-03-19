using GrotHotel.HotelRepository.IServices;
using GrotHotelApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GrotHotel.HotelRepository.Services
{
    public class HotelListService : IHotelListService
    {
        private readonly string baseurl = "https://localhost:44309/api/HotelApi/";
        private readonly HttpClient _client = new HttpClient();
        public readonly IWebHostEnvironment _webHostEnvironment;

        public HotelListService(HttpClient client, IWebHostEnvironment webHostEnvironment)
        {
            _client = client;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var url = $"{baseurl}GetHotels";
            var response = await _client.GetAsync(url);
            var Hotels = new List<Hotel>();

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

        public async Task<Hotel> GetRooms(int id)
        {
            var url = $"{baseurl}GetHotel/{id}";
            var response = await _client.GetAsync(url);
            var hotel = new Hotel();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Hotel>(result);
                if (data != null)
                {
                    hotel = data;
                }
            }
            return hotel;
        }

        public async Task<HotelRoom> GetRates(int id)
        {
            var url = $"{baseurl}GetRoom/{id}";
            var response = await _client.GetAsync(url);
            var hotelroom = new HotelRoom();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<HotelRoom>(result);
                if (data != null)
                {
                    hotelroom = data;
                }
            }
            return hotelroom;
        }

        public async Task<HttpResponseMessage> AddHotel(Hotel hotel)
        {
            if (hotel.HotelImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images\\HotelImages");
                var orgFileName = Path.GetFileName(hotel.HotelImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, orgFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    hotel.HotelImageFile.CopyTo(fileStream);
                }
            }
            var hotelupdate = await HotelSerializeWithImage(hotel);

            var url = baseurl + "addHotel";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(hotelupdate), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, stringContent);
            return response;
        }

        public async Task<HttpResponseMessage> AddRoom(HotelRoom hotelRoom)
        {
            if (hotelRoom.RoomPictureFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images\\RoomImages");
                var orgFileName = Path.GetFileName(hotelRoom.RoomPictureFile.FileName);
                var filePath = Path.Combine(uploadsFolder, orgFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    hotelRoom.RoomPictureFile.CopyTo(fileStream);
                }
            }
            var hotelupdate = await RoomSerializeWithImage(hotelRoom);

            var url = baseurl + "addRoom";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(hotelupdate), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, stringContent);
            return response;
        }

        public async Task<HttpResponseMessage> AddRate(RoomRate roomRate)
        {
            var url = baseurl + "addRate";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(roomRate), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, stringContent);
            return response;
        }

        public async Task<Hotel> EditHotel(int id)
        {
            var url = $"{baseurl}GetHotel/{id}";
            var response = await _client.GetAsync(url);
            var hotel = new Hotel();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Hotel>(result);
                if (data != null)
                {
                    hotel = data;
                }
            }
            return hotel;
        }

        public async Task<HttpResponseMessage> EditHotel(Hotel hotel)
        {
            var productUpdate = await HotelSerialize(hotel);

            var url = $"{baseurl}UpdateHotel";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(productUpdate), Encoding.UTF8, "application/json");

            var response = _client.PutAsync(url, stringContent).Result;
            return response;
        }

        public async Task<HttpResponseMessage> EditHotelImage(Hotel hotel)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images\\HotelImages");
            var filePath = Path.Combine(uploadsFolder, hotel.HotelImage);
            if (System.IO.File.Exists(filePath)) { System.IO.File.Delete(filePath); }
            if (hotel.HotelImageFile.Length > 0)
            {
                var orgFileName = Path.GetFileName(hotel.HotelImageFile.FileName);
                var _filePath = Path.Combine(uploadsFolder, orgFileName);

                using (var fileStream = new FileStream(_filePath, FileMode.Create))
                {
                    hotel.HotelImageFile.CopyTo(fileStream);
                }
            }
            var productUpdate = await HotelSerializeWithImage(hotel);
            var url = $"{baseurl}UpdateHotel";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(productUpdate), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, stringContent);
            return response;
        }

        public async Task<HotelRoom> EditRoom(int id)
        {
            var url = $"{baseurl}GetRoom/{id}";
            var response = await _client.GetAsync(url);
            var hotelRoom = new HotelRoom();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<HotelRoom>(result);
                if (data != null)
                {
                    hotelRoom = data;
                }
            }
            return hotelRoom;
        }

        public async Task<HttpResponseMessage> EditRoom(HotelRoom hotelRoom)
        {
            var productUpdate = await RoomSerialize(hotelRoom);

            var url = $"{baseurl}UpdateRoom";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(productUpdate), Encoding.UTF8, "application/json");

            var response = _client.PutAsync(url, stringContent).Result;
            return response;
        }

        public async Task<HttpResponseMessage> EditRoomImage(HotelRoom hotelRoom)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images\\RoomImages");
            var filePath = Path.Combine(uploadsFolder, hotelRoom.RoomPicture);
            if (System.IO.File.Exists(filePath)) { System.IO.File.Delete(filePath); }
            if (hotelRoom.RoomPictureFile.Length > 0)
            {
                var orgFileName = Path.GetFileName(hotelRoom.RoomPictureFile.FileName);
                var _filePath = Path.Combine(uploadsFolder, orgFileName);

                using (var fileStream = new FileStream(_filePath, FileMode.Create))
                {
                    hotelRoom.RoomPictureFile.CopyTo(fileStream);
                }
            }
            var productUpdate = await RoomSerializeWithImage(hotelRoom);
            var url = $"{baseurl}UpdateRoom";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(productUpdate), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, stringContent);
            return response;
        }

        public async Task<RoomRate> EditRate(int id)
        {
            var url = $"{baseurl}GetRate/{id}";
            var response = await _client.GetAsync(url);
            var roomRate = new RoomRate();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<RoomRate>(result);
                if (data != null)
                {
                    roomRate = data;
                }
            }
            return roomRate;
        }

        public async Task<HttpResponseMessage> EditRate(RoomRate roomRate)
        {
            var url = $"{baseurl}UpdateRate";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(roomRate), Encoding.UTF8, "application/json");
            var response = _client.PutAsync(url, stringContent).Result;
            return response;
        }


        //Delete Method
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var hotel = await GetRooms(id);
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images\\HotelImages");
            var filePath = Path.Combine(uploadsFolder, hotel.HotelImage);
            if (System.IO.File.Exists(filePath)) { System.IO.File.Delete(filePath); }

            var url = $"{baseurl}DeleteHotel/{id}";
            var response = _client.DeleteAsync(url).Result;
            return response;
        }

        public async Task<HttpResponseMessage> DeleteRoom(int id)
        {
            var url = $"{baseurl}GetRoom/{id}";
            var response = await _client.GetAsync(url);
            var hotelRoom = new HotelRoom();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<HotelRoom>(result);
                if (data != null)
                {
                    hotelRoom = data;
                }
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images\\RoomImages");
            var filePath = Path.Combine(uploadsFolder, hotelRoom.RoomPicture);
            if (System.IO.File.Exists(filePath)) { System.IO.File.Delete(filePath); }

            var Deleteurl = $"{baseurl}DeleteRoom/{id}";
            var Deleteresponse = _client.DeleteAsync(Deleteurl).Result;
            return Deleteresponse;
        }


        public async Task<HttpResponseMessage> DeleteRate(int id)
        {
            var url = $"{baseurl}DeleteRate/{id}";
            var response = _client.DeleteAsync(url).Result;
            return response;
        }




        //Serialization 
        public async Task<HotelUpdate> HotelSerialize(Hotel hotel)
        {
            var hotelUpdate = new HotelUpdate
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                Address = hotel.Address,
                ChildAgeRange = hotel.ChildAgeRange,
                Rating = hotel.Rating,
                HotelImage = hotel.HotelImage,
                Description = hotel.Description
            };
            return hotelUpdate;
        }

        public async Task<HotelUpdate> HotelSerializeWithImage(Hotel hotel)
        {
            var hotelUpdate = new HotelUpdate
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                Address = hotel.Address,
                ChildAgeRange = hotel.ChildAgeRange,
                Rating = hotel.Rating,
                HotelImage = hotel.HotelImageFile.FileName,
                Description = hotel.Description
            };
            return hotelUpdate;
        }

        public async Task<HotelRoomUpdate> RoomSerialize(HotelRoom hotelRoom)
        {
            var RoomUpdate = new HotelRoomUpdate
            {
                HotelRoomId = hotelRoom.HotelRoomId,
                Title = hotelRoom.Title,
                RoomPicture = hotelRoom.RoomPicture,
                Description = hotelRoom.Description,
                HotelId = hotelRoom.HotelId
            };
            return RoomUpdate;
        }

        public async Task<HotelRoomUpdate> RoomSerializeWithImage(HotelRoom hotelRoom)
        {
            var RoomUpdate = new HotelRoomUpdate
            {
                HotelRoomId = hotelRoom.HotelRoomId,
                Title = hotelRoom.Title,
                RoomPicture = hotelRoom.RoomPictureFile.FileName,
                Description = hotelRoom.Description,
                HotelId = hotelRoom.HotelId
            };
            return RoomUpdate;
        }

    }
}
