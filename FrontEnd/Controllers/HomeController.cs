using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult ApointmentTickerAdmin()
        {
            return View();
        }
        public IActionResult TimeSlot()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
       
        
        public IActionResult DoctorManage()
        {
            return View();
        }
        public IActionResult DepartmentManage()
        {
            return View();
        }
        public IActionResult RegisterTicket()
        {
            return View();
        }
        public IActionResult RegisterTicketAdmin()
        {
            return View();
        }
        public IActionResult AddRoleForUser()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
