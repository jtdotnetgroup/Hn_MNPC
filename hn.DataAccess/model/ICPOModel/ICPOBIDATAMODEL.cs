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
    [TableName("ICPOBIDATA")]
    [Description("采购订单-参考数量")]
    public class ICPOBIDATAMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 分录号
        /// </summary>
        public int FENTRYID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string FUNITID { get; set; }

        /// <summary>
        /// 未结订单数量
        /// </summary>
        public decimal FSALEOUTQTY { get; set; }

        /// <summary>
        /// 销售预测数量
        /// </summary>
        public decimal FFORCASTQTY { get; set; }

        /// <summary>
        /// 安全库存数量
        /// </summary>
        public decimal FSAFETYQTY { get; set; }

        /// <summary>
        /// 现有库存
        /// </summary>
        public decimal FINVQTY { get; set; }

        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime FNEEDDATE { get; set; }

        /// <summary>
        /// 在途数量
        /// </summary>
        public decimal FPURINQTY { get; set; }

        /// <summary>
        /// 运输周期
        /// </summary>
        public decimal FCYCDAYS { get; set; }

        /// <summary>
        /// 系数
        /// </summary>
        public decimal FRATE { get; set; }

        /// <summary>
        /// 参考数量
        /// </summary>
        public decimal FADVQTY { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }


    }
}
