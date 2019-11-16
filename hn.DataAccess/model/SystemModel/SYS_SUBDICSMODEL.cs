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
    [TableName("SYS_SUBDICS")]
    [Description("数字典子表")]
    public class SYS_SUBDICSMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 类型-主编码
        /// </summary>
        public string FCLASSID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string FNUMBER { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FNAME { get; set; }

        /// <summary>
        /// 是否启用 0：禁用 1：启用
        /// </summary>
        public int FSTATUS { get; set; }

        /// <summary>
        /// 是否启用 0：禁用 1：启用
        /// </summary>
        [DbField(false)]
        public string FSTATUSNAME
        {
            get
            {
                return FSTATUS==1 ? "启用" : "禁用";
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string FCREATOR { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FCREATETIME { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime FUPDATETIME { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string FUPDATER { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 父类ID
        /// </summary>
        public string FPARENTID { get; set; }


        public override string ToString()
        {
            return FNAME;
        }
    }
}
