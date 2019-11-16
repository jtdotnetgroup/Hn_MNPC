using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("sales_order_data")]
    [Description("销售出库表")]
    public class sales_order_dataModel
    {
        public int fid { get; set; }
        public string Part_name { get; set; }
        public string PRIME_COMMODITY_NAME { get; set; }
        public string funit { get; set; }
        public decimal fqty { get; set; }
        public DateTime ftime { get; set; }
        
        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
