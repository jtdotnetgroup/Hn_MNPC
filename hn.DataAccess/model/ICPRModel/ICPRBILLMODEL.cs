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
    [TableName("ICPRBILL")]
    [Description("请购计划")]
    public class ICPRBILLMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 经营场所ID
        /// </summary>
        public string FPREMISEID { get; set; }

        /// <summary>
        /// 经营场所名称
        /// </summary>
        [DbField(false)]
        public string FPREMISENAME { get; set; }

        /// <summary>
        /// 业务类型ID
        /// </summary>
        public string FTRANSTYPE { get; set; }

        /// <summary>
        /// 发货方式
        /// </summary>
        public string FTRANSID { get; set; }



        /// <summary>
        /// 计划类型
        /// </summary>
        public string FTYPEID { get; set; }



        /// <summary>
        /// 品牌ID
        /// </summary>
        public string FBRANDID { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string FBRANDNAME { get; set; }

        /// <summary>
        /// 计划编号
        /// </summary>
        public string FBILLNO { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime FDATE { get; set; }


        /// <summary>
        /// 申请日期
        /// </summary>
        [DbField(false)]
        public string FDATESTR
        {
            get
            {
                return FDATE != null ? FDATE.ToString("yyyy-MM-dd") : "";
            }
        }

        ///// <summary>
        ///// 业务类型名称
        ///// </summary>
        //[DbField(false)]
        //public string FTRANSTYPENAME
        //{
        //    get
        //    {
        //        return FTRANSTYPE != null ? Enum.GetName(typeof(Constant.ICPRBILL_FTRANSTYPE), FTRANSTYPE) : "";
        //    }
        //}

        /// <summary>
        /// 申请人ID
        /// </summary>
        public string FBILLERID { get; set; }

        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime FBILLDATE { get; set; }

        /// <summary>
        /// 审核状态 1:草稿  2:审核中  3:审核通过  4:审核不通过  
        /// </summary>
        public int FSTATUS { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [DbField(false)]
        public string FSTATUSNAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.ICPRBILL_FSTATUS), FSTATUS);
            }
        }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public string FCHECKERID { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime FCHECKDATE { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string FTELEPHONE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 厂家账户
        /// </summary>
        public string FCLIENTID { get; set; }

        public string FENGINEERINGID { get; set; }
        public string FRECEIVINGADDR { get; set; }
        public string FFREIGHTID { get; set; }

        /// <summary>
        /// 签约主体
        /// </summary>
        public string SIGN_MAIN { get; set; }

        /// <summary>
        /// JDE地址号
        /// </summary>
        public string JDE { get; set; }

        /// <summary>
        /// CRM单号
        /// </summary>
        public string CRMNO { get; set; }

        /// <summary>
        /// 工程名称
        /// </summary>
        public string FPROJECTNAME { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        public string FPURCHASE_NO { get; set; }

        /// <summary>
        /// 结算组织
        /// </summary>
        public string FSETTLE_ORG { get; set; }

        /// <summary>
        /// 发货地点
        /// </summary>
        public string FDELIVERYADDR { get; set; }
        
        /// <summary>
        /// 重量合计
        /// </summary>
        public decimal FWEIGHT { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string FIDENTIFICATION { get; set; }

        /// <summary>
        /// 是否启用审批流
        /// </summary>
        public int FISAUDITFLOW { get; set; }
        /// <summary>
        /// 采购确认时间
        /// </summary>
        public DateTime FConfirmTime { get; set; }

        public string FPROVINCEID { get; set; }
        public string FCITYID { get; set; }
        public string FDISTRICTID { get; set; }
        public string FCONSIGNEE { get; set; }
        public string FCONSIGNEE_TEL { get; set; }


        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }

    /// <summary>
    /// 保存请购计划的数据类
    /// </summary>
    public class ICPRBillDataModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string FITEMID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public decimal FENTRYID { get; set; }

        public ICPRBILLENTRYMODEL ICPRBillEntry { get; set; }

        //public ICPRBIDATAMODEL ICRPBIData { get; set; }

        public ICPRPOLICYMODEL ICPRPolicy { get; set; }

     
    }
}
