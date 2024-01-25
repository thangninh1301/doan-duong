using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.IServices.Base;
using BackEnd.Service;

namespace BackEnd.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlot _timeSlot;
        public TimeSlotController(ITimeSlot timeSlot)
        {
            _timeSlot = timeSlot;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TimeSlot>> getalluser()
        {
            return Ok(_timeSlot.getAllUser());
        }
        [HttpGet("Admin")]
        public ActionResult<IEnumerable<TimeSlot>> getAllAdmin()
        {
            return Ok(_timeSlot.getAllAdmin());
        }
        [HttpGet("{Id}")]
        public ActionResult<TimeSlot> getId(int Id)
        {
            return Ok(_timeSlot.getById(Id));
        }
        [HttpPost]
        public ActionResult<TimeSlot> add(TimeSlot time) {
            if (time != null) {
                _timeSlot.Add(time);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult upDate(TimeSlot time)
        {
            TimeSlot db = _timeSlot.getById(time.Id);
            try {
                
                db.Decription = time.Decription;
                db.Deleted = time.Deleted;
                _timeSlot.Update(db);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
            
        }
        [HttpPut("delete/{Id}")]
        public IActionResult delete(int Id)
        {
            try
            {
                TimeSlot db = _timeSlot.getById(Id);

                db.Deleted = true;
                _timeSlot.Update(db);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
    }
}