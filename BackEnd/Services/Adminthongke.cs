using BackEnd.Data;
using BackEnd.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Service
{
    public class Adminthongke : IAdminthongke
    {
        private readonly AppDBContext _context;
        public Adminthongke(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> getApoittheoDepart(string admin)
        {
            var Depart = from t in _context.ApointmentTickets.ToList().Where(x => x.Deleted != true)
                         group t by t.DateMeet into rgGroup
                         /*where string.CompareTo(t.DateMeet.ToString("yyyy-MM-dd"),DateTime)*/


                         select new
                         {
                             dateMeet = rgGroup.Key.ToString("dd/MM/yy"),
                             gio = from ti in rgGroup
                                   group ti by ti.IdTimeMeet into gr
                                   select new
                                   {

                                       idTimeMeet = gr.Key,
                                       decription = _context.TimeSlot.FindAsync(gr.Key).Result.Decription,
                                       phieu = (from c in gr
                                                select c.Id).Count()
                                   }
              

        };
            return Depart.ToList();           
        }

        public IEnumerable<dynamic> getRegistheoDepart(string admin)
        {
            var data = from t in _context.RegisterTickets.ToList().Where(x => x.Deleted != true)
                         group t by t.DateMeet into rgGroup
       
                         select new
                         {
                             dateMeet = rgGroup.Key.ToString("dd/MM/yy"),
                             gio = from ti in rgGroup
                                   group ti by ti.IdTimeMeet into gr
                                   select new
                                   {

                                       idTimeMeet = gr.Key,
                                       decription = _context.TimeSlot.FindAsync(gr.Key).Result.Decription,
                                       phieu = (from c in gr
                                                select c.Id).Count()
                                   }
                                   };
            return data.ToList();
        }
    }
}
