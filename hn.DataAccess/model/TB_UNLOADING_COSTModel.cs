using hn.Common.Data;
using System;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_UNLOADING_COST")]
    [Description("卸案成本")]
    public class TB_UNLOADING_COSTModel
    {
        public string FID { get; set; }
        public string FCOMPANY { get; set; }
        public string FDELIVERY_ADDR_BP { get; set; }
        public string FDELIVERY_ADDR_NAME { get; set; }
        public string FARRIVAL_ADDR_BP { get; set; }
        public string FARRIVAL_ADDR_NAME { get; set; }
        public string FPRINTMSG { get; set; }
        public string FPRINT_EXPLAIN { get; set; }
        public decimal FTRANSPORTTPE { get; set; }
        public string FTRANSPORT_EXPLAIN { get; set; }
        public string FCOST_RULES { get; set; }
        public string FCOST_EXPLAIN { get; set; }
        public decimal FSHIP_BUFDA { get; set; }
        public decimal FTILES_PRICE { get; set; }
        public decimal FBATHROOM_PRICE { get; set; }
        public string FIDENTIFICATION { get; set; }
        public string FUPDATE_DATE { get; set; }
    }
}
