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
    [TableName("ICPRPOLICY")]
    [Description("请购计划价格政策")]
    public class ICPRPOLICYMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 原订单分录
        /// </summary>
        public int FSRCENTRYID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int FENTRYID { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string FUNITID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal FPRICE { get; set; }

        /// <summary>
        /// 价格政策ID
        /// </summary>
        public string FPOLICYID { get; set; }

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
