using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("TB_DZmanuscript")]
    public class TB_DZmanuscript
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public string FID { get; set; }
        /// <summary>
        /// 枚举项，由JDE_SO单同步生成
        /// </summary>
        public string FbrandID { get; set; }
        /// <summary>
        /// 经营单位内码
        /// </summary>
        public string FpremiseID { get; set; }
        /// <summary>
        /// SO订单号
        /// </summary>
        [Required]
        public string Fjde_sono { get; set; }
        /// <summary>
        /// SO订单属性
        /// </summary>
        public string Fsrcbillno { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string Fproductcode { get; set; }
        /// <summary>
        /// 定制品备注说明
        /// </summary>
        public string Fproductname { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal? Fdelqty_total { get; set; }

        public decimal? Forderprice { get; set; }
        /// <summary>
        /// 商品计量单位
        /// </summary>
        //public string forderunit { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string fempname { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string fbilldate { get; set; }
        /// <summary>
        /// 同步记录生成时间
        /// </summary>
        public string ftime { get; set; }
        /// <summary>
        /// 确认状态,根据下游记录操作情况更新，空值、待确认、已确认
        /// </summary>
        public string fstatus { get; set; }
        /// <summary>
        /// 是否发货完毕,用户操作填写或者接口导入
        /// </summary>
        public string Fisdelover { get; set; }
        /// <summary>
        /// 预留备注信息栏
        /// </summary>
        public string fremark1 { get; set; }
        /// <summary>
        /// 发货日期,用户操作填写或者接口导入
        /// </summary>
        public string fdeldate { get; set; }
        /// <summary>
        /// 实际发货件数,用户操作填写或者接口导入
        /// </summary>
        public string fdelqty { get; set; }
        /// <summary>
        /// 终端客户名,JDE同步信息
        /// </summary>
        public string fcutname { get; set; }
        /// <summary>
        /// 终端客户地址
        /// </summary>
        public string fcutaddr { get; set; }
        /// <summary>
        /// 终端客户电话
        /// </summary>
        public string fcutphone { get; set; }
        /// <summary>
        /// 厂家采购金额	用户操作填写或者接口导入	用户操作填写或者接口导入
        /// </summary>
        public decimal? Forderamount { get; set; }
        /// <summary>
        /// 订单行号	JDE同步信息	JDE同步信息
        /// </summary>
        public decimal FsoentryID { get; set; }

        public int? Fisrejected { get; set; }


    }
}
