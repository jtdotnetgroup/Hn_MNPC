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
    [TableName("V_ICPRPOLICY")]
    [Description("V_ICPRPOLICY")]
    public class V_ICPRPOLICYMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 分录号
        /// </summary>
        public decimal FSRCENTRYID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public decimal FENTRYID { get; set; }

        /// <summary>
        /// 单位ID--不知道干什么
        /// </summary>
        public string FUNITID { get; set; }

        /// <summary>
        /// 价格政策采购单位ID
        /// </summary>
        public string ICPRICEPOLICYFUNITID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 采购单价
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
        /// 基础单位名称
        /// </summary>
        public string FBASICUNITNAME { get; set; }

        /// <summary>
        /// 起始数量
        /// </summary>
        public decimal FBEGQTY { get; set; }

        /// <summary>
        /// 结束数量
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
