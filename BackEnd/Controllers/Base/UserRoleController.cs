using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;

namespace BackEnd.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRole _urole;
        public UserRoleController(IUserRole urole)
        {
            _urole = urole;
        }
        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> GetAll()
        {
            return Ok(_urole.getAllAdmin());
        }
        [HttpPost]
        public IActionResult GetById(IdentityUserRole<string> role)
        {
            _urole.Add(role);
            return Ok(role);
        }
        [HttpDelete]
        public IActionResult Delete(string UserId,string RoleId)
        {
            IdentityUserRole<string> db = _urole.getById(UserId,RoleId);
            if (_urole.getById(UserId,RoleId) != null)
            {
                _urole.Delete(_urole.getById(UserId,RoleId));
                return Ok();
            }
            return NotFound("khong thay");
        }
    }
}
