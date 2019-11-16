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
    [TableName("TB_DISTRICT")]
    [Description("城市区县表")]
    public class DistrictModel
    {
        public decimal DISTRICTID { get; set; }
        public string DISTRICTNAME { get; set; }
        public decimal CITYID { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
