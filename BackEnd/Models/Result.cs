using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.Models
{
    public partial class Result
    {
        public int Id { get; set; }
        public int? IdApointmentTicket { get; set; }
      
        public string Diagnostic { get; set; }
        public string TherapyRegiment { get; set; }
        public DateTime? DateCreate { get; set; }
        public int Status { get; set; }
        public Result()
        {
            DateCreate = DateTime.Now;
        }
        public bool? Deleted { get; set; }
        public ApointmentTicket ApointmentTicket { get; set; }
        public List<ResultDetail> ResultDetails2 { get; set; }

    }
}
