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
    [TableName("TB_PROVINCE")]
    [Description("城市省表")]
    public class ProvinceModel
    {
        public decimal PROVINCEID { get; set; }
        public string PROVINCENAME { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
