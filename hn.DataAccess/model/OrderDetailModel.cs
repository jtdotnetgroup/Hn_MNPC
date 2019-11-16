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
    [TableName("TB_ORDER_DETAIL")]
    [Description("订单明细表")]
    public class OrderDetailModel
    {
        public string FID { get; set; }
        public string ORDER_ID { get; set; }
        public decimal ORDER_SN { get; set; }
        public decimal LINE_NO { get; set; }
        public string ORDER_TYPE { get; set; }
        public string ORG_ID { get; set; }
        public string PRODUCT_ID { get; set; }
        public string OUT_PRODUCT_SN { get; set; }
        public string PRODUCT_SN { get; set; }
        public string PRODUCT_NAME { get; set; }
        public decimal PRODUCT_PRICE { get; set; }
        public decimal SALES_PRICE { get; set; }
        public decimal PRODUCT_QUANTITY { get; set; }
        public decimal AMOUNT { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}

