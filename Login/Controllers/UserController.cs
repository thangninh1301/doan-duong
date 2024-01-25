using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "Patient,Admin")]

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
