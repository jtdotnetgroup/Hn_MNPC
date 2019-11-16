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
    [TableName("V_ICPRICEPOLICYENTRY")]
    [Description("请购计划明细")]
    public class V_ICPRICEPOLICYENTRYMODEL : ICPRICEPOLICYENTRYMODEL
    {

        public string FPRODUCTTYPE { get; set; }

        public string FPRODUCTCODE { get; set; }

        public string FPRODUCTNAME { get; set; }

        public string FMODEL { get; set; }

        public string FUNITID { get; set; }

        public string FUNITNAME { get; set; }

        public string FPOLICYNAME { get; set; }

        public string FPOLICYBILLNO { get; set; }


        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
