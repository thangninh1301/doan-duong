using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BackEnd.Models;
using BackEnd.IServices.Base;
using System.Threading.Tasks;

namespace BackEnd.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _department;
        public DepartmentController(IDepartment db)
        {
            _department = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>>  getAll()
        {
            return Ok(_department.getAllUser());
        }
        [HttpGet("Admin")]
        public ActionResult<IEnumerable<Department>> getAllAdmin()
        {
            return Ok(_department.getAllAdmin());
        }
        [HttpGet("{Id}")]
        public ActionResult<Department> getById(int Id)
        {
            Department db = _department.getById(Id);
            if (db != null) {
                return db;
            }return NotFound();
        }
        [HttpPost]
        public ActionResult<Department> Create(Department db)
        {
            if (db != null)
            {
                _department.Add(db);
                return Ok(db);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update(Department db) {
           
            Department db2 = _department.getById(db.Id);
            if ( db2!= null)
            {
                db2.Datecreate = db.Datecreate;
                db2.Decription = db.Decription;
                db2.Deleted = db.Deleted;
                db2.Name = db.Name;
                _department.Update(db2);
                return NoContent();
            }
            return BadRequest();

        }
        [HttpPut("{Id}")]
        public IActionResult Delete(int Id)
        {
           
            Department db2 = _department.getById(Id);
            if (db2 != null)
            {  
                db2.Deleted = true;
                _department.Update(db2);
                return NoContent();
            }
            return BadRequest();
        }
    }
}
