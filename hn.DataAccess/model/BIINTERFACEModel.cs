using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("BIINTERFACE")]
    [Description("BI接口")]
    public class BIINTERFACEMODEL
    {
        public string FID { get; set; }
        public string FBRANDNO { get; set; }
        public string FDEPTNO { get; set; }
        public string FNUMBER { get; set; }
        public decimal FMONTHLYQTY { get; set; }
        public decimal FHIGHLIMIT { get; set; }
        public decimal FLOWLIMIT { get; set; }
        public decimal FSAFEQTY { get; set; }
        public decimal FINVQTY { get; set; }
        public decimal FDIFFQTY { get; set; }
        public decimal FEXTENDEDINV { get; set; }
        public string FSTATE { get; set; }
        public decimal FPURINQTY { get; set; }
        public decimal FSALEOUTQTY { get; set; }
        public decimal FFORCASTQTY { get; set; }
        public decimal FADVICEQTY { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }

    }
}
