using GrotHotel.HotelRepository.IServices;
using GrotHotel.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using GrotHotelApi.Models;

namespace GrotHotel.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IUserService _service;

        public ValidationController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser _login)
        {
            var validateUser = await _service.ValidateUser(_login);
            if (validateUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, validateUser.username),
                     new Claim(ClaimTypes.Role, validateUser.role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetInt32("UserId", validateUser.id);
                HttpContext.Session.SetString("Roles", validateUser.role);
                HttpContext.Session.SetString("UserName", validateUser.username);
                if (validateUser.role == "Admin")
                {
                    return RedirectToAction("Index", "Hotel");
                }
                if (validateUser.role == "Consumer")
                {
                    return RedirectToAction("Index", "Booking");
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var response = await _service.RegisterUser(user);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
