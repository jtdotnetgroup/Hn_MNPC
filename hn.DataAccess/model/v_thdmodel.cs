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
    [TableName("v_mn_thd")]
    [Description("v_mn_thd")]


    public class v_thdModel
    {
      

        [DbField(false)]
        public decimal LEFTNUM1 { get {
                try
                {
                    decimal i = decimal.Parse(sl);
                    if (XZSL > 0)
                    {

                    }

                    decimal ii = (decimal)XZSL;
                    return i - ii;

                }
                catch
                {
                    return 0;
                }
            } }

        [DbField(false)]

        public int XZSL { get; set; }
        public bool FCHECK { get; set; }
        public string PJHM { get; set; }

        public string DHNO { get; set; }

        public string bz { get; set; }

        public string gg { get; set; }
        public string GGS { get; set; }
        public decimal USENUM { get; set; }
        public string FID { get; set; }
        public string tpackage { get; set; }
        public string cpsh { get; set; }

        public string cpcm { get; set; }

        public string AUTOID { get; set; }
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
        public string FTRANSNAME { get; set; }
        public string FPREMISEBRANDNAME { get; set; }

        public string icprbillentryid { get; set; }

        public string fclassarea2 { get; set; }

        public string fclassarea2name { get; set; }

        public string fpremisename { get; set; }
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }



  
}
