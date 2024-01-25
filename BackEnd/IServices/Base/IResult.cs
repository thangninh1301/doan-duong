using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackEnd.IServices.Base
{
    public interface IResult
    {
        IEnumerable<Result> GetAllAdmin();
        IEnumerable<Result> GetAllResult();
        Result GetResultId(int Id);
        Result AddResult(Result result);
        Result EditResult(Result result);
        void DeleteResult(Result result);
    }
}
