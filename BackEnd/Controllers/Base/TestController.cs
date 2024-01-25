
﻿using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITest _test;


        public TestController(ITest test)
        {
            _test = test;

        }
        [HttpGet("Admin")]
        public ActionResult<IEnumerable<Test>> getAllAdmin()
        {
            return Ok(_test.getAllAdmin());
        }
        [HttpGet("{Id}")]
        public ActionResult<Test> getById(int Id)
        {
            Test app = _test.getById(Id);
            if (app != null)
            {
                return app;
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult GetById(Test test)
        {
            _test.Add(test);
            return Ok(test);
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Test applice = _test.getById(Id);
            if (applice != null)
            {
                _test.Delete(applice);
                return Ok();
            }
            return NotFound("khong thay");
        }
        [HttpPut("{Id}")]
        public IActionResult EditIdentityRole(int Id, Test test)
        {

            if (_test.getById(Id) != null)
            {

                var usr = _test.getById(Id);
                usr.Name = test.Name;
                usr.Desciption = test.Desciption;
                _test.Update(usr);
                return Ok(test);
            }
            return NotFound("khong thay");
        }

    }
}
