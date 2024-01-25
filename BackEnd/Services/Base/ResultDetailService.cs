using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Base
{
    public class ResultDetailService : IResultDetail
    {
        private readonly AppDBContext _context;
        public ResultDetailService(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<ResultDetail> GetAllResultDetail()
        {
            return _context.ResultDetails.ToList();
        }

        public IEnumerable<dynamic> GetResultDetailByIdResult(int idResult)
        {
            var data = _context.ResultDetails./*DefaultIfEmpty().*/Where(x => x.IdResult == idResult);
            var query = from rsDt in data
                        select new
                        {
                            IdResult = rsDt.Result.Id,
                            Diagnostic = rsDt.Result.Diagnostic,
                            TherapyRegiment = rsDt.Result.TherapyRegiment,
                            idDoctorTest=rsDt.IdDoctorTest,
                            doctorTest = rsDt.DoctorTest.LastName,
                            nameTest = rsDt.DoctorTest.Test.Name,
                        };
            return query;
                        
        }
    }
}
