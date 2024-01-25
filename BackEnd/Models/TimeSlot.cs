using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }
        [StringLength(int.MaxValue)]
        public string Decription { get; set; }


        public int Max { get; set; }
        public DateTime DateCreate { get; set; }
        public TimeSlot()
        {
            DateCreate = DateTime.Now;
        }
        public Boolean Deleted { get; set; }
        
        public virtual List<RegisterTicket> RegisterTickets { get; set; }
        public virtual List<ApointmentTicket> ApointmentTickets { get; set; }
    }
}