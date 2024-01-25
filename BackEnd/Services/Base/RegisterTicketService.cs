
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;

namespace BackEnd.Service.Base
{
    public class RegisterTicketService : IRegisterTicket
    {
        private readonly AppDBContext _context;

        public RegisterTicketService(AppDBContext context)
        {
            _context = context;
        }
        public RegisterTicket AddRegisterTicket(RegisterTicket registerTicket)
        {
            registerTicket.Deleted = false;
            _context.RegisterTickets.Add(registerTicket);
            _context.SaveChanges();
            return registerTicket;
        }

        public void DeleteRegisterTicket(RegisterTicket registerTicket)
        {
            _context.RegisterTickets.Remove(registerTicket);
            _context.SaveChanges();
        }

        public RegisterTicket EditRegisterTicket(RegisterTicket registerTicket)
        {
            _context.RegisterTickets.Update(registerTicket);
            _context.SaveChanges();
            return registerTicket;
        }

        public IEnumerable<RegisterTicket> GetAllAdmin()
        {
            var results = _context.Results.ToList();
            var registerTickets = _context.RegisterTickets.ToList();

            return _context.RegisterTickets.ToList();
        }

        public IEnumerable<RegisterTicket> GetAllRegisterTicket()
        {
            return _context.RegisterTickets.ToList().Where(item => item.Deleted == false);
        }

        public RegisterTicket GetRegisterTicketId(int Id)
        {
            return _context.RegisterTickets.Find(Id);
        }
    }
}
