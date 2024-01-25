using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.IService
{
    public interface IAdminthongke
    {
        public IEnumerable<dynamic> getApoittheoDepart(string admin);
        public IEnumerable<dynamic> getRegistheoDepart(string admin);
    }
}
