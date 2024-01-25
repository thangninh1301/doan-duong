using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;


namespace BackEnd.IServices.Base
{
    public interface IUserRole
    {
        public IEnumerable<dynamic> getAllAdmin();
        public IdentityUserRole<string> getById(string UserId,string RoleId);
        public void Add(IdentityUserRole<string> userRole);
        public void Delete(IdentityUserRole<string> userRole);
    }
}
