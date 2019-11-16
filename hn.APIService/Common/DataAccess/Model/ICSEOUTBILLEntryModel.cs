using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("ICSEOUTBILLEntry")]
    [Description("发货计划明细")]
    public class ICSEOUTBILLEntryModel
    {
        public int FID { get; set; }
        public int FICSEOUTBILLID { get; set; }
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

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
