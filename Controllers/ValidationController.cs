using Microsoft.AspNetCore.Mvc;

namespace GrotHotel.Controllers
{
    public class ValidationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
