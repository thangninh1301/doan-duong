using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BackEnd.Models
{
    public partial class RegisterTicket
    {
        public int Id { get; set; }
        public string IdPatient { get; set; }
        public string Symptom { get; set; }
      
      
        public DateTime DateMeet { get; set; }
        public int IdTimeMeet { get; set; }

       
      
        public DateTime DateCreate { get; set; }
        public RegisterTicket()
        {
            DateCreate = DateTime.Now;
        }
        public bool? Deleted { get; set; }
        public int Status { get; set; }
        public virtual ApointmentTicket apointmentTicket { get; set; }
        public virtual TimeSlot Timeslot { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
