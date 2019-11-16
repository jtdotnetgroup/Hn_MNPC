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
    [TableName("TB_SMS")]
    [Description("短信记录")]
    public class SmsModel
    {
        public string FID { get; set; }
        public string PHONE { get; set; }
        public DateTime SEND_TIME { get; set; }
        public string CONTENT { get; set; }
        public string SEND_CODE { get; set; }
 
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
