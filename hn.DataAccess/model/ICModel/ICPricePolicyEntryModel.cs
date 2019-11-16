using System;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;

namespace hn.DataAccess.Model
{
    [TableName("ICPRICEPOLICYENTRY")]
    [Description("价格政策明细")]
    public class ICPRICEPOLICYENTRYMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 价格政策ID
        /// </summary>
        public string FPOLICYID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int FENTRYID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string FBATCHNO { get; set; }

        /// <summary>
        /// 色号
        /// </summary>
        public string FCOLORNO { get; set; }

        /// <summary>
        /// 最大执行数量
        /// </summary>
        public decimal FQTYLIMIT { get; set; }

        /// <summary>
        /// 已执行数量
        /// </summary>
        public decimal FQTYCURRENT { get; set; }

        /// <summary>
        /// 剩余数量
        /// </summary>
        public decimal FQTYREST { get; set; }

        /// <summary>
        /// 结算价格
        /// </summary>
        public decimal FACCOUNTPRICE { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public decimal FREQUIREQTY { get; set; }

        /// <summary>
        /// 厂家额度数量
        /// </summary>
        public decimal FFACTORYLIMIT { get; set; }

        /// <summary>
        /// 批条状态
        /// </summary>
        public decimal FSTATUS { get; set; }

        /// <summary>
        /// 扣年返点值
        /// </summary>
        public decimal FYEARLYRETURN { get; set; }

        /// <summary>
        /// 扣广告返点值
        /// </summary>
        public decimal FADRETURN { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }


        ///// <summary>
        ///// 单位ID
        ///// </summary>
        //public string FUNITID { get; set; }




        ///// <summary>
        ///// 采购单价
        ///// </summary>
        //public decimal FWHOLESALEPRICE { get; set; }

        ///// <summary>
        ///// 价格调整
        ///// </summary>
        //public decimal FAJUST { get; set; }

        ///// <summary>
        ///// 加点
        ///// </summary>
        //public decimal FADD { get; set; }

        ///// <summary>
        ///// 销区价格
        ///// </summary>
        //public decimal FAREAPRICE { get; set; }



        ///// <summary>
        ///// 项目ID
        ///// </summary>
        //public string FPROJECTID { get; set; }







        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
