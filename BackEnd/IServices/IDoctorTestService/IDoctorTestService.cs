using System.Collections.Generic;

namespace BackEnd.IServices.IDoctorTestService
{
    public interface IDoctorTestService
    {
        public IEnumerable<Models.Test> GetTests(string IdDoctorTest);
        public IEnumerable<dynamic> GetAllTestregister(string doctorTestId);
        public Models.ResultDetail UpdateResultDetail(Models.ResultDetail resultDetail);
        public bool DeleteResultDetail(int IdResult,string IdDoctorTest);
        public IEnumerable<Models.ApplicationUser> getDoctorByIdTest(int IdTest);
        /*public bool Upfiledetail();*/
    }
}
