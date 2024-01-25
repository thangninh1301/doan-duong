using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.DTO;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Service
{
    public class Adminservice : IService.IAdminService
    {
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Adminservice(AppDBContext context,
                          UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //lấy ra bác sỹ đầu tiên có số phiếu hẹn ít nhất tại một khung giờ, nếu tất cả đều đạt max thì trả về null
        public Models.ApplicationUser getDoctorHaveApointMin(DateTime datemeet,int idTimeslot,int idDepart)
        {
            int max = _context.TimeSlot.Find(idTimeslot).Max;
            if(_context.ApplicationUsers.Where(x => x.IdDepartment == idDepart).FirstOrDefault() == null)
            {
                return null; 
            }
            var userapointnotcount =
                from doctor in _context.ApplicationUsers.ToList()
                where doctor.IdDepartment==idDepart
                group doctor by doctor.Id into newdoctor
                select new
                {
                    IdUser = newdoctor.Key,
                    CountTicketApoint = from doc in newdoctor


                                        join apoin in _context.ApointmentTickets/*.DefaultIfEmpty()*/.ToList()


                                         /* on apoin.IdDoctor equals doc.Id*/
                                         on doc.Id equals apoin.IdDoctor
                                        where apoin.DateMeet == datemeet && apoin.IdTimeMeet == idTimeslot
                                        select new
                                        {
                                            Id = apoin == null ? 0 : apoin.Id,
                                            Doctor = doc.FirstName
                                        }

                };
            var userapoint = userapointnotcount.Select(
                x => new
                {
                    IdUser = x.IdUser,
                    CountTicketApoint = x.CountTicketApoint.Count()
                }
                );
            var count = userapoint.Count();
            if (userapoint != null)
            {
                int CountTicketApointMin = userapoint.Min(x => x.CountTicketApoint);


                var ob = userapoint.Where(x => x.CountTicketApoint < max && x.CountTicketApoint == CountTicketApointMin).FirstOrDefault();
                if (ob != null)
                {
                    return _context.ApplicationUsers.Find(ob.IdUser);
                   

                }
            }
            
            return null;


        }
        // thêm phiếu hẹn và cập nhật phiếu đăng kí
        public int add(ApointmentTicket ob)
        {
            var regis = _context.RegisterTickets.Find(ob.IdRegisterTicket);
            
            // nếu chưa có phiếu hẹn(regis.Status == 0) thì sẽ thêm
            if (regis.Status == 0)
            {
                ApointmentTicket newapoint = new ApointmentTicket();

                newapoint.IdRegisterTicket = ob.IdRegisterTicket;
                newapoint.DateMeet = regis.DateMeet;
                newapoint.IdTimeMeet = regis.IdTimeMeet;
                newapoint.Status = ob.Status;
                newapoint.IdDoctor = ob.IdDoctor;
                newapoint.Decription = ob.Decription;
                newapoint.Deleted = ob.Deleted;
                _context.ApointmentTickets.Add(newapoint);
                regis.Status = 1;
                _context.RegisterTickets.Update(regis);
                _context.SaveChanges();

            }
            //nếu đã có phiếu hẹn(regis.Status == 1)  
            else if (regis.Status == 1)
            {
                var crrApoin = _context.ApointmentTickets.Find(ob.Id);
                //phiếu hẹn bệnh nhân chưa nhận được
                if (crrApoin.Status == 0)
                {
                    crrApoin.DateMeet = regis.DateMeet;
                    crrApoin.IdTimeMeet = regis.IdTimeMeet;
                    crrApoin.Status = ob.Status;
                    crrApoin.Decription = ob.Decription;
                    crrApoin.Deleted = ob.Deleted;
                    crrApoin.IdDoctor = ob.IdDoctor;
                    crrApoin.IdRegisterTicket = ob.IdRegisterTicket;
                    _context.ApointmentTickets.Update(crrApoin);
                    _context.SaveChanges();
                }
               
            }
            
                return ob.Id;
        }
        //lấy bác sỹ theo khoa
        public IEnumerable<dynamic> getDoctorDepartment(int IdDepart)
        {
            var Doctors = _context.ApplicationUsers.Where(x => x.IdDepartment == IdDepart);

            return Doctors;
        }
        public IEnumerable<dynamic> getAllRegisterTicket()
        {
            var apoinmenttickets = _context.ApointmentTickets.ToList();
            var registerticket = _context.RegisterTickets.Where(x => x.Deleted != true).ToList();
            var applicationUsers = _context.ApplicationUsers.ToList();
            var timeslots = _context.TimeSlot.ToList();
            var ob = from regis in registerticket
                    
                     group regis by regis.Status into newGroup
                     from grp in newGroup
                     orderby grp.Status ascending
                     join user in applicationUsers
                     on grp.IdPatient equals user.Id
                     join timeslot in timeslots
                     on grp.IdTimeMeet equals timeslot.Id

                     join apoinment in apoinmenttickets
                     on grp.Id equals apoinment.IdRegisterTicket into ap
                     from apoinment in ap.DefaultIfEmpty()


                     select new
                     {

                         Name = user.LastName,
                         bgDisease = user.BgDisease,                    
                         Symptom = grp.Symptom,
                         IdTimeMeet = grp.IdTimeMeet,
                         DesTimeSlot = timeslot.Decription,
                         DateMeet = grp.DateMeet,
                         StatusRegis = grp.Status,
                         Deleted=grp.Deleted,
                         /* IdDoctor=apoinment.IdDoctor,*/
                         IdRegis = grp.Id,
                         IdApointment = apoinment == null ? 0 : apoinment.Id,
                     };
            return ob;
        }

        //lấy Phiếu đăng kí theo ngày
        public IEnumerable<dynamic> getRegisterTicketByDate(string requestDate)
        {
            var apoinmenttickets = _context.ApointmentTickets.ToList();
            var registerticket = _context.RegisterTickets.Where(x=>x.Deleted!=true).ToList();
            var applicationUsers = _context.ApplicationUsers.ToList();
            var timeslots = _context.TimeSlot.ToList();
            var ob = from regis in registerticket
                     group regis by regis.Id into newGroup
                     from grp in newGroup
                     orderby grp.Status ascending
                     join user in applicationUsers
                     on grp.IdPatient equals user.Id
                     join timeslot in timeslots
                     on grp.IdTimeMeet equals timeslot.Id

                     join apoinment in apoinmenttickets
                     on grp.Id equals apoinment.IdRegisterTicket into ap
                     from apoinment in ap.DefaultIfEmpty()
                     orderby grp.DateMeet, grp.IdTimeMeet descending

                     where (grp.DateMeet.ToString("yyyy-MM-dd").Contains(requestDate))

                     select new
                     {

                         Name = user.LastName,
                         Symptom = grp.Symptom,
                         IdTimeMeet = grp.IdTimeMeet,
                         DesTimeSlot = timeslot.Decription,
                         DateMeet = grp.DateMeet,
                         StatusRegis = grp.Status,
                         IdRegis = grp.Id,
                         IdApointment = apoinment == null ? 0 : apoinment.Id,
                     }

                     ;
            return ob;
        }


        // lấy danh sách bác sỹ chưa sếp vào khoa
        public IEnumerable<dynamic> getDoctorNotinDepartment()
        {
            var Doctors = _context.Users.Where(x => x.IdDepartment == null)
                    ;
            var ab = from a in Doctors
                     join b in _context.UserRoles
                     on a.Id equals b.UserId

                     select new
                     {
                         UserId = a.Id,
                         Name = a.LastName,
                         IdDepartment = a.IdDepartment,
                     };
            return ab;

        }
        public IEnumerable<dynamic> GetsAllUserRoles()
        {
            var ob = from a in _context.Users
                     select new
                     {
                         UserId = a.Id,
                         UserName = a.UserName,
                         Roles = string.Join(",", _userManager.GetRolesAsync(a).Result),
                     };
            return ob;

        }
        // cập nhật role theo user
        public string AddRoleForUser(UserRoleRequest user)
        {
            string[] CrrRoles = user.CurrentRoleNames.Split(',').ToArray();

            string[] NewRoles = Array.FindAll(user.NewRoleNames.Split(','), a => a != "").ToArray();
            /* ApplicationUser usercrr =  _userManager.FindByIdAsync(user.UserId).Result;
             if(CrrRoles[0]!="")_userManager.RemoveFromRolesAsync(usercrr, CrrRoles);
             _userManager.AddToRoleAsync(usercrr, "Admin");
             if (_userManager.AddToRolesAsync(usercrr, NewRoles) != null)
             { 
                 _context.SaveChanges();
                 var a = usercrr.Id;
                 return a;  }
             return null;*/
            if (CrrRoles[0] != "")
            {
                // xóa các role cũ
                foreach (string CrrRole in CrrRoles)
                {
                    /* var ob=_context.UserRoles.Find(user.UserId, _roleManager.FindByNameAsync(CrrRole).Result.Id);
                     _context.UserRoles.Remove(ob);*/

                    var role = _context.Roles.ToList().FindAll(x => x.Name == CrrRole).ToArray();
                    var ob = _context.UserRoles.Find(user.UserId, role[0].Id);
                    _context.UserRoles.Remove(ob);
                    _context.SaveChanges();
                }

            }

            foreach (string CrrRole in NewRoles)
            {
                /*                    var role1 = await _roleManager.FindByNameAsync(CrrRole);
                 var ob = _context.UserRoles.Find(user.UserId, role[0].Id);*/
                var role = _context.Roles.ToList().FindAll(x => x.Name == CrrRole).ToArray();


                _context.UserRoles.Add(
                    //thêm role mới
                    new IdentityUserRole<string>
                    {
                        UserId = user.UserId,
                        RoleId = role[0].Id,
                    });
                _context.SaveChanges();
            }
            return user.UserId;
        }
        // lấy tất cả Bác sỹ
        public IEnumerable<dynamic> getAllDocotor()
        {
            var x = _roleManager.FindByNameAsync("Doctor").Result;
            var ab = from userrole in _context.UserRoles
                     where userrole.RoleId == "02"
                     join user in _context.ApplicationUsers.DefaultIfEmpty() on userrole.UserId equals user.Id
                     join depart in _context.Departments on user.IdDepartment equals depart.Id into newDepart
                     from a in newDepart.DefaultIfEmpty()
                    /* join test in _context.Tests.DefaultIfEmpty() on user.IdTest equals test.Id into newTest*/

                     select new
                     {
                         Id = userrole.UserId,
                         UserName = user.UserName,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         PhoneNumber = user.PhoneNumber,
                         Deleted = user.Deleted,
                         IdDepartment = a == null ? 0 : a.Id,

                         NameDepart = a == null ? "" : a.Name,
                         

                     };

            return ab;
        }
        // thống kê bác sĩ khoa xét nghiệm
        public IEnumerable<dynamic> getAllDocotorTest()
        {
            var x = _roleManager.FindByNameAsync("Doctor").Result;
            var ab = from userrole in _context.UserRoles
                     where userrole.RoleId == "04"
                     join user in _context.ApplicationUsers.DefaultIfEmpty() on userrole.UserId equals user.Id
                     join test in _context.Tests on user.IdTest equals test.Id into newTest
                     from a in newTest.DefaultIfEmpty()
                         /* join test in _context.Tests.DefaultIfEmpty() on user.IdTest equals test.Id into newTest*/

                     select new
                     {
                         Id = userrole.UserId,
                         UserName = user.UserName,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         PhoneNumber = user.PhoneNumber,
                         Deleted = user.Deleted,
                         IdTest = a == null ? 0 : a.Id,

                         NameTest = a == null ? "" : a.Name,


                     };

            return ab;
        }
        // thống kê bệnh nhân theo bác sĩ
        public IEnumerable<dynamic> GetPatient()
        {
            var data = from ap in _context.ApointmentTickets.ToList().Where(x => x.Deleted != true)
                       group ap by ap.IdDoctor into rgGroup
                       select new
                       {
                           idDoctor = rgGroup.Key,
                           nameDoctor = _userManager.FindByIdAsync(rgGroup.Key).Result.LastName,
                           apoint = (from pa in rgGroup
                                     select pa.Id).Count(),
                           patient = (from rg in rgGroup
                                      join r in _context.RegisterTickets on rg.IdRegisterTicket equals r.Id
                                      group r by r.IdPatient into z
                                      select z.Key).Count()

                       };

            return data.ToList();
        }
        //thống kê bác sỹ theo phòng ban
        public IEnumerable<dynamic> GetDoctorByDepart()
        {
            var data = from dp in _context.Departments.Where(x => x.Deleted != true)

                       select new
                       {
                           Id = dp.Id,
                           NameDepart = dp.Name,
                           NumDoctor = dp.applicationUsers.Count(),

                       };

            return data.ToList();
        }
    }
}