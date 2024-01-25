using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;

namespace BackEnd.Service.Base
{
    public class DepartmentService:IDepartment
    {
        private readonly AppDBContext _context;
        public DepartmentService(AppDBContext db)
        {
            this._context = db;
        }

        public void Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }

        public IEnumerable<Department> getAllUser()
        {
            return _context.Departments.ToList().Where(item=>item.Deleted==false);
            
        }
        public IEnumerable<Department> getAllAdmin()
        {
            return _context.Departments.ToList();

        }
        public Department getById(int id)
        {
            var a= _context.Departments.Find(id);
            if (a.Deleted == true) return null;
            return a;
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }
    }
}
