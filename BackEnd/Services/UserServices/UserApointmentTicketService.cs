using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.IUserServices;
using BackEnd.Models;

 
namespace BackEnd.Services.UserServices
{
    public class UserApointmentTicketService:IUserApointmentTicket
    {
       
        private readonly AppDBContext _context;
        public UserApointmentTicketService(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<dynamic> GetAllApointmentTicketById(string id)
        {
            /*var data = _context.ApointmentTickets.Where(a=>a.Id == )*/
            var query = from ap in _context.ApointmentTickets.ToList()
                        join us in _context.ApplicationUsers.ToList() on ap.IdDoctor equals us.Id
                        join dp in _context.Departments.ToList() on us.IdDepartment equals dp.Id
                        join rs in _context.Results.ToList().DefaultIfEmpty() on ap.Id equals rs.IdApointmentTicket
                        join rg in _context.RegisterTickets.ToList().Where(x=>x.IdPatient==id) on ap.IdRegisterTicket equals rg.Id
                        join t in _context.TimeSlot.ToList() on rg.IdTimeMeet equals t.Id

                         select new
                         {
                             Id = ap.Id,
                             TimeMeet = t.Decription,
                             Department = dp.Name,
                             RegisterTicket = rg.Id,
                             DateMeet = ap.DateMeet,
                             DateCreate = ap.DateCreate,
                             Status = ap.Status,
                             Decription = ap.Decription,
                             Deleted = ap.Deleted


                         };
            return query;
        }
    }
}
