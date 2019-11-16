using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
namespace hn.Core.Model
{
    [TableName("Sys_Dics")]
    public class SysDics
    {
        /// <summary>
        /// 标识列
        /// </summary>
        public virtual string FID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public virtual string FClassCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string FClassName { get; set; }

        /// <summary>
        /// 系统预置
        /// </summary>
        public virtual int FSysDefault { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public virtual string FCreatorID { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public virtual string FUpdaterID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime FCreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime FUpdateTime { get; set; }

        /// <summary>
        /// 字典标识
        /// </summary>
        public virtual string FClassIdent { get; set; }

        // public int Sortnum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FRemark { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }

    }
}
