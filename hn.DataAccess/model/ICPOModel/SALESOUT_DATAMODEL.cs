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
    [TableName("SALESOUT_DATA")]
    [Description("销售出库表")]
    public class SALESOUT_DATAMODEL
    {
        public int FID { get; set; }
        public DateTime FDATE { get; set; }
        public string PART_NAME { get; set; }
        public string PRIME_COMMODITY_NAME { get; set; }
        public string COLOR_NO { get; set; }
        public string FUNIT { get; set; }
        public decimal FQTY { get; set; }
        public DateTime SHIPPING_DATE { get; set; }
        
    }
}
