using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.IServices.IUserServices
{
    public interface IUserApointmentTicket
    {
        IEnumerable<dynamic> GetAllApointmentTicketById(string id);

    }
}
