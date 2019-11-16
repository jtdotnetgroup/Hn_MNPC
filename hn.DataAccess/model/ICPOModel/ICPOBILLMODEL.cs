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
    [TableName("ICPOBILL")]
    [Description("采购订单")]
    public class ICPOBILLMODEL
    {
    

        /// <summary>
        /// 品牌ID
        /// </summary>
        public string FBRANDID { get; set; }

        /// <summary>
        /// 业务类型ID
        /// </summary>
        public string FTRANSTYPE { get; set; }

        /// <summary>
        /// 厂家账号
        /// </summary>
        public string FCLIENTID { get; set; }

     


        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime FDATE { get; set; }

        [DbField(false)]
        public string FDATESTR { get; set; }

        /// <summary>
        /// 制单人ID
        /// </summary>
        public string FBILLER { get; set; }

        /// <summary>
        /// 制单人名称
        /// </summary>
        [DbField(false)]
        public string FBILLERNAME { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string FTELEPHONE { get; set; }

        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime FBILLDATE { get; set; }


      

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime FCHECKDATE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }


        /// <summary>
        /// 启用状态
        /// </summary>
        public int FSTATE { get; set; }



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


        #endregion


        /// <summary>
        /// 启用状态
        /// </summary>
        [DbField(false)]
        public string FSTATENAME
        {
            get
            {
                return FSTATE == 1 ? "启用" : "禁用";
            }
        }

        [DbField(false)]
        public string FSYNCSTATUSNAME
        {
            get
            {
                switch (FSYNCSTATUS)
                {
                    case 0:
                        return "华耐审核";
                    case 1:
                        return "厂家检查通过";
                    case 2:
                        return "厂家同步生成单据成功";
                    case 3:
                        return "华耐更新成功";
                    case -1:
                        return "厂家审核不通过";
                    case 4:
                        return "同步至厂家";
                    default:
                        return "草稿";
                }
            }
        }
    }
}
