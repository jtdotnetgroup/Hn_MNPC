using hn.Common.Data;
using System;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_PRICEPOLICY")]
    [Description("价格政策表")]
    public class TB_PRICEPOLICYModel
    {
        public string FID { get; set; }
        public string FBRAND { get; set; }
        public string FCLIENTACCOUNT { get; set; }
        public string FPRICENUMBER { get; set; }
        public string FPRICENAME { get; set; }
        public string FPRICETYPE { get; set; }
        public string FPRIORITY { get; set; }
        public string FSTATUS { get; set; }
        public string FPRODUCTNUMBER { get; set; }
        public string FPRODUCTNAME { get; set; }
        public string FSPC { get; set; }
        public string FUNIT { get; set; }
        public string FBATCHNO { get; set; }
        public string FCOLOR { get; set; }
        public decimal FQTY { get; set; }
        public decimal FAUDITQTY { get; set; }
        public decimal FTOTALQTY { get; set; }
        public decimal FAUDITPRICE { get; set; }
        public decimal FLEFTQTY { get; set; }
        public string FBATCHES_STATUS { get; set; }
        public string FITEMNAME { get; set; }
        public string FREMARK { get; set; }
        public string FSTARTDATE { get; set; }
        public string FENDDATE { get; set; }
        public string FBATCHES_DATE { get; set; }
        public decimal FPOINT { get; set; }
        public decimal FADVERT_POINT { get; set; }
    }
}
