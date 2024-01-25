using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;

namespace BackEnd.Service.Base
{
    public class ApplicationUserService : IApplicationUser        
    {
        private readonly AppDBContext _context;
        public ApplicationUserService(AppDBContext context)
        {
            this._context = context;
        }

        public void Create(ApplicationUser user)
        {
             _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(ApplicationUser user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users.ToList();
        }
        public IEnumerable<ApplicationUser> getAllUser()
        {
            return _context.Users.ToList().Where(item => item.Deleted != true);

        }
         public ApplicationUser GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public void Update(ApplicationUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
