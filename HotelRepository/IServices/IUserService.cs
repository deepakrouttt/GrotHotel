using GrotHotel.Models;
using GrotHotelApi.Models;

namespace GrotHotel.HotelRepository.IServices
{
    public interface IUserService
    {
        Task<TempUser> ValidateUser(LoginUser _login);
    }
}