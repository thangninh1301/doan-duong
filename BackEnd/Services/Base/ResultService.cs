

//using testUseridentity.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using testUseridentity.Data;
//using testUseridentity.IService;

//namespace testUseridentity.Service
//{
//    public class ResultService : IResult
//    {
//        private readonly AppDBContext _context;
//        public ResultService(AppDBContext context)
//        {
//            _context = context;
//        }
//        public Result AddResult(Result result)
//        {
//            _context.Results.Add(result);
//            _context.SaveChanges();
//            return result;
//        }

//        public void DeleteResult(Result result)
//        {
//            _context.Results.Remove(result);
//            _context.SaveChanges();
//        }

//        public Result EditResult(Result result)
//        {
//            _context.Results.Update(result);
//            _context.SaveChanges();
//            return result;
//        }

//        public IEnumerable<Result> GetAllAdmin()
//        {
//            return _context.Results.ToList();
//        }

//        public IEnumerable<Result> GetAllResult()
//        {
//            return _context.Results.ToList().Where(item => item.Deleted == false);
//        }

//        public Result GetResultId(int Id)
//        {
//        var id = _context.Results.Find(Id);
//        return id;
//        }
//    }
//}
