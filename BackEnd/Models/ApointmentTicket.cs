using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class ApointmentTicket
    {
        [Key]
        public int Id { get; set; }
   
        public int IdTimeMeet { get; set; }
 
        public int IdRegisterTicket { get; set; }
   
        [StringLength(450)]
        public String IdDoctor { get; set; }
 
      

        public DateTime DateMeet { get; set; }

       

        public DateTime DateCreate { get; set; }

        public ApointmentTicket()
        {
            DateCreate = DateTime.Now;
        }
      
        public int Status { get; set; }

        [StringLength(int.MaxValue)]
        public string Decription { get; set; }

        public Boolean Deleted { get; set; } = false;
        public virtual Result Result { get; set; }
        public virtual RegisterTicket registerticket { get; set; }
        /*public virtual Department Department { get; set; }*/
        public virtual TimeSlot TimeSlot { get; set; }
        public virtual ApplicationUser Doctor { get; set; }
    }
}
