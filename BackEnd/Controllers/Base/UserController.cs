using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.IServices.Base;
using BackEnd.Models;
using BackEnd.Service;

namespace BackEnd.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUser _user;
        public UserController(IApplicationUser user)
        {
            _user = user;
        }
        [HttpGet("Admin")]
        public ActionResult <IEnumerable<ApplicationUser>> GetAll()
        {
            return Ok(_user.GetAll());
        }
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationUser>> GetAllUser()
        {
            return Ok(_user.getAllUser());
        }

        [HttpGet("{Id}")]
        public ActionResult<ApplicationUser> getById(string Id)
        {
            ApplicationUser app = _user.GetById(Id);
            if (app != null)
            {
                return app;
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult GetById(ApplicationUser user)
        {
            _user.Create(user);
            return Ok(user);
        }
        [HttpPut("delete/{id}")]
        public IActionResult Delete(String Id)
        {
            ApplicationUser applice = _user.GetById(Id);
            if (applice != null)
            {
                applice.Deleted = true;
                _user.Update(applice);
                return Ok();
            }
            return NotFound("khong thay");
        }
        [HttpPut("{Id}")]
        public IActionResult EditApplicationUser(String Id, ApplicationUser user)
        {
            if (Id != null)
            {
                user.Id = Id;
                var usr = _user.GetById(Id);
                usr.FirstName = user.FirstName;
                usr.LastName = user.LastName;
                usr.UserName = user.UserName;
                usr.IdTest = user.IdTest;
                usr.PhoneNumber = user.PhoneNumber;
                usr.IdDepartment = user.IdDepartment;
                usr.Deleted = user.Deleted;
                _user.Update(usr);
                return Ok(user);
            }
            return NotFound("khong thay");
        }
    }

    }

