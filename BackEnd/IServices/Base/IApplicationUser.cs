using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;


namespace BackEnd.IServices.Base
{
   public interface IApplicationUser
    {
        IEnumerable<ApplicationUser> GetAll();
        public IEnumerable<ApplicationUser> getAllUser();
        ApplicationUser GetById(string id);
        void Create(ApplicationUser user);
        void Update(ApplicationUser user);
        void Delete(ApplicationUser user);
    }
}
