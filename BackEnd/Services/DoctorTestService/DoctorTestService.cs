using BackEnd.Data;
using BackEnd.IServices.IDoctorTestService;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace BackEnd.Services.DoctorTestService
{
    public class DoctorTestService : IDoctorTestService
    {
        private readonly AppDBContext _context;
        

        public DoctorTestService(AppDBContext context)
        {
            _context = context;
        }

        public bool DeleteResultDetail(int IdResult, string IdDoctorTest)
        {
            var crrresultdetail = _context.ResultDetails.Find(IdResult, IdDoctorTest);
            if (crrresultdetail != null)
            {             
                _context.ResultDetails.Remove(crrresultdetail);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<dynamic> GetAllTestregister(string doctorTestId)
        {
            var test = _context.ResultDetails.Where(rsd => rsd.IdDoctorTest == doctorTestId);
            var query = from rsd in test
                        orderby rsd.IdResult ascending
                        select new
                        {
                            idResult =  rsd.IdResult,
                            idDoctorTest = rsd.IdDoctorTest,
                            nameDoctorTest = rsd.DoctorTest == null ? "" : rsd.DoctorTest.LastName,
                            datecreate = rsd.DateCreate,
                            dateUpdate = rsd.DateUpdate,
                            diagnostic = rsd.Diagnostic,
                            idTest = rsd.DoctorTest == null ? 0 : rsd.DoctorTest.IdTest,
                            nameTest = rsd.DoctorTest != null ? rsd.DoctorTest.Test.Name : "",
                            IdPatient = rsd.Result == null ? "" : rsd.Result.ApointmentTicket == null ? "" : rsd.Result.ApointmentTicket.registerticket == null ? "" : rsd.Result.ApointmentTicket.registerticket.IdPatient,
                            NamePati = rsd.Result == null ? "" : rsd.Result.ApointmentTicket == null ? "" : rsd.Result.ApointmentTicket.registerticket == null ? "" : rsd.Result.ApointmentTicket.registerticket.User == null ? "" : rsd.Result.ApointmentTicket.registerticket.User.LastName,
                            Email = rsd.Result == null ? "" : rsd.Result.ApointmentTicket == null ? "" : rsd.Result.ApointmentTicket.registerticket == null ? "" : rsd.Result.ApointmentTicket.registerticket.User == null ? "" : rsd.Result.ApointmentTicket.registerticket.User.Email,
                            Phone = rsd.Result == null ? "" : rsd.Result.ApointmentTicket == null ? "" : rsd.Result.ApointmentTicket.registerticket == null ? "" : rsd.Result.ApointmentTicket.registerticket.User == null ? "" : rsd.Result.ApointmentTicket.registerticket.User.PhoneNumber,
                            Url=rsd.UrlFile,
                        };
            return query;
        }

        public IEnumerable<Test> GetTests(string IdDoctorTest)
        {

            var tets= from T in _context.Tests.ToList()
                      where _context.ApplicationUsers.Find(IdDoctorTest).IdTest==T.Id
                      select T;
            return tets.AsEnumerable();
        }

        public ResultDetail UpdateResultDetail(ResultDetail resultDetail)
        {
            var crrresultdetail = _context.ResultDetails.Find(resultDetail.IdResult,resultDetail.IdDoctorTest);
            if (crrresultdetail != null)
            {
                crrresultdetail.UrlFile = resultDetail.UrlFile;
                _context.ResultDetails.Update(crrresultdetail);
                _context.SaveChanges();
                return crrresultdetail;
            }
            return null;
        }

        //get doctorby id test
        public IEnumerable<Models.ApplicationUser> getDoctorByIdTest(int IdTest) {

            return _context.ApplicationUsers.Where(x => x.IdTest == IdTest);
        }

       /* public IEnumerable<ResultDetail> Upfiledetail(IFormFile file)
        {*/
          /*  try
            {
*//*                var file = Request.Form.Files[0];
 *                
*/             
               /* var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }*/
                  /*  return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }*/
        /*}*/
    }
}
