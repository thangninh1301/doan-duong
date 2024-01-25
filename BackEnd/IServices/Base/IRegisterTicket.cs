using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.IServices.Base
{
    interface IRegisterTicket
    {
        IEnumerable<RegisterTicket> GetAllAdmin();
        IEnumerable<RegisterTicket> GetAllRegisterTicket();
        RegisterTicket GetRegisterTicketId(int Id);
        RegisterTicket AddRegisterTicket(RegisterTicket registerTicket);
        RegisterTicket EditRegisterTicket(RegisterTicket registerTicket);
        void DeleteRegisterTicket(RegisterTicket registerTicket);
    }
}
