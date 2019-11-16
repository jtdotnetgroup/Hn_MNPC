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
    [TableName("TB_MASTER_SIGN")]
    [Description("签到记录")]
    public class MasterSignModel
    {
        public string FID { get; set; }
        public string SIGNER { get; set; }
        public DateTime SIGN_TIME { get; set; }
        public string TYPE { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }
        public string CODE { get; set; }
        public string WORK_ORDER_ID { get; set; }
        public string ADDRESS { get; set; }
        public string SIGNER_ID { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
