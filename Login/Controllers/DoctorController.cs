using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Controllers
{
    [Authorize(Roles = "Doctor Examines")]
    public class DoctorController : Controller
    {
        public IActionResult DoctorApoint()
        {
            return View();
        }
        public IActionResult ListPatient()
        {
            return View();
        }
        public IActionResult detailRegister()
        {
            return View();
        }


    }
}
