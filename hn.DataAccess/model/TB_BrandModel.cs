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
    [TableName("TB_Brand")]
    [Description("厂家品牌资料")]
    public class TB_BrandModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }
        /// <summary>
        /// 品牌代码
        /// </summary>
        public string FNUMBER { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string FNAME { get; set; }
        /// <summary>
        /// 厂家名称
        /// </summary>
        public string FFACTORY { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string FINTERFACEADD { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary>
        public string FFACTORYNO { get; set; }

        /// <summary>
        /// 品类
        /// </summary>
        public string FTYPE { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string FLINKMAN { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string FLINKPHONE { get; set; }

        public override string ToString()
        {
            return FNAME;
        }
    }
}
