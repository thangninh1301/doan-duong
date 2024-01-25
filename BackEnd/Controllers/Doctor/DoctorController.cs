using BackEnd.IService;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers.Doctor
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctor;
        public DoctorController(IDoctorService doctor)
        {
            _doctor = doctor;
        }

        //lấy phiếu hẹn theo bác sĩ
        [HttpGet("{doctorId}")]
        public ActionResult<List<ApointmentTicket>> GetAllApointmentByDoctor(string doctorId)
        {

            var apoint = _doctor.GetAllApointmentByDoctor(doctorId);
            return Ok(apoint);
        }
        //lấy bệnh nhân theo bsi
        [HttpGet("doctor/{doctor}")]
        public ActionResult<List<dynamic>> GetAllPatient(string doctor)
        {

            var pa = _doctor.GetAllPatient(doctor);
            return Ok(pa);
        }

        //thêm kết quả
        [HttpPost("Add")]
        public ActionResult add(Result rs)
        {
            if (rs != null)
            {
                _doctor.add(rs);
                return Ok();

            }
            return BadRequest();

        }
    }
}
