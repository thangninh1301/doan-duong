using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BackEnd.Models
{
    public partial class ResultDetail
    {
      /*  public int Id { get; set; }*/
        public int IdResult { get; set; }
        public string IdDoctorTest { get; set; }
        public DateTime? DateCreate { get; set; } 
        public DateTime? DateUpdate { get; set; }
        public string UrlFile { get; set; }
        public string Diagnostic { get; set; }
        public ResultDetail()
        {
            this.DateCreate= DateTime.Now;
        }
        public virtual Result Result { get; set; }
        public virtual ApplicationUser DoctorTest { get; set; }
    }
}
