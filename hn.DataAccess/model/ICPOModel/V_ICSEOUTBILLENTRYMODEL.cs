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
    [TableName("V_ICSEOUTBILLENTRY")]
    [Description("发货通知单-明细")]
    public class V_ICSEOUTBILLENTRYMODEL : ICSEOUTBILLENTRYMODEL
    {

        public string perWeight { set; get; }

        public string tCount { set; get; }

        public string BXWeight { set; get; }
        public bool FCHECK { get; set; }
        public string FSRCCODE { get; set; }
        public string FSRCNAME { get; set; }
        public string FSRCMODEL { get; set; }
        public string FSRCUNIT { get; set; }
        public string FORDERUNIT { get; set; }
        public decimal FRATE { get; set; }
        public string FPRODUCTTYPE { get; set; }
        public string FPRODUCTCODE { get; set; }
        public string FPRODUCTNAME { get; set; }
        public string FMODEL { get; set; }

        public decimal LEFTNUM { set; get; }
        public string FUNITID { get; set; }
        public string FUNITNAME { get; set; }
        public decimal FICPRENTRYID { get; set; }
        public decimal FACCOUNTPRICE { get; set; }
        public string ICPRBILLNO { get; set; }
        public string FPOLICYNAME { get; set; }
        public string FPOLICYBILLNO { get; set; }
        public decimal FASKQTY { get; set; }
        public decimal FLEFTAMOUNT { get; set; }
        public decimal FORDERUNITQTY { get; set; }
        public decimal FORDERUNITLEFTQTY { get; set; }
        public string FPREMISENAME { get; set; }
        public string FCLASSAREAREMARK { get; set; }
        public string FPREMISEBRANDNAME { get; set; }
        public string JDE { get; set; }
        public string FFACTORYNO { get; set; }
        public string FCATEGORYCODE { get; set; }
        public string FCATEGORYNAME { get; set; }
        public string FCLIENTNAME { get; set; }





        public string thdbm { get; set; }

        public string pz { get; set; }

        public string gg { get; set; }

        public string xh { get; set; }

        public string sh { get; set; }

        public string dw { get; set; }

        public string dj { get; set; }

        public string cpdj { get; set; }

        public string khhm { get; set; }

        public string khmc { get; set; }

        public string pzhm { get; set; }

        public string kdrq { get; set; }


        public string cpcm { get; set; }

        public string cpsh { get; set; }

        public decimal singleWeight { set; get; }








    }
}
