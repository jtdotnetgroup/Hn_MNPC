using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.api6
{


    public class Rootobject
    {
        public string action { get; set; }
        public string token { get; set; }
        public int comid { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string pzhm { get; set; }
        public string rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string pzlb { get; set; }
        public int cplb { get; set; }
        public string pjhm { get; set; }
        public string zdr { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public int cpdj { get; set; }
        public string cpsh { get; set; }
        public string cpcm { get; set; }
        public string package { get; set; }
        public string dw { get; set; }
        public int ks { get; set; }
        public int sl { get; set; }
        public string bz { get; set; }
        public string gg { get; set; }
        public string ggs { get; set; }
        public string pjhm1 { get; set; }
        public string pjhm2 { get; set; }
        public string telphone { get; set; }
        public string carno { get; set; }
        public string jsdz { get; set; }
        public string jsr { get; set; }
        public string pjhm3 { get; set; }
        public string ysfs { get; set; }
        public string jsfs { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }



}
