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
    [TableName("ICPRBILLENTRY")]
    [Description("请购计划明细")]
    public class ICPRBILLENTRYMODEL
    {

        public string ICPOBILLENTRYID { get; set; }

        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string FPLANID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public decimal FENTRYID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal FWHOLESALEPRICE { get; set; }


        /// <summary>
        /// 基本单位
        /// </summary>
        /// public string FBASICUNIT { get; set; }

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
        /// 参考数量
        /// </summary>
        public decimal FADVQTY { get; set; }

        /// <summary>
        /// 申请数量
        /// </summary>
        public decimal FASKQTY { get; set; }

        /// <summary>
        /// 申请金额
        /// </summary>
        public decimal FASKAMOUNT { get; set; }

        /// <summary>
        /// 审批数量
        /// </summary>
        //public int FAUDQTY   { get; set; }

        /// <summary>
        /// 审批金额
        /// </summary>
        // public decimal FAUDAMOUNT { get; set; }

        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime FNEEDDATE { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int FSTATUS { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 采购订单编号
        /// </summary>
        public string FICPOBILLNO { get; set; }

        /// <summary>
        /// 发货剩余数量
        /// </summary>
        public decimal FLEFTAMOUNT { get; set; }

        public string FORDERUNIT { get; set; }
        public decimal FORDERUNITQTY { get; set; }
        public string FWEIGHTUNIT { get; set; }
        public decimal FWEIGHTQTY { get; set; }
        public string FORDERREMARK1 { get; set; }
        public string FORDERREMARK2 { get; set; }
        public string FORDERREMARK3 { get; set; }
        public string FORDERREMARK4 { get; set; }

        public string FACCOUNT { get; set; }
        public string FSTOREHOUSE { get; set; }
        public string FPOLICY { get; set; }
        public decimal FCOMMITQTY { get; set; }
        public string FTRANSNAME { get; set; }

        /// <summary>
        /// 采购确认人
        /// </summary>
        public string FCONFIRM_USER { get; set; }
        public DateTime FCONFIRM_TIME { get; set; }

        /// <summary>
        /// 人工关闭状态
        /// </summary>
        public decimal FCLOSE { get; set; }

        /// <summary>
        /// 人工关闭用户
        /// </summary>
        public string FCLOSE_USER { get; set; }

        public string FCLOSE_RESON { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime FCLOSE_TIME { get; set; }

        /// <summary>
        /// 价格政策编号
        /// </summary>
        public string FPRICENUMBER { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
