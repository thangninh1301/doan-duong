using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Controllers
{
    public class DoctorTestController : Controller
    {
        public IActionResult TestregisterTicket()
        {
            return View();
        }
    }
}
