using BackEnd.Data;
using BackEnd.IService;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DoctorService(AppDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // danh sách bệnh nhân theo bac si
        public IEnumerable<dynamic> GetAllPatient(string doctor)
        {
            /*var b = from t in _context.ApointmentTickets.Where(x => x.IdDoctor == doctor)
                    .AsEnumerable()
                    join rs in _context.Results on t.Id equals rs.IdApointmentTicket into gr
                    from rs in gr.DefaultIfEmpty()

                    join c in _context.RegisterTickets
                    .AsEnumerable()
                    on t.IdRegisterTicket equals c.Id
                    group c by c.IdPatient into q
                    select new
                    {
                        patient = q.Key,
                        userNumber = _context.ApplicationUsers.Find(q.Key).PhoneNumber,
                        FirstName = _context.ApplicationUsers.Find(q.Key).FirstName,

                        name = _userManager.FindByIdAsync(q.Key).Result.LastName,
                        register = from rg in q
                                   .AsEnumerable()
                                   select new
                                   {
                                       idApoint = rg.apointmentTicket?.Id,
                                       symptom = rg.Symptom,
                                       dateMeetApoint = rg.DateMeet,
                                       doctor1 = rg.apointmentTicket?.Doctor?.LastName,
                                       statusApoint = rg.apointmentTicket?.Status,
                                      *//* timeMeet = rg.apointmentTicket?.TimeSlot?.Decription,*//*
                                       ResultDiagnostic = rg.apointmentTicket.Result != null ? rg.apointmentTicket.Result.Diagnostic : "",
                                       ResultThera = rg.apointmentTicket.Result != null ? rg.apointmentTicket.Result.TherapyRegiment : "",
                                       idResult = rg.apointmentTicket.Result == null ? 0: rg.apointmentTicket.Result.Id
                                   }
                    };
            return b.ToList();*/
            var ob = from rg in _context.RegisterTickets.AsEnumerable()
                     join ap in _context.ApointmentTickets.AsEnumerable()
                     on rg.Id equals ap.IdRegisterTicket
                     where ap.IdDoctor == doctor
                     group rg by rg.IdPatient into newrg
                     select new 
                     {
                         patient = newrg.Key,
                         lastName = _context.ApplicationUsers.Find(newrg.Key).LastName,
                         userNumber = _context.ApplicationUsers.Find(newrg.Key).PhoneNumber,
                         FirstName = _context.ApplicationUsers.Find(newrg.Key).FirstName,
                         Regis = from newrgingr in newrg
                                 join ap in _context.ApointmentTickets.AsEnumerable()
                                 on newrgingr.Id equals ap.IdRegisterTicket
                                 join rs in _context.Results.AsEnumerable()
                                 on ap.Id equals rs.IdApointmentTicket

                                 join ts in _context.TimeSlot.AsEnumerable()
                                 on newrgingr.IdTimeMeet equals ts.Id
                                 select new Models.RegisterTicket
                                 {
                                     Id= newrgingr.Id,
                                     Symptom = newrgingr.Symptom,
                                     apointmentTicket = new Models.ApointmentTicket {
                                        Id = ap.Id,
                                        Status=ap.Status,
                                        
                                        Result = new Models.Result
                                        {
                                            Id = ap.Result.Id
                                        },
                                        Doctor = new Models.ApplicationUser
                                        {
                                            LastName = ap.Doctor.LastName,
                                           
                                        }
                                     },
                                     Timeslot = new Models.TimeSlot
                                     {
                                         Decription = ts.Decription,
                                     }
                                 },
                        

                     };
                

            return ob.ToList();
        }

        //danh sách phiếu hen theo bác sĩ
        public IEnumerable<dynamic> GetAllApointmentByDoctor(string doctorId)
        {
            var apoint = _context.ApointmentTickets.Where(ap => ap.IdDoctor == doctorId && ap.Deleted != true && ap.Status > 1);
            var query = from ap in apoint
                        orderby ap.Status ascending

                        select new
                        {
                            idApoint = ap.Id,
                            patient = ap.registerticket.User.LastName,
                            timeMeetApoint = ap.TimeSlot.Decription,
                            department = ap.Doctor.department.Name,
                            dateMeetApoint = ap.DateMeet.ToString("dd/MM/yy"),
                            dateCreateApoint = ap.DateCreate.ToString("dd/MM/yy"),
                            decription = ap.Decription,
                            doctor = ap.Doctor.LastName,
                            statusApoint = ap.Status,
                            deleteApoint = ap.Deleted,
                            symptom = ap.registerticket.Symptom,
                            bgDisease = ap.registerticket.User.BgDisease,

                            result = ap.Result == null ? null : ap.Result,

                            diagnostic = ap.Result == null ? "" : ap.Result.Diagnostic,
                            therapyRegiment = ap.Result == null ? "" : ap.Result.TherapyRegiment,
                            dateCreateResult = ap.Result != null ? ap.Result.DateCreate : DateTime.Now,
                            statusResult = ap.Result == null ? 5 : ap.Result.Status,


                        };
            return query;
        }

        // thêm kết quả và cập nhật phiếu hẹn
        public int add(Result rs)
        {
            var ckeck = _context.Results.Find(rs.Id);
            var id = 0;
            var apoint = _context.ApointmentTickets.Find(rs.IdApointmentTicket);
            if (ckeck == null)
            {
                Result newResult = new Result();
                newResult.IdApointmentTicket = rs.IdApointmentTicket;
                newResult.Diagnostic = rs.Diagnostic;
                newResult.TherapyRegiment = rs.TherapyRegiment;
                newResult.DateCreate = rs.DateCreate;
                newResult.Deleted = rs.Deleted;
                newResult.Status = rs.Status;
                _context.Results.Add(newResult);

                apoint.Status = 2;
                _context.ApointmentTickets.Update(apoint);
                _context.SaveChanges();
                id = newResult.Id;
                foreach (var item in rs.ResultDetails2)
                {
                   /* var resultDetail = _context.ResultDetails.FirstOrDefault(x=>x.IdResult==item.IdResult && x.IdDoctorTest == item.IdDoctorTest);
                    if (resultDetail == null)*/ {
                        ResultDetail newDetail = new ResultDetail();
                        newDetail.IdResult = id;
                        newDetail.IdDoctorTest = item.IdDoctorTest;
                        newDetail.DateUpdate = null;
                        newDetail.UrlFile = "";
                        newDetail.Diagnostic = "";
                        _context.ResultDetails.Add(newDetail);
                    }                   
                }
                _context.SaveChanges();
            }
            else
            {
                id = rs.Id;
                var crrresult = _context.Results.Find(rs.Id);
                crrresult.TherapyRegiment = rs.TherapyRegiment;
                crrresult.Diagnostic=rs.Diagnostic;
                crrresult.Status = rs.Status;
                _context.Results.Update(crrresult);
                _context.SaveChanges();
                foreach (var item1 in rs.ResultDetails2)
                {
                    var resultDetail = _context.ResultDetails.Find(id,item1.IdDoctorTest);
                    if (resultDetail == null)
                    {
                        ResultDetail newDetail = new ResultDetail();
                        newDetail.IdResult = id;
                        newDetail.IdDoctorTest = item1.IdDoctorTest;

                        newDetail.DateUpdate = null;
                        newDetail.UrlFile = "";
                        newDetail.Diagnostic = "";
                        _context.ResultDetails.Add(newDetail);
                    }
                }
                _context.SaveChanges();
            }
            return rs.Id;
        }
        //thêm kết quả và kết quả deltail


    }
}
