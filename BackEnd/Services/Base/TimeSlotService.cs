using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.IServices.Base;
using BackEnd.Models;

namespace BackEnd.Service.Base
{
    public class TimeSlotService : ITimeSlot
    {
        private readonly AppDBContext _context;
        public TimeSlotService(AppDBContext db)
        {
            this._context = db;
        }

        public void Add(TimeSlot timeslot)
        {
            _context.TimeSlot.Add(timeslot);
            _context.SaveChanges();
        }

        public void Delete(TimeSlot timeslot)
        {
            _context.TimeSlot.Remove(timeslot);
            _context.SaveChanges();
        }

        public IEnumerable<TimeSlot> getAllAdmin()
        {
             return _context.TimeSlot.ToList();
        }

        public IEnumerable<TimeSlot> getAllUser()
        {
            return _context.TimeSlot.ToList().Where(item=>item.Deleted==false);
        }

        public TimeSlot getById(int id)
        {
            var a = _context.TimeSlot.Find(id);
            return a;
        }

        public void Update(TimeSlot timeslot)
        {
            _context.TimeSlot.Update(timeslot);
            _context.SaveChanges();
        }
    }
}
