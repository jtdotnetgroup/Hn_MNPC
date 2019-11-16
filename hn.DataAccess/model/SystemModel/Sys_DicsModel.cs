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
    [TableName("SYS_DICS")]
    [Description("数据字典主表")]
    public class SYS_DICSMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string FCLASSCODE { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FCLASSNAME { get; set; }

        /// <summary>
        /// 系统预置
        /// </summary>
        public int FSYSDEFAULT { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string FCREATORID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FCREATETIME { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string FUPDATERID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime FUPDATETIME { get; set; }

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
