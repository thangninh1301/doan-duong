using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.IServices.IUserServices
{
    public interface IUserRegisterTicket
    {
        public IEnumerable<dynamic> GetAllRegisterTicketById(string id);
        public IEnumerable<dynamic> GetAllApointmentByPatient(string idPatient);
        public IEnumerable<RegisterTicket> SearchUserRegisterTicket(string search, string idPatient);
    }
}
