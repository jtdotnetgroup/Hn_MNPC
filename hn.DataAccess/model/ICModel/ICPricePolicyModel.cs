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
    [TableName("ICPRICEPOLICY")]
    [Description("价格政策")]
    public class ICPRICEPOLICYMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 价格政策编号
        /// </summary>
        public string FBILLNO { get; set; }

        /// <summary>
        /// 价格政策名称
        /// </summary>
        public string FNAME { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public string FBRANDID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public string FPROJECTID { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public decimal FPRIORITY { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime FBILLDATE { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string FBILLERID { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime FCHECKDATE { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public string FCHECKERID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string FBRANDNAME { get; set; }

        /// <summary>
        /// 厂家账号ID
        /// </summary>
        public string FCLIENTID { get; set; }

        /// <summary>
        /// 厂家账号名称
        /// </summary>
        public string FCLIENTACCOUNT { get; set; }

        /// <summary>
        /// 价格政策类型
        /// </summary>
        public string FPOLICYTYPE { get; set; }

        /// <summary>
        /// 审批状态：0未审核 1已审核
        /// </summary>
        public int FCHECKSTATUS { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime FBEGDATE { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime FENDDATE { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string FPROJECTNAME { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
