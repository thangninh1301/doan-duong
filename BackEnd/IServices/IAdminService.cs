using BackEnd.DTO;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.IService
{
   public interface IAdminService
    {
        public Models.ApplicationUser getDoctorHaveApointMin(DateTime datemeet, int idTimeslot, int idDepart);
        public IEnumerable<dynamic> getDoctorDepartment(int IdDepart);
        public IEnumerable<dynamic> getAllRegisterTicket();
        public int add(ApointmentTicket ob);
        public IEnumerable<dynamic> getRegisterTicketByDate(string requestDate);
        public IEnumerable<dynamic> getDoctorNotinDepartment();
        public IEnumerable<dynamic> GetsAllUserRoles();
        public  string  AddRoleForUser(UserRoleRequest use);
        public IEnumerable<dynamic>  getAllDocotor();
        public IEnumerable<dynamic> getAllDocotorTest();
        //thống kê bệnh nhân theo bác sĩ
        public IEnumerable<dynamic> GetPatient();
        //thống kê Bác sỹ theo khoa   
        public IEnumerable<dynamic> GetDoctorByDepart();


    }
}
