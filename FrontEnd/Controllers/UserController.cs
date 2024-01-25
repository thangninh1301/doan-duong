using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserRegisterTicket()
        {
            return View();
        }
        public IActionResult Start()
        {
            return View();
        }
    }
}
