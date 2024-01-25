using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.IServices.Base
{
    public interface IApointmentTicket
    {
        IEnumerable<ApointmentTicket> GetAllUser();
        IEnumerable<dynamic> GetAllAdmin();
        ApointmentTicket GetById(int id);
        void Create(ApointmentTicket db);
        void Update(ApointmentTicket db);
        void Delete(ApointmentTicket db);
    }
}
