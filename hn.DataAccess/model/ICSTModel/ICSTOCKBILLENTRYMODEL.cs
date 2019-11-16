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
    [TableName("ICSTOCKBILLENTRY")]
    [Description("销区发货-明细")]
    public class ICSTOCKBILLENTRYMODEL
    {
        public string FID { get; set; }
        public string ICSTOCKBILLID { get; set; }
        public decimal FENTRYID { get; set; }
        public string FSRCCODE { get; set; }
        public string FSRCMODEL { get; set; }
        public string FBATCHNO { get; set; }
        public string FCOLORNO { get; set; }
        public decimal FAUDQTY { get; set; }
        public string FBASENUMBER { get; set; }
        public string FSTOCKNUMBER { get; set; }
        public string FSTOCKNAME { get; set; }
        public string FSPNUMBER { get; set; }
        public string FSPNAME { get; set; }
        public string FREMARK { get; set; }
    }
}
