using hn.Common.Data;
using System;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_SETTLEMENT")]
    [Description("结算确认表")]
    public class TB_SETTLEMENTModel
    {
        public string FID { get; set; }
        public string FBRANDNAME { get; set; }
        public string FSETTLE_ORG { get; set; }
        public string FCUSTOMER_NO { get; set; }
        public string FCUSTOMER_NAME { get; set; }
        public string FJDE { get; set; }
        public string FORDER_NO { get; set; }
        public DateTime FDATE_APPLIED { get; set; }
        public string FTYPENAME { get; set; }
        public string FGRADE { get; set; }
        public decimal FQTY { get; set; }
        public string FUNIT { get; set; }
        public decimal FPRICE { get; set; }
        public decimal FAMOUNT { get; set; }
        public string FWAREHOUSE { get; set; }
        public string FTRANSNAME { get; set; }
        public string FSPID { get; set; }
        public string FBATCH_NO { get; set; }
        public string FBATCH_EXPLAIN { get; set; }
        public string FSHIPPING_NO { get; set; }
        public decimal FCONFIRM_PRICE { get; set; }
        public decimal FCONFIRM_AMOUNT { get; set; }
        public string FJDE_ORDER { get; set; }
        public string FPOLICY_PRICE_NO { get; set; }
        public decimal FPOLICY_PRICE { get; set; }
        public string FCOST_RULES { get; set; }
        public decimal FPLAN_FREIGHT_PRICE { get; set; }
        public decimal FPLAN_FREIGHT_AMOUNT { get; set; }
        public string FWAYBILL_NO { get; set; }
        public decimal FFREIGHT { get; set; }
        public string FREMARK1 { get; set; }
        public string FREMARK2 { get; set; }
        public string FREMARK3 { get; set; }
        public decimal FSTATUS { get; set; }
    }
}
