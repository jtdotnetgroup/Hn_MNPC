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
    [TableName("ICSTOCKBILL")]
    [Description("销区发货")]
    public class ICSTOCKBILLMODEL
    {
        public string FID { get; set; }
        public string FBILLNO { get; set; }
        public string FACCOUNT { get; set; }
        public int FSYNCSTATUS { get; set; }       
    }
}
