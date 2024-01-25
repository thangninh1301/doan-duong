
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.IServices.Base;

namespace BackEnd.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterTicketController : ControllerBase
    {
        private readonly IRegisterTicketAdmin _register;
        public RegisterTicketController(IRegisterTicketAdmin register)
        {
            _register = register;
        }

        [HttpGet]
        public IActionResult GetAllRegisterTicket()
        {
            return Ok(_register.GetAllRegisterTicket());
        }
        [HttpGet("admin")]
        public IActionResult GetAllRegisterTicket1()
        {
            return Ok(_register.GetAllAdmin());
        }

        [HttpGet("{Id}")]
        public IActionResult GetRegisterTicketId(int Id)
        {
            var db = _register.GetRegisterTicketId(Id);
            if (db!=null)
            {
                return Ok(db);
            }
            return NotFound("Not Found");
        }


        [HttpPost]
        public IActionResult AddRegisterTicket(RegisterTicket register)
        {

            _register.AddRegisterTicket(register);
            return Ok(register);
        }

        [HttpPut]
        public IActionResult EditRegisterTicket( RegisterTicket register)
        {
            RegisterTicket db = _register.GetRegisterTicketId(register.Id);
            if (db != null)
            {
                db.Symptom = register.Symptom;
                db.DateMeet = register.DateMeet;
                db.IdTimeMeet = register.IdTimeMeet;
                db.Deleted = register.Deleted;
                db.Status = register.Status;
                _register.EditRegisterTicket(db);
                 return Ok(db);
            }
            return BadRequest();
        }

        [HttpPut("{Id}")]
        public IActionResult DeleteRegisterTicket(int Id)
        {
            RegisterTicket db = _register.GetRegisterTicketId(Id);
            if (db != null)
            {
                db.Deleted = true;
                _register.EditRegisterTicket(db);
                return NoContent();
            }
            return NotFound("Not Found");
        }
    }
}
