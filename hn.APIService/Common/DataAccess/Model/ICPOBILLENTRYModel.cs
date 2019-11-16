using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("ICPOBILLENTRY")]
    [Description("销售订单")]
    public class ICPOBILLENTRYModel_MHLS
    {
        #region 蒙厂的字段


        /// <summary>
        /// 同步状态
        /// </summary>
        public int FSYNCSTATUS { get; set; }

        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string FBILLNO { get; set; }

        /// <summary>
        /// 厂家订单编号
        /// </summary>
        public string FDesBillNo { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int FSTATUS { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public string Fcompany { get; set; }

        /// <summary>
        /// 项目审批号码
        /// </summary>
        public string FprojectNO { get; set; }

        /// <summary>
        /// 厂家账户代码（客户代号）
        /// </summary>
        public string FACCOUNT { get; set; }

        /// <summary>
        /// 订单类别
        /// </summary>
        public string FPOtype { get; set; }

        /// <summary>
        /// 价格政策
        /// </summary>
        public string Fpricepolicy { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string Fnote { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string FTIMESTAMP { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int FENTRYID { get; set; }
        /// <summary>
        /// 分录总行数
        /// </summary>
        public int FentryTotal { get; set; }

        /// <summary>
        /// 厂家订单分录ID/序号
        /// </summary>
        public int Fdesbillentry { get; set; }

        /// <summary>
        /// 厂家代码/品种
        /// </summary>
        public string FSRCCODE { get; set; }

        /// <summary>
        /// 厂家名称/型号
        /// </summary>
        public string FSRCNAME { get; set; }

        /// <summary>
        /// 厂家规格
        /// </summary>
        public string FSRCMODEL { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public string Flevel { get; set; }


        /// <summary>
        /// 仓库
        /// </summary>
        public string FstockNO { get; set; }


        /// <summary>
        /// 合同号
        /// </summary>
        public string FcontractNO { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Funit { get; set; }

        /// <summary>
        /// 合同数量
        /// </summary>
        public decimal FAUDQTY { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal fprice { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal Famount { get; set; }

        /// <summary>
        /// 检查错误描述
        /// </summary>
        public string FERR_MESSAGE { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 色号
        /// </summary>
        public string FCOLORNO { get; set; }


        #endregion

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
