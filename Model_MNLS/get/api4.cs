using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.api4
{
    public class Rootobject
    {
        public string action { get; set; }
        public string token { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public string sourceno { get; set; }
        public string rq1 { get; set; }
        public string rq2 { get; set; }
        public string pzhm { get; set; }
    }


}
