using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("V_PRODUCTS")]
    [Description("商品资料视图")]
    public class ProductViewModel
    {
        /// <summary>
        /// 标识列
        /// </summary>
        public virtual string FID { get; set; }
        public virtual decimal FPRICE { get; set; }

        public virtual decimal FPRICE_A { get; set; }

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
        /// 平米
        /// </summary>
        public virtual decimal FSQUARE { get; set; }

        /// <summary>
        /// 厂家名称
        /// </summary>
        public virtual string FSRCNAME { get; set; }
        
        /// <summary>
        /// 优先级最高单价
        /// </summary>
        public virtual decimal FPRIORITY_H_PRICE { get; set; }

        /// <summary>
        /// 优先级最低单价
        /// </summary>
        public virtual decimal FPRIORITYP_L_RICE { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public virtual string FBATCHNO { get; set; }

        /// <summary>
        /// 色号
        /// </summary>
        public virtual string FCOLORNO { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int FSTATUS { get; set; }

        /// <summary>
        /// 状态名称 
        /// </summary>
        public virtual string FSTATUSNAME
        {
            get
            {
                return FSTATUS == 1 ? "启用" : "禁用";
            }
        }

       
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime FUPDATETIME { get; set; }
        

        /// <summary>
        /// 商品类别
        /// </summary>
        public virtual string FCATEGORYNAME { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public virtual string FBRANDNAME { get; set; }

        /// <summary>
        /// 基本单位名称
        /// </summary>
        public virtual string FUNITNAME { get; set; }

     
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
        /// 审核人
        /// </summary>
        public virtual string FCHECKERUSER { get; set; }


        //public string FSRCID { get; set; }
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

        public virtual string FSRC_GROUP_ID { get; set; }
        public virtual string FSRC_GROUP_UNIT { get; set; }
        public virtual decimal FSRC_GROUP_RATE { get; set; }
    }
}
