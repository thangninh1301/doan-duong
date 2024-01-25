using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Base
{
    public class TestService: ITest

    {
        private readonly AppDBContext _context;
        public TestService(AppDBContext context)
        {
            _context = context;
        }

        public void Add(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
            
        }

        public void Delete(Test test)
        {
            _context.Tests.Remove(test);
            _context.SaveChanges();
        }

        public IEnumerable<Test> getAllAdmin()

        {
            return _context.Tests.ToList();
        }

        public Test getById(int id)
        {
            return _context.Tests.Find(id);
            
        }

        public void Update(Test test)
        {
            _context.Tests.Update(test);
            _context.SaveChanges();

        }
    }
}
