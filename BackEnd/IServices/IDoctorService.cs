using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.IService
{
    public interface IDoctorService
    {
        public IEnumerable<dynamic> GetAllApointmentByDoctor(string doctorId);
        public IEnumerable<dynamic> GetAllPatient(string doctor);
        public int add(Result rs);

    }
}
