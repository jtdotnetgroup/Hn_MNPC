using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using hn.APIService;

namespace hn.APIService.DataAccess.Model
{
    [TableName("ICPO_BOLentry")]
    [Description("提货单开单数据")]
    public class ICPO_BOLentryModel_MNLS
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 原销售订单编号
        /// </summary>
        public string FPObillno { get; set; }

        /// <summary>
        /// 原销售订单ID号
        /// </summary>
        public string Fpobillentry { get; set; }

        /// <summary>
        /// 提货单号
        /// </summary>
        public string Ficbolno { get; set; }

            /// <summary>
            /// 提货单ID号
            /// </summary>
       public int Ficbolentry { get; set; }

        /// <summary>
        /// 项目审批号码
        /// </summary>
        public string FprojectNO { get; set; }

        /// <summary>
        /// 同步状态
        /// </summary>
        public int FSYNCSTATUS { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public int FTIMESTAMP { get; set; }

        /// <summary>
        /// 提货单日期
        /// </summary>
        public int FDATE { get; set; }

        /// <summary>
        /// 厂家账户号（客户代号）
        /// </summary>
        public int FACCOUNT { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int FITEMID { get; set; }

        /// <summary>
        /// 厂家代码/品种
        /// </summary>
        public int FSRCCODE { get; set; }

        /// <summary>
        ///厂家名称/型号 
        /// </summary>
        public int FSRCNAME { get; set; }

        /// <summary>
        /// 
        /// 厂家规格        /// </summary>
        public int FSRCMODEL { get; set; }


        /// <summary>
        ///  合同号
        /// </summary>
        public int FcontractNO { get; set; }

        /// <summary>
        ///  等级
        /// </summary>
        public int Flevel { get; set; }

        /// <summary>
        ///  仓库
        /// </summary>
        public int FstockNO { get; set; }

        /// <summary>
        ///  色号
        /// </summary>
        public int FCOLORNO { get; set; }

        /// <summary>
        ///  单位
        /// </summary>
        public int Funit { get; set; }

        /// <summary>
        ///  开单数量
        /// </summary>
        public int FAUDQTY { get; set; }

        /// <summary>
        ///  出仓数量
        /// </summary>
        public int FcommitQTY { get; set; }

        /// <summary>
        ///  开单价格
        /// </summary>
        public int fprice { get; set; }

        /// <summary>
        ///  合同金额
        /// </summary>
        public int Famount { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        public int FREMARK { get; set; }

    }
}
