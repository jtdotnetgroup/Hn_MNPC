using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("ICSEOUTBILL")]
    [Description("发货计划")]
    public class ICSEOUTBILLModel
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
        public bool FIS_CONSIGN { get; set; }
        public string FDELIVERY_METHOD { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
