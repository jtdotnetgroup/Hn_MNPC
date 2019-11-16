using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("TB_Brand")]
    [Description("品牌信息表")]
    public class BrandModel
    {
        /// <summary>
        /// 标识列
        /// </summary>
        public virtual string FID { get; set; }

        /// <summary>
        /// 品牌代码
        /// </summary>
        public virtual string FNumber { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public virtual string FName { get; set; }

        /// <summary>
        /// 厂家名称
        /// </summary>
        public virtual string FFactory { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public virtual string FInterfaceAdd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string FRemark { get; set; }
    }
}
