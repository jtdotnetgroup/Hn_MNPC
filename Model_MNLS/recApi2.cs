using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApiModel.recApi2
{
    public class Rootobject
    {
        public int status { get; set; }
        public string msg { get; set; }
       // public string comid { get; set; }
        public int totalCount { get; set; }
        public Resultinfo[] resultInfo { get; set; }
    }

    public class Resultinfo
    {
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string cpsh { get; set; }
        public string package { get; set; }
        public string cpcm { get; set; }
        public string dw { get; set; }
        public int ks { get; set; }
        public int bysl { get; set; }

        public string db { get; set; }
    }
}
