using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BackEnd.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
      
        public DateTime Datecreate { get; set; }
        public Department()
        {
            Datecreate = DateTime.Now;
        }
        public bool Deleted { get; set; }
      /*  public virtual List<ApointmentTicket> apointmentTickets { get; set; }*/
        public virtual List<ApplicationUser> applicationUsers { get; set; }
    }
}
