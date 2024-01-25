using BackEnd.IServices.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultDetailController : ControllerBase
    {
        private readonly IResultDetail _resultDetail;
        public ResultDetailController(IResultDetail resultDetail)
        {
            _resultDetail = resultDetail;
        }
        [HttpGet]
        public IActionResult GetAllResultDetail()
        {
            return Ok(_resultDetail.GetAllResultDetail());
        }
        [HttpGet("{idResult}")]
        public IActionResult GetResultDetailByIdResult(int idResult)
        {
            var db = _resultDetail.GetResultDetailByIdResult(idResult);
            if (db != null)
            {
                return Ok(db);
            }
            return NotFound("Not Found");
        }
    }
}
