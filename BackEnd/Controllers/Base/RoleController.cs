using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.IServices.Base;
using BackEnd.Models;

namespace BackEnd.Api
{
    [Route("api/[controller]")]
    [ApiController]
    /*[Authorize]*/
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        public RoleController(IRole role)
        {
            _role = role;
        }
        [HttpGet]
        public ActionResult<IEnumerable<IdentityRole>> GetAll()
        {
            return Ok(_role.getAllUser());
        }
         [HttpGet("Admin")]
         public ActionResult<IEnumerable<IdentityRole>> getAllAdmin()
         {
             return Ok(_role.getAllAdmin());
         }
         [HttpGet("{Id}")]
         public ActionResult<IdentityRole> getById(string Id)
         {
            IdentityRole app = _role.getById(Id);
             if (app != null)
             {
                 return app;
             }
             return NotFound();
         }
         [HttpPost]
         public IActionResult GetById(IdentityRole role)
         {
             _role.Add(role);
             return Ok(role);
         }
         [HttpDelete("{Id}")]
         public IActionResult Delete(string Id)
         {
            IdentityRole applice = _role.getById(Id);
             if (applice != null)
             {
                 _role.Delete(applice);
                 return Ok();
             }
             return NotFound("khong thay");
         }
         [HttpPut("{Id}")]
         public IActionResult EditIdentityRole(string Id, IdentityRole role)
         {
            
             if (_role.getById(Id)!=null)
             {

                 var usr = _role.getById(Id);
                 usr.Name = role.Name;
                 usr.NormalizedName = role.NormalizedName;
                _role.Update(usr);
                 return Ok(role);
             }
             return NotFound("khong thay");
         }
    }
}
