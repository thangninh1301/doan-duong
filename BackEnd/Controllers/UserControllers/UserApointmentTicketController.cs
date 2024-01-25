using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.IServices.IUserServices;
using BackEnd.Models;

namespace BackEnd.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApointmentTicketController : ControllerBase
    {
        private readonly IUserApointmentTicket _userApointmentTicket;

        public UserApointmentTicketController(IUserApointmentTicket userApointmentTicket)
        {
            _userApointmentTicket = userApointmentTicket;
        }

        [HttpGet("{Id}")]
        public ActionResult<List<ApointmentTicket>> GetAll(string Id)
        {
           /* UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;*/
            return Ok(_userApointmentTicket.GetAllApointmentTicketById(Id));
        }
    }
}
