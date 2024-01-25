using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.IServices.Base
{
    public interface IDepartment
    {
        public IEnumerable<Department> getAllAdmin();
        public IEnumerable<Department> getAllUser();
        public Department getById(int id);
        public void Add(Department department);
        public void Update(Department department);
        public void Delete(Department department);
    }
}
