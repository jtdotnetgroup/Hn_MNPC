using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.recToken
{
    public class Rootobject
    {
        public int status { get; set; }
        public string msg { get; set; }
        //public int comid { get; set; }
        public string token { get; set; }
        public int limit_time { get; set; }
        public Tokeninfo tokenInfo { get; set; }
    }

    public class Tokeninfo
    {
        public string token { get; set; }
        public int limit_time { get; set; }
        public string starDate { get; set; }
        public string endDate { get; set; }
        public int residueNum { get; set; }
        public int comid { get; set; }
    }
}
