using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("TP_inventory")]
    [Description("时点库存表")]
    public class TP_inventoryModel
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
