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
    [TableName("V_ICPRICEPOLICY")]
    [Description("价格政策")]
    public class V_ICPRICEPOLICYMODEL : ICPRICEPOLICYMODEL
    {
        
        public string FPOLICYTYPENAME { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public string FBEGDATESTR
        {
            get
            {
                return FBEGDATE != null ? Convert.ToDateTime(FBEGDATE).ToString("yyyy-MM-dd") : "";
            }
        }

        /// <summary>
        /// 截止日期
        /// </summary>
        public string FENDDATESTR
        {
            get
            {
                return FENDDATE != null ? Convert.ToDateTime(FENDDATE).ToString("yyyy-MM-dd") : "";
            }
        }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
