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
    [TableName("V_ICPOPOLICY")]
    [Description("采购订单-价格政策")]
    public class V_ICPOPOLICYMODEL
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
        /// 价格政策明细ID
        /// </summary>
        public string FPOLICYID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }


        /// <summary>
        /// 价格政策编号 
        /// </summary>
        public string ICPRICEPOLICYBILLNO { get; set; }

        /// <summary>
        /// 价格政策名称
        /// </summary>
        public string ICPRICEPOLICYNAME { get; set; }

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
        /// 基本单位
        /// </summary>
        public string FBASICUNITNAME { get; set; }

        /// <summary>
        /// 起始单位
        /// </summary>
        public decimal FBEGQTY { get; set; }

        /// <summary>
        /// 结束单位
        /// </summary>
        public decimal FENDQTY { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime FBEGDATE { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime FENDDATE { get; set; }
    }
}
