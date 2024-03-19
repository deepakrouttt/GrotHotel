using GrotHotel.HotelRepository.IServices;
using GrotHotel.Models;
using GrotHotelApi.Models;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace GrotHotel.HotelRepository.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string baseUrl = "https://localhost:44309/api/UserApi/Login";

        public async Task<TempUser> ValidateUser(LoginUser _login)
        {
            var data = JsonConvert.SerializeObject(_login);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            using (var response = await _client.PostAsync(baseUrl, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    var validation = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TempUser>(validation);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }
}
