using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.Model
{
    [TableName("TB_DELIVERY_DETAIL")]
    [Description("装车发货明细")]
    public class TB_DELIVERY_DETAILModel
    {
        //内码ID
        public string FID { get; set; }
        public decimal FENTRYID { get; set; }
        public string FGROUP_NO { get; set; }
        public DateTime FDATE { get; set; }
        [DbField(false)]
        public string FDATESTR
        {
            get
            {
                return FDATE.ToString("yyyy-MM-dd");
            }
        }

        public string FBRANDID { get; set; }
        public string FPREMISEID { get; set; }
        public string FTRANSID { get; set; }
        public string FPLAN_INFO { get; set; }
        public string FRECEIVERADDR { get; set; }
        public string FDELIVERERADDR { get; set; }
        public decimal FALLWEIGHT { get; set; }
        public string FCARNUMBER { get; set; }
        public string FWAYBILLNO { get; set; }
        public decimal FAMOUNT { get; set; }
        public decimal FSTATUS { get; set; }
        public string FEXPRESSCOMPANYID { get; set; }
        public DateTime FDELIVERDATE { get; set; }
    }
}
