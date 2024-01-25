using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.IServices.Base
{
    public interface IRole
    {
        public IEnumerable<IdentityRole> getAllAdmin();
        public IEnumerable<IdentityRole> getAllUser();

        public IdentityRole getById(string id);
        public void Add(IdentityRole role);
        public void Update(IdentityRole role);
        public void Delete(IdentityRole role);
    }
}
