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
    [TableName("V_QUERY_STOCK")]
    [Description("V_QUERY_STOCK")]


    public class V_QUERY_STOCKModel
    {

        public int fid { get; set; }
        public string fpz { get; set; }

        public string cpxh { get; set; }
        public string ccpgg { get; set; }
        public string color_no { get; set; }
        public string funit { get; set; }
        public decimal fqty { get; set; }
        public DateTime ftime { get; set; }

  
        public decimal M2 { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }

  
}
