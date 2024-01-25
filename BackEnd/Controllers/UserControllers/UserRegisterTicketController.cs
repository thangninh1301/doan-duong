using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.IServices.IUserServices;
using BackEnd.Models;

namespace BackEnd.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterTicketController : ControllerBase
    {
        private readonly IUserRegisterTicket _userRegisterTicket;
     
        public UserRegisterTicketController(IUserRegisterTicket userRegisterTicket)
        {
            _userRegisterTicket = userRegisterTicket;
          
        }

        [HttpGet("{IdPatient}")]
        public ActionResult<List<RegisterTicket>> GetById(string IdPatient)
        {
           
            var register = _userRegisterTicket.GetAllRegisterTicketById(IdPatient);
            return Ok(register);
        }
        //tìm kiếm
        [HttpGet("date/{search}/{idPatient}")]
        public ActionResult<List<RegisterTicket>> SearchUserRegisterTicket(string search, string idPatient)
        {
           
                var result = _userRegisterTicket.SearchUserRegisterTicket(search, idPatient);

                if (result!=null)
                {
                    return Ok(result);
                }
                
                return NotFound();
          
        }
    }
}
