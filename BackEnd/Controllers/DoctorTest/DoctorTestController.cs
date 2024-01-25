using BackEnd.Data;
using BackEnd.IServices.IDoctorTestService;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace BackEnd.Controllers.DoctorTest
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorTestController : ControllerBase
    {
        private readonly IDoctorTestService _service;
        private readonly AppDBContext _context;
        public DoctorTestController(IDoctorTestService service, AppDBContext context)
        {
            _service = service;
            _context = context;
        }
        [HttpGet("{IdDoctor}")]
        public ActionResult<IEnumerable<Models.Test>> GetAllTestforDoctor(string IdDoctor)
        {
            if (IdDoctor != null)
            {
                return Ok(_service.GetTests(IdDoctor));
            }
            return NotFound();
        }
        [HttpPut]
        public ActionResult<IEnumerable<Models.Test>> UpdateResultDetail(Models.ResultDetail resultDetail)
        {
            if (resultDetail != null) return Ok(_service.UpdateResultDetail(resultDetail));
            return BadRequest();
        }
        [HttpDelete("{IdResult}/{IdDoctorTest}")]
        public ActionResult<bool> DeleteResultDetail(int IdResult,string IdDoctorTest)
        {
            if (IdResult != 0 && IdDoctorTest != null) return Ok(_service.DeleteResultDetail(IdResult, IdDoctorTest));
            return BadRequest();
        }
        [HttpGet("aa/{doctorTestId}")]
        public ActionResult<IEnumerable<dynamic>> GetAllGetAllTestregister(string doctorTestId)
        {

            var apoint = _service.GetAllTestregister(doctorTestId);
            return Ok(apoint);
        }
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Update(/*[FromForm] IFormFile form*/ IFormCollection data)
        {
            
            var file = Request.Form.Files[0];
           
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                   
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    
                    
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                
                    var idt = Request.Form["idDoctorTest"];
                    var irs = Request.Form["idResult"];
                    var dcr = Request.Form["dateCreate"];
                    var dia = Request.Form["diagnostic"];
                    var ob = _context.ResultDetails.Find(Convert.ToInt32(irs[0]), idt[0]);
                    if (ob!=null)
                    {
                        ob.UrlFile = dbPath;
                        ob.DateUpdate = DateTime.Now;
                        ob.Diagnostic = dia;
                        _context.ResultDetails.Update(ob);
                        _context.SaveChanges();
                    }
                    return Ok(ob);
                }
                else
                {
                    return BadRequest();
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
    
        }
        [HttpGet("DoctorByTest/{IdTest}")]
        public ActionResult<IEnumerable<Models.ApplicationUser>> GetDoctorBytest(int IdTest)
        {
            try
            {
                return Ok(_service.getDoctorByIdTest(IdTest));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
    }
}
