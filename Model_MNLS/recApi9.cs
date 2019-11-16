using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.recApi9
{


    public class Rootobject
    {
        public int status { get; set; }
        public string msg { get; set; }
        public int comid { get; set; }
        public int totalCount { get; set; }
        public Resultinfo[] resultInfo { get; set; }
    }

    public class Resultinfo
    {
        public int autoid { get; set; }
        public string pzhm { get; set; }
        public string rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string dw { get; set; }
        public int ks { get; set; }
        public int sl { get; set; }
        public int dj { get; set; }
        public int je { get; set; }
        public int DB { get; set; }
    }



}
