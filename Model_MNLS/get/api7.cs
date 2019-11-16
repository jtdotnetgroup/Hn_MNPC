using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.api7
{


    public class Rootobject
    {
        public string action { get; set; }
        public string token { get; set; }
        public int comid { get; set; }
        public string pzhm { get; set; }
        public string khhm { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }

}
