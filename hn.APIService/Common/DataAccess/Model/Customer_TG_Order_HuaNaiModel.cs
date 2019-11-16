using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("_Customer_TG_Order_HuaNai")]
    [Description("发货计划")]
    public class Customer_TG_Order_HuaNaiModel
    {
        public int FID { get; set; }
        public string FBILLNO { get; set; }
        public DateTime FNEEDDATE { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string CUST_REF { get; set; }
        public string CAR_NO { get; set; }
        public string NOTE { get; set; }
        public string PART_NO { get; set; }
        public string LOCATION_NO { get; set; }
        public string WareHouse { get; set; }
        public string LOT_BATCH_NO { get; set; }
        public string WAIV_DEV_REJ_NO { get; set; }
        public decimal QUANTITY { get; set; }
        public string lineNote { get; set; }
        public string IFS_TG_ORDER { get; set; }
        public int FSYNCSTATUS { get; set; }
        public string FERR_MESSAGE { get; set; }
        public string CodeTG { get; set; }
        public int FENTRYID { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
