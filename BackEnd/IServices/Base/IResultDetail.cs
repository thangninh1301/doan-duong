using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.IServices.Base
{
    public interface IResultDetail
    {
        IEnumerable<ResultDetail> GetAllResultDetail();
        IEnumerable<dynamic> GetResultDetailByIdResult(int idResult);
    }
}
