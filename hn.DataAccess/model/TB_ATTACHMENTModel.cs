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
    [TableName("TB_ATTACHMENT")]
    [Description("附件表")]
    public class TB_ATTACHMENTModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }
        /// <summary>
        /// 单据ID内码
        /// </summary>
        public string FBILLID { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public int FTYPE { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FFILENAME { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FPATH { get; set; }
        /// <summary>
        /// 上传人
        /// </summary>
        public string FADD_USER { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime FADD_TIME { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
