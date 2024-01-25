using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.IServices.Base
{
    public interface ITimeSlot
    {
        public IEnumerable<TimeSlot> getAllAdmin();
        public IEnumerable<TimeSlot> getAllUser();
        public TimeSlot getById(int id);
        public void Add(TimeSlot timeslot);
        public void Update(TimeSlot timeslot);
        public void Delete(TimeSlot timeslot);
    }
}
