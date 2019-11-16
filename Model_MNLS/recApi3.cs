using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.recApi3
{
    public class Rootobject
    {
        public int status { get; set; }
        public string msg { get; set; }
       // public string comid { get; set; }
        public Resultinfo[] resultInfo { get; set; }
    }

    public class Resultinfo
    {
        public string pzhm { get; set; }
    }
}
