using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.IServices.Base
{
    public interface ITest
    {

        public IEnumerable<Test> getAllAdmin();
        public Test getById(int id);
        public void Add(Test test);
        public void Update(Test test);
        public void Delete(Test test);

    }
}
