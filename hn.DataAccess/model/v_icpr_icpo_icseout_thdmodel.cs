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
    [TableName("v_icpr_icpo_icseout_thd")]
    [Description("v_icpr_icpo_icseout_thd")]


    public class v_icpr_icpo_icseout_thdModel
    {
        //select T1.FID as icprbillentryid, T6.FCLassArea2,to_number(nvl(T5.Sl,'0')) as sl,(to_number(nvl(T5.Sl,'0'))-(select sum(fcommitqty) from icseoutbillentry where thdbm=t5.autoid)) as leftsl,t6.FBRANDID


        public string icprbillentryid { set; get; }

        public string FCLassArea2 { set; get; }

        public decimal sl { set; get; }

        public decimal leftsl { set; get; }

        public string FBRANDID { set; get; }
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }

  
}
