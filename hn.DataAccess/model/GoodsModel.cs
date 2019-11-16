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
    [TableName("TB_GOODS")]
    [Description("商品表")]
    public class GoodsModel
    {
        public string FID { get; set; }
        public string CATEGORY_ID { get; set; }
        public string CATEGORY_TYPE { get; set; }
        public string SHOP_NUMBER { get; set; }
        public string GOODS_NUMBER { get; set; }
        public string UNIT { get; set; }
        public string GOODS_NAME { get; set; }
        public string SPECIFICATION { get; set; }
        public decimal RECEIVABLE_FEE { get; set; }
        public decimal PAYABLE_FEE { get; set; }
        public decimal PRICE { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string AUDIT_USER_ID { get; set; }
        public DateTime AUDIT_TIME { get; set; }
        public string IMAGE_URL { get; set; }
        public decimal STATUS { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string AUDIT_USER { get; set; }
        public string CATEGORY_NUMBER { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
