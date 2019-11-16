using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("_trans_finish")]
    [Description("完成记录表")]
    public class trans_finishModel
    {
        public int fid { get; set; }
        public string fbillno { get; set; }
        public bool if_TGbill { get; set; }
        
        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
