using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("salesout_data")]
    [Description("订单订货数据表")]
    public class salesout_dataModel
    {
        public int fid { get; set; }
        public DateTime fdate { get; set; }
        public string Part_name { get; set; }
        public string PRIME_COMMODITY_NAME { get; set; }
        public string COLOR_NO { get; set; }
        public string funit { get; set; }
        public decimal fqty { get; set; }
        public DateTime Shipping_date { get; set; }
        
        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
