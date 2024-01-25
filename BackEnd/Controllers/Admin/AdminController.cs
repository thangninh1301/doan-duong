using BackEnd.DTO;
using BackEnd.IService;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService  _adminService;

        public AdminController( IAdminService adminService)
        {
            this._adminService = adminService;
        }
        //lấy tất cả các registerticket
        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> getAll()
        {
            return Ok(_adminService.getAllRegisterTicket()) ;
        }
        //lấy bác sỹ có số phiếu ít nhất trong 1 phòng ban tại 1 khung giờ 1 ngày nào đó
        [HttpGet("DoctorHaveApointmin/{datemeet}/{idTimeSlot}/{idDepart}")]
        public ActionResult<Models.ApplicationUser> DoctorHaveApointmin(DateTime datemeet,int idTimeSlot,int idDepart)
        {
            return Ok(_adminService.getDoctorHaveApointMin(datemeet,idTimeSlot,idDepart));
        }
        //lấy bác sỹ
        [HttpGet("Doctor/{Id}")]
        public ActionResult<IEnumerable<dynamic>> getDoctorDepart(int Id)
        {
            return Ok(_adminService.getDoctorDepartment(Id));
        }
        // lấy phiếu hẹn theo ngày
        [HttpGet("Date/{Date}")]
        public ActionResult<IEnumerable<dynamic>> getByDate(string Date)
        {
            return Ok(_adminService.getRegisterTicketByDate(Date));
        }
        // lấy tất cả các user và list role
        [HttpGet("AllUserRole")]
        public ActionResult<IEnumerable<dynamic>> getallUserRoll()
        {
            return Ok(_adminService.GetsAllUserRoles());
        }
        // them phiếu hẹn
        [HttpPost("Add")]
        public ActionResult add(ApointmentTicket ob)
        {
            if (ob != null)
            {
                _adminService.add(ob);
                return Ok();

            }return BadRequest();
           
        }
        [HttpPost("AddRoleForUser")]
        public ActionResult addUserRole(UserRoleRequest ob)
        {
            if (ob != null)
            {
                _adminService.AddRoleForUser(ob);
                return Ok();

            }
            return BadRequest();

        }
        //getall Doctor

        [HttpGet("AllDoctor")]
        public ActionResult<IEnumerable<dynamic>> getAllDoctor()
        {
            return Ok(_adminService.getAllDocotor());
        }
        //getall DoctorTest

        [HttpGet("AllDoctorTest")]
        public ActionResult<IEnumerable<dynamic>> getAllDoctorTest()
        {
            return Ok(_adminService.getAllDocotorTest());
        }
        //thống kê bệnh nhân theo bác sĩ

        [HttpGet("statistic/patient")]
        public ActionResult<List<ApointmentTicket>> GetPatient()
        {

            var x = _adminService.GetPatient();
            return Ok(x);
        }

        //thống kê Bác sỹ theo khoa
        [HttpGet("Statistic/doctorInDepart")]
        public ActionResult<List<ApointmentTicket>> GetPatientByTime()
        {

            var x = _adminService.GetDoctorByDepart();
            return Ok(x);
        }
    }
}
