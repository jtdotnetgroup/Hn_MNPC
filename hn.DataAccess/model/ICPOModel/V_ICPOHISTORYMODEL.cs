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
    [TableName("V_ICPOHISTORY")]
    [Description("采购订单-下单记录")]
    public class V_ICPOHISTORYMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int FENTRYID { get; set; }

        /// <summary>
        /// 订单内码ID
        /// </summary>
        public string FBILLID { get; set; }

        /// <summary>
        /// 原订单分录
        /// </summary>
        public int FSRCENTRYID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string FUNITID { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string FBATCHNO { get; set; }

        /// <summary>
        /// 色号
        /// </summary>
        public string FCOLORNO { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal FPRICE { get; set; }

        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime FNEEDDATE { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal FPOQTY { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal FPOAMOUNT { get; set; }

        /// <summary>
        /// 厂家订单号
        /// </summary>
        public string FCOMMIBILLNO { get; set; }

        /// <summary>
        /// 厂家确认单价
        /// </summary>
        public decimal FCOMMITPRICE { get; set; }

        /// <summary>
        /// 厂家确认数量
        /// </summary>
        public decimal FCOMMITQTY { get; set; }

        /// <summary>
        /// 厂家确认金额
        /// </summary>
        public decimal FCOMMITAMOUNT { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string FSTOCK { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        public string FSTOCKPLACE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }


        /// <summary>
        /// 产品系列
        /// </summary>
        public string FPRODUCTTYPE { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        public string FPRODUCTCODE { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string FPRODUCTNAME { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string FMODEL { get; set; }

        /// <summary>
        /// 基本单位ID
        /// </summary>
        public string FBASICUNIT { get; set; }

        /// <summary>
        /// 基本单位名称
        /// </summary>
        public string FBASICUNITNAME { get; set; }

        /// <summary>
        /// 采购单位名称
        /// </summary>
        public string FUNITNAME { get; set; }
    }
}
