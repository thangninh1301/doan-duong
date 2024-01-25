using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.IServices.IUserServices;
using BackEnd.Data;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services.UserServices
{
    public class UserRegisterTicketService : IUserRegisterTicket
    {
        private readonly AppDBContext _context;
        public UserRegisterTicketService(AppDBContext context)
        {
            _context = context;
        }

       // ds phieu dki theo benh nhan
        public IEnumerable<dynamic> GetAllRegisterTicketById(string id)
        {
          
            var data = _context.RegisterTickets.Where(rg => rg.IdPatient == id && rg.Deleted != true).OrderBy(o => o.Status).ThenBy(u=>u.DateCreate);
            var input = from r in data

                        join ap in _context.ApointmentTickets.Where(x=> x.Deleted !=true).DefaultIfEmpty()
                        on r.Id equals ap.IdRegisterTicket into t
                        from ap in t.DefaultIfEmpty()
                        orderby ap.Status descending

                        select new
                        {
                            idRegis= r.Id,
                            IdPatient = r.User.Id,
                            LastName = r.User.LastName,
                            symptomRegis = r.Symptom,
                            dateMeetRegis = r.DateMeet,
                            timeMeetRegis = r.Timeslot.Decription,
                            dateCreateRegis = r.DateCreate,
                            deletedRegis = r.Deleted,
                            statusRegis = r.Status,
                            idTimeMeet = r.IdTimeMeet,
                           

                            ApointmentTicket1 = ap == null ? null : ap,
                        
                            Doctor1 = ap == null ? "" :  ap.Doctor.LastName,
                            departmentApoint = ap == null ? "" : ap.Doctor.department.Name,
                            statusApoint = ap == null ? 5 : ap.Status,
                            timeMeetApoint = ap == null ? "" : ap.TimeSlot.Decription,
                            deleteApoint = ap == null ? false : ap.Deleted,

                            result = ap.Result == null ? null : ap.Result,
                            idRs = ap.Result == null ? 0 : ap.Result.Id,
                            diagnostic = ap.Result == null ? "" : ap.Result.Diagnostic,
                            therapyRegiment = ap.Result == null ? "" : ap.Result.TherapyRegiment,
                            dateCreate = ap.Result == null ? null : ap.Result.DateCreate
                        };
            return input;
        }

        // ds phieu hen theo benh nhan
        public IEnumerable<dynamic> GetAllApointmentByPatient(string idPatient)
        {
            var a = _context.RegisterTickets.Where(rg => rg.IdPatient == idPatient && rg.Deleted != true).OrderBy(o => o.DateCreate).ThenBy(u => u.Status);
            var x = from r in a
                    join ap in _context.ApointmentTickets.Where(x => x.Status > 0).DefaultIfEmpty() on r.Id equals ap.IdRegisterTicket into t
                    from ap in t.DefaultIfEmpty()
                    orderby ap.Status ascending
                    select new
                    {

                    };
            return x;


        }

        //tìm kiếm
        public IEnumerable<RegisterTicket> SearchUserRegisterTicket(string search, string idPatient)
        {
            IEnumerable<RegisterTicket> query = _context.RegisterTickets.Where(x=>x.IdPatient == idPatient);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.DateCreate.ToString("yyyy-MM-dd").Contains(search)
                            || e.DateMeet.ToString("yyyy-MM-dd").Contains(search));
            }
            
            return query.AsEnumerable();
        }
    }
}
