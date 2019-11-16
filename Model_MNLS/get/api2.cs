using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.api2
{

    public class Rootobject
    {
        public string action { get; set; }
        public string token { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }

        public string comid { get; set; }
    }

}
