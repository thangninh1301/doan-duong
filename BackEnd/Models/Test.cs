using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.Models
{
    public partial class Test
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public DateTime? Datecreate { get; set; }
        public Test()
        {
            Datecreate = DateTime.Now;
        }

        public virtual ICollection<ApplicationUser> DoctorTests { get; set; }
       
    }
}
