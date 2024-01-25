
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.IServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackEnd.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApointmentTicketsController : ControllerBase
    {
        private readonly IApointmentTicket _apointmentTickets;

        public ApointmentTicketsController(IApointmentTicket apoint)
        {
            this._apointmentTickets = apoint;
        }
        [HttpGet]
        public IEnumerable<ApointmentTicket> Getall()
        {
            return _apointmentTickets.GetAllUser();
        }
        //apoint(duyen sua)
        [HttpGet("Admin")]
        public IEnumerable<dynamic> GetallAdmin()
        {
            return _apointmentTickets.GetAllAdmin();
        }
        [HttpGet("{Id}")]
        public IActionResult  GetById(int Id)
        {
            var apoint = _apointmentTickets.GetById(Id);
            if (apoint != null)
            {
                return Ok(apoint);
            }
            return NotFound("notfound");

        }

        [HttpPost]
        public IActionResult Create(ApointmentTicket db)
        {

           
                _apointmentTickets.Create(db);
                return Ok();
            
        }

        [HttpPut]
        public IActionResult Update(ApointmentTicket obj)
        {
            ApointmentTicket db = _apointmentTickets.GetById(obj.Id);
            if (db!=null) {
                db.Decription = obj.Decription;
                db.Deleted = obj.Deleted;
                db.IdTimeMeet = obj.IdTimeMeet;
                db.IdDoctor = obj.IdDoctor;
               db.Status = obj.Status;
                _apointmentTickets.Update(db);
                
                return Ok();
            }return  BadRequest();
        }
        [HttpPut("{Id}")]
        public IActionResult Delete(int Id)
        {
            ApointmentTicket db = _apointmentTickets.GetById(Id);
            if (db != null)
            {
                db.Deleted = true;
                _apointmentTickets.Update(db);
                return Ok();
            }
            return BadRequest();
        }
    }
    
}
