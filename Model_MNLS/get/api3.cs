using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.api3
{


    public class Rootobject
    {
        public string action { get; set; }
        public string token { get; set; }
        public string comid { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string sourceno { get; set; }
        public string rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string pjhm { get; set; }
        public string zdr { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string cpsh { get; set; }
        public string cpcm { get; set; }
        public string package { get; set; }
        public string dw { get; set; }
        public int ks { get; set; }
        public decimal sl { get; set; }
        public decimal dj { get; set; }
        public decimal je { get; set; }
        public string khhm1 { get; set; }
        public string khmc1 { get; set; }

        public string bz { get; set; }
    }

}
