using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using hn.Core.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Model
{
    [TableName("thd")]
    [Description("thd")]


    public class thdModel
    {

        public string GG { get; set; }
        public string GGS { get; set; }

        public string dhno { get; set; }
        public string tpackage { get; set; }
        public string CPSH { get; set; }
        public string CPCM { get; set; }
        public string PJHM { get; set; }
        public int LEFTNUM { get; set; }

        public int XZSL { get; set; }

        [DbField(false)]
        public bool FCHECK { get; set; }
        public int FID { get; set; }
        public int AUTOID { get; set; }
        public string pzhm { get; set; }
        public DateTime rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string dw { get; set; }
        public string ks { get; set; }
        public string sl { get; set; }
        public string dj { get; set; }
        public string je { get; set; }
        public string khhm1 { get; set; }
        public string khmc1 { get; set; }
        public string DB { get; set; }

        public string bz { get; set; }
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }

  
}
