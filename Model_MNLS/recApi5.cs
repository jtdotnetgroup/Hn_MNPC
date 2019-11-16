using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.recApi5
{
    public class Rootobject
    {
        public int status { get; set; }
        public string msg { get; set; }
      //  public int comid { get; set; }
        public int db { get; set; }
        public int totalCount { get; set; }
        public Resultinfo[] resultInfo { get; set; }
    }

    public class Resultinfo
    {
        public string autoid { get; set; }
        public string pzhm { get; set; }
        public string totalpzhm { get; set; }
        public string rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string pjhm { get; set; }
        public string zdr { get; set; }
        public string shr { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string cpsh { get; set; }
        public string cpcm { get; set; }
        public string package { get; set; }
        public string dw { get; set; }
        public int ks { get; set; }
        public string sl { get; set; }
        public string bz { get; set; }
        public float gg { get; set; }
        public float ggs { get; set; }
        public string pjhm2 { get; set; }
        public string telphone { get; set; }
        public string carno { get; set; }
        public string membz1 { get; set; }
        public string pzlb { get; set; }
    }

}
