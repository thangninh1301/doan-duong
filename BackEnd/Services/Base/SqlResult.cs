

using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;

namespace BackEnd.Service.Base
{
    public class SqlResult : IResult
    {
        private readonly AppDBContext _context;
        public SqlResult(AppDBContext context)
        {
            _context = context;
        }
        public Result AddResult(Result result)
        {
            _context.Results.Add(result);
            _context.SaveChanges();
            return result;
        }

        public void DeleteResult(Result result)
        {
            _context.Results.Remove(result);
            _context.SaveChanges();
        }

        public Result EditResult(Result result)
        {
            _context.Results.Update(result);
            _context.SaveChanges();
            return result;
        }

        public IEnumerable<Result> GetAllAdmin()
        {
            return _context.Results;
        }

        public IEnumerable<Result> GetAllResult()
        {
            return _context.Results.ToList().Where(item => item.Deleted == false);
        }

        public Result GetResultId(int Id)
        {
        var rs = _context.Results.Where(x=>x.Id==Id)
                .Select(db=> new Models.Result{
                    Id=db.Id,
                    Diagnostic=db.Diagnostic,
                    TherapyRegiment=db.TherapyRegiment,
                    Status=db.Status,
                    ResultDetails2= (List<ResultDetail>)(from rsdt in db.ResultDetails2
                                   select new Models.ResultDetail
                                   {
                                       Diagnostic = rsdt.Diagnostic,
                                       DateCreate = rsdt.DateCreate,
                                       DateUpdate = rsdt.DateUpdate,
                                       UrlFile = rsdt.UrlFile,
                                       DoctorTest= new Models.ApplicationUser { 
                                        LastName=rsdt.DoctorTest.LastName,
                                        Id=rsdt.DoctorTest.Id,
                                        Test=rsdt.DoctorTest.Test
                                       },
                                       

                                   })
                });
           
            return rs.FirstOrDefault();
        }
    }
}
