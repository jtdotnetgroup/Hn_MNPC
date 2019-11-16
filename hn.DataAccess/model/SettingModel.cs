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
    [TableName("TB_SETTING")]
    [Description("系统设置")]
    public class SettingModel
    {
        public string FID { get; set; }
        public decimal GPS_TIMER { get; set; }
        public string GPS_START_TIME { get; set; }
        public string GPS_END_TIME { get; set; }
        public string VER_ANDROID { get; set; }
        public string VER_IOS { get; set; }
        public string VER_URL { get; set; }
        public string VER_CONTENT { get; set; }
        public DateTime VER_DATE { get; set; }
        public string DATA_START_DATE { get; set; }
        public decimal MAX_SCHEDULE_ID { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
