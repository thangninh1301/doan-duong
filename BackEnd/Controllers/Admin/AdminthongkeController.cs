using BackEnd.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminthongkeController : ControllerBase
    {
        private readonly IAdminthongke _adminthongke;

        public AdminthongkeController(IAdminthongke adminthongke)
        {
            _adminthongke = adminthongke;
        }
        [HttpGet("admin/{admin}")]
        public IActionResult GetById(string admin)
        {
            var apoint = _adminthongke.getApoittheoDepart(admin);
            if (apoint != null)
            {
                return Ok(apoint);
            }
            return NotFound("notfound");

        }
        [HttpGet("admintre/{admin}")]
        public IActionResult GetgetRegistheoDepart(string admin)
        {
            var apoint = _adminthongke.getRegistheoDepart(admin);
            if (apoint != null)
            {
                return Ok(apoint);
            }
            return NotFound("notfound");

        }
    }
}
