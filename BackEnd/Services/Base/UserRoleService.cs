using BackEnd.Data;
using BackEnd.IService;
using BackEnd.IServices.Base;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Service.Base
{
    public class UserRoleService : IUserRole
    {
        private readonly AppDBContext _context;
        public UserRoleService(AppDBContext db)
        {
            this._context = db;
        }

        public void Add(IdentityUserRole<string> userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public void Delete(IdentityUserRole<string> userRole)
        {
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        public IEnumerable<dynamic> getAllAdmin()
        {
            var data = from r in _context.UserRoles.ToList()
                       select new
                       {
                           UserId = r.UserId,
                           UserName = _context.ApplicationUsers.Find(r.UserId).UserName,
                           RoleId = r.RoleId,
                           RoleName = _context.Roles.Find(r.RoleId).Name

                       };
            return data.AsEnumerable();
        }

        public IdentityUserRole<string> getById(string UserId,string RoleId)
        {
            return _context.UserRoles.Find(UserId ,RoleId);
        }
    }
}
