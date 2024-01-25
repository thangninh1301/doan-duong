using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.IServices.Base
{
    public interface IRegisterTicketAdmin
    {
        IEnumerable<RegisterTicket> GetAllAdmin();
        IEnumerable<RegisterTicket> GetAllRegisterTicket();
        RegisterTicket GetRegisterTicketId(int Id);
        RegisterTicket AddRegisterTicket(RegisterTicket registerTicket);
        RegisterTicket EditRegisterTicket(RegisterTicket registerTicket);
        void DeleteRegisterTicket(RegisterTicket registerTicket);
    }
}
