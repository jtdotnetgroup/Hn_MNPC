using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("V_BIINTERFACE")]
    [Description("BI接口")]
    public class V_BIINTERFACEMODEL : BIINTERFACEMODEL
    {

        public string FPRODUCTID { get; set; }
        public string FPRODUCTNAME { get; set; }
        public string FMODEL { get; set; }
        public string FPRODUCTTYPE { get; set; }
        public string FUNITNAME { get; set; }
        public string FUNITID { get; set; }
        public string FORDERUNIT { get; set; }
        public string FBRANDID { get; set; }
        public string FBRANDNAME { get; set; }
        public string FPKGFORMAT { get; set; }
        public decimal FWEIGHT { get; set; }
        public decimal FVOLUME { get; set; }
        public string FCOLORNO { get; set; }
        public decimal FPRIORITYP_L_RICE { get; set; }
        public string FBATCHNO { get; set; }
        public decimal FRATE { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }

    }
}
