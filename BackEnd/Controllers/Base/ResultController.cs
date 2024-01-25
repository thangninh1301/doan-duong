
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
    public class ResultController : ControllerBase
    {
        private readonly IResult _result;
        public ResultController(IResult result)
        {
            _result = result;
        }

        [HttpGet]
        public IActionResult GetAllResult()
        {
            return Ok(_result.GetAllResult());
        }

        [HttpGet("{Id}")]
        public IActionResult GetResultId(int Id)
        {
            var db = _result.GetResultId(Id);
            if (db != null)
            {
                return Ok(db);
            }
            return NotFound($"Not Found");
        }

        [HttpPost]
        public IActionResult AddResult(Result result)
        {
            _result.AddResult(result);
            return Ok(result);
        }

        [HttpPost("{Id}")]
        public IActionResult EditResult(int Id, Result result)
        {
            Result db = _result.GetResultId(Id);
            if (db != null)
            {
                db.IdApointmentTicket = result.IdApointmentTicket;
              
                db.Diagnostic = result.Diagnostic;
                db.TherapyRegiment = result.TherapyRegiment;
                db.DateCreate = result.DateCreate;
                db.Deleted = result.Deleted;
                _result.EditResult(db);
                return Ok(db);
            }
            return NotFound("Not Find!");
        }

        [HttpPut("{Id}")]
        public IActionResult DeleteResult(int Id)
        {
            Result db = _result.GetResultId(Id);
            if (db != null)
            {
                db.Deleted = true;
                _result.EditResult(db);
                return Ok();
            }
            return NotFound("not find");
        }
    }
}
