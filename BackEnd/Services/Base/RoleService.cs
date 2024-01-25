using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;


namespace BackEnd.Service.Base
{
    public class RoleService : IRole
    {
        private readonly AppDBContext _context;
        public RoleService(AppDBContext db)
        {
            this._context = db;
        }

        public void Add(IdentityRole role)
        {
            try
            {
                var newrole = new IdentityRole();
                newrole.Name = role.Name;
                newrole.Id = role.Id;
                _context.Roles.Add(newrole);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            }
            
        }

        public void Delete(IdentityRole role)
        {
            try
            {
                var crrrole=_context.Roles.Find(role.Id);   
                _context.Roles.Remove(crrrole);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public IEnumerable<IdentityRole> getAllAdmin()
        {
            return _context.Roles.ToList();
        }

        public IEnumerable<IdentityRole> getAllUser()
        {
            return _context.Roles.ToList();
        }

        public IdentityRole getById(string id)
        {
            return _context.Roles.Find(id);
        }

        public void Update(IdentityRole role)
        {
            try
            {
                var newrole = _context.Roles.Find(role.Id);
                newrole.Name = role.Name;
                newrole.Id = role.Id;
                _context.Roles.Update(newrole);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
    }
}
