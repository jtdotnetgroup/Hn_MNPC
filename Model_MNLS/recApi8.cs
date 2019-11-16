using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.recApi8
{

    public class Rootobject
    {
        public int status { get; set; }
        public string msg { get; set; }
        public string comid { get; set; }
        public int totalCount { get; set; }
        public Resultinfo[] resultInfo { get; set; }
        public int limit_time { get; set; }
    }

    public class Resultinfo
    {
        public int AUTOID { get; set; }
        public string pzhm { get; set; }
        public string rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string cppz { get; set; }
        public string bz { get; set; }

        public string xzsl { get; set; }
        public string dhno { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string dw { get; set; }
        public string package { get; set; }
        public string cpsh { get; set; }
        public string pjhm { get; set; }

        public float gg { get; set; }

        public float ggs { get; set; }
        public string cpcm { get; set; }
        public int ks { get; set; }
        public int sl { get; set; }
        public float dj { get; set; }
        public float je { get; set; }
        public string khhm1 { get; set; }
        public string khmc1 { get; set; }
        public int DB { get; set; }
    }



}
