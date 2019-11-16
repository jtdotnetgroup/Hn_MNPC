using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("TB_ICPRBill")]
    [Description("订单明细表")]
    public class ICPRBillModel
    {
        public virtual string FID { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public virtual string FCheckerID { get; set; }

        /// <summary>
        /// 发货方式
        /// </summary>
        public virtual string FTransID { get; set; }

        /// <summary>
        /// 计划类型
        /// </summary>
        public virtual string FTypeID { get; set; }

        /// <summary>
        /// 销区ID
        /// </summary>
        public virtual string FOrgID { get; set; }
        
        /// <summary>
        /// 品牌ID
        /// </summary>
        public virtual string FBrandID { get; set; }

        /// <summary>
        /// 计划编号
        /// </summary>
        public virtual string FBillNo { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public virtual DateTime FDate { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public virtual int FTransType { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        public virtual string FBillerID { get; set; }

        /// <summary>
        /// 制单日期
        /// </summary>
        public virtual DateTime FBillDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int FStatus { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public virtual DateTime FCheckDate { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string FTelephone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string FRemark { get; set; }

    }
}
