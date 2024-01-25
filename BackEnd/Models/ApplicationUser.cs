using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public int? IdTest { get; set; }
        public int? IdDepartment { get; set; }
        public string Address { get; set; }
        public string BgDisease { get; set; }

        public virtual Department department { get; set; }
        public virtual IList<RegisterTicket>  registerTickets { get; set; }
        public virtual IList<ApointmentTicket> ApointmentTickets { get; set; }
        public virtual Test Test { get; set; }
        public virtual IList<ResultDetail> ResultDetails1 { get; set; }

        public Boolean? Deleted { get; set; }
    }
    
   
}
