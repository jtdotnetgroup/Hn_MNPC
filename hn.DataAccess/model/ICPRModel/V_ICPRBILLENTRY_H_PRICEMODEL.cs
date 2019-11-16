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
    [TableName("V_ICPRBILLENTRY_H_PRICE")]
    [Description("请购计划明细")]
    public class V_ICPRBILLENTRY_H_PRICEMODEL: V_ICPRBILLENTRYMODEL
    {
        
        public decimal FACCOUNTPRICE { get; set; }
        public string FCLIENTID { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
