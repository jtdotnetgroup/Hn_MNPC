using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("_PLANBill_HuaNai")]
    [Description("发货计划")]
    public class PLANBill_HuaNaiModel
    {
        public int FID { get; set; }
        public string FBILLNO { get; set; }
        public string FSRCBILLNO { get; set; }
        public int FSTATUS { get; set; }
        public string FACCOUNT { get; set; }
        public int FSYNCSTATUS { get; set; }
        public string FPLATENUMBER { get; set; }
        public string FDRIVER { get; set; }
        public string FDRPHONE { get; set; }
        public string FCENTER_WAREHOUSE { get; set; }
        public int FIS_CONSIGN { get; set; }
        public string FDELIVERY_METHOD { get; set; }
        public int FENTRYID { get; set; }
        public string FITEMID { get; set; }
        public string FSRCCODE { get; set; }
        public string FSRCNAME { get; set; }
        public string FSRCMODEL { get; set; }
        public string FBATCHNO { get; set; }
        public string FCOLORNO { get; set; }
        public decimal FPRICE { get; set; }
        public decimal FAUDQTY { get; set; }
        public decimal FAMOUNT { get; set; }
        public decimal FCOMMITQTY { get; set; }
        public string FREMARK { get; set; }
        public string FERR_MESSAGE { get; set; }
        public string FGRADE { get; set; }
        public DateTime FNEEDDATE { get; set; }
        public string FLINE_NOTE { get; set; }
        public string FPLANDESC { get; set; }
        public string FPURCHASE_NO { get; set; }
        public bool Finfo_RE_id { get; set; }
        public int Finfo_RE_status { get; set; }
        public int Finfo_RE_qty { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
