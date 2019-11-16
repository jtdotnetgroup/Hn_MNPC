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
    [TableName("SYS_UPDATELOGS")]
    [Description("系统更新日志")]
    public class SYS_UPDATELOGMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 内部编码
        /// </summary>
        public int FINSIDECODE { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime FUpdateTime { get; set; }

        /// <summary>
        /// 更新内容
        /// </summary>
        public string FContent { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
