using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using hn.Core.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Model
{
    [TableName("TB_ORDER")]
    [Description("订单表")]
    public class OrderModel
    {
        public string FID { get; set; }
        public decimal ORDER_SN { get; set; }
        public string ORDER_TYPE { get; set; }
        public string ORG_ID { get; set; }
        public string STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public string WAREHOUSE_ID { get; set; }
        public string WAREHOUSE_NAME { get; set; }
        public string OUT_ORDER_SN { get; set; }
        public decimal CHECKSTATUS { get; set; }
        public string OUT_STATUS { get; set; }
        public DateTime DD_DATE { get; set; }
        public DateTime SH_DATE { get; set; }
        public string MEMBER_ID { get; set; }
        public string MEMBER_CODE { get; set; }
        public string SHIP_NAME { get; set; }
        public string SHIP_MOBILE { get; set; }
        public string SHIP_ADDRESS { get; set; }
        public decimal TOTAL_PRODUCT_PRICE { get; set; }
        public DateTime OUT_CREATE_DATE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime LRJDEDATE { get; set; }
        public DateTime CK_DATE { get; set; }
        public string STORES_INVOICE_ID { get; set; }
        public string STORES_INVOICE_NAME { get; set; }
        public string STORES_WORKER_NAME { get; set; }
        public string NEED_INSTALL { get; set; }
        public string DISPATCH_MEMO { get; set; }
        public string DISPATCH_SN { get; set; }
        public string MEMO { get; set; }
        public string MEMO2 { get; set; }
        public string INVOICE_INFO { get; set; }
        public string WORK_ORDER_ID { get; set; }
        public DateTime PUSH_TIME { get; set; }
        public decimal DELETE_FLAG { get; set; }
        public DateTime WORK_ORDER_TIME { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
