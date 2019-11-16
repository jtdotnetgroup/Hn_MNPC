using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("TMP_STOCKBill")]
    [Description("销区发货")]
    public class TMP_STOCKBillModel
    {
        public int FID { get; set; }
        public string FBILLNO { get; set; }
        public string FACCOUNT { get; set; }
        public int FSYNCSTATUS { get; set; }
        public int FENTRYID { get; set; }
        public string FSRCCODE { get; set; }
        public string FSRCMODEL { get; set; }
        public string FBATCHNO { get; set; }
        public string FCOLORNO { get; set; }
        public decimal FAUDQTY { get; set; }
        public string FBASENUMBER { get; set; }
        public string FSTOCKNUMBER { get; set; }
        public string FSTOCKNAME { get; set; }
        public string FSPNUMBER { get; set; }
        public string FSPNAME { get; set; }
        public string FREMARK { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
