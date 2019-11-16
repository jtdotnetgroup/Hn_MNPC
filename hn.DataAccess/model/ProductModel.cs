using hn.Common.Data;
using System;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_Products")]
    [Description("商品资料表")]
    public class ProductModel
    {
        /// <summary>
        /// 标识列
        /// </summary>
        public virtual string FID { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public virtual string FBRANDID { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        public virtual string FPRODUCTCODE { get; set; }

        /// <summary>
        /// 产品系列
        /// </summary>
        public virtual string FPRODUCTTYPE { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string FPRODUCTNAME { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public virtual string FMODEL { get; set; }

        /// <summary>
        /// 基础单位
        /// </summary>
        public virtual string FUNITID { get; set; }


        /// <summary>
        /// 包装规格
        /// </summary>
        public virtual string FPKGFORMAT { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public virtual string FTYPEID { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public virtual decimal FWEIGHT { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public virtual decimal FVOLUME { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public virtual decimal FSTATUS { get; set; }

        /// <summary>
        /// 平米
        /// </summary>
        public virtual decimal FSQUARE { get; set; }




        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime FUPDATETIME { get; set; }

       
        ///// <summary>
        ///// 常用单位
        ///// </summary>
        //public virtual string FOFTENUNIT { get; set; }

        ///// <summary>
        ///// 基本单位名称
        ///// </summary>
        //public virtual string FBASICUNITNAME { get; set; }

        ///// <summary>
        ///// 常用单位名称
        ///// </summary>
        //public virtual string FOFTENUNITNAME { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        public virtual decimal FCHECKSTATUS { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual string FCHECKERID { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public virtual DateTime FCHECKTIME { get; set; }









        /// <summary>
        /// 厂家名称
        /// </summary>
        public virtual string FSRCNAME { get; set; }
        /// <summary>
        /// 厂家商品代码
        /// </summary>
        public virtual string FSRCCODE { get; set; }
        /// <summary>
        /// 厂家商品型号
        /// </summary>
        public virtual string FSRCMODEL { get; set; }
        /// <summary>
        /// 厂家单位
        /// </summary>
        public virtual string FSRCUNIT { get; set; }
        /// <summary>
        /// 换算率
        /// </summary>
        public virtual decimal FRATE { get; set; }
        /// <summary>
        /// 采购单位
        /// </summary>
        public virtual string FORDERUNIT { get; set; }

        /// <summary>
        /// 组柜编码
        /// </summary>
        public virtual string FGROUP_NO { get; set; }
        /// <summary>
        /// 单位（托）
        /// </summary>
        public virtual string FUNIT_TUO { get; set; }
        /// <summary>
        /// 组柜商品名称
        /// </summary>
        public virtual string FGROUPNAME { get; set; }
        /// <summary>
        /// 组柜商品规格
        /// </summary>
        public virtual string FGROUPMODEL { get; set; }
        /// <summary>
        /// 组柜换单位
        /// </summary>
        public virtual string FGROUPUNIT { get; set; }
    }
}