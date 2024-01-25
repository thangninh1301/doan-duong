using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;

namespace BackEnd.Service.Base
{
    public class ApointmentTicketService : IApointmentTicket
    {
        private readonly AppDBContext _context;
         public ApointmentTicketService(AppDBContext context) {
            this._context = context;
        }
        public void Create(ApointmentTicket db)
        {
                _context.ApointmentTickets.Add(db);
                _context.SaveChanges();
        }

        public void Delete(ApointmentTicket Id)
        {
                _context.ApointmentTickets.Remove(Id);
                _context.SaveChanges();
        }


        public IEnumerable<dynamic> GetAllAdmin()
        {
            var data = _context.ApointmentTickets.OrderBy(x=>x.Status);
            var input = from ap in data
                        join us in _context.ApplicationUsers on ap.IdDoctor equals us.Id
                        join dp in _context.Departments on us.IdDepartment equals dp.Id   
                        join t in _context.TimeSlot on ap.IdTimeMeet equals t.Id
                        select new
                        {
                            id = ap.Id,
                            decriptionTimeMet = ap.TimeSlot.Decription,
                            doctor = ap.Doctor.LastName,
                            nameDepartment = ap.Doctor.department.Name,
                            idRegisterTicket = ap.IdRegisterTicket,
                            dateMeet = ap.DateMeet,
                            dateCreate = ap.DateCreate,
                            status = ap.Status,
                            decription = ap.Decription,
                            deleted = ap.Deleted,
                            idTimeMeet = ap.TimeSlot.Id,
                            idDepartment = ap.Doctor.department.Id,
                            idDoctor = ap.Doctor.Id

                        };


            return input;
        }
        public IEnumerable<ApointmentTicket> GetAllUser()
        {
            return _context.ApointmentTickets.ToList<ApointmentTicket>().Where(item => item.Deleted == false);
        }

        public ApointmentTicket GetById(int id)
        {
            if (id ==0) { return null; }

             var a= _context.ApointmentTickets.Find(id);
            
            if (a.Deleted == true) return null;
            return a;
        }

        public void Update (ApointmentTicket db)
        {
                _context.ApointmentTickets.Update(db);
                _context.SaveChanges(); 
        }
    }
}
