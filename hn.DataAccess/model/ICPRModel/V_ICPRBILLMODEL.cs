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
    [TableName("V_ICPRBILL")]
    [Description("请购计划")]
    public class V_ICPRBILLMODEL : ICPRBILLMODEL
    {

        /// <summary>
        /// 二级销区
        /// </summary>
        public string FCLASSAREA2 { get; set; }


        /// <summary>
        /// 品牌部
        /// </summary>
        public string FPREMISEBRANDID { get; set; }

        /// <summary>
        /// 二级销区
        /// </summary>
        public string FCLASSAREA2NAME { get; set; }


        /// <summary>
        /// 品牌部
        /// </summary>
        public string FPREMISEBRANDNAME { get; set; }


        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string FBILLERNAME { get; set; }

        /// <summary>
        /// 计划类型名称
        /// </summary>
        [DbField(false)]
        public string FTYPENAME { get; set; }

        /// <summary>
        /// 运输方式名称
        /// </summary>
        [DbField(false)]
        public string FTRANSNAME { get; set; }

        /// <summary>
        /// 工程性质
        /// </summary>
        public string FENGINEERINGNAME { get; set; }

        /// <summary>
        /// 运费结算
        /// </summary>
        public string FFREIGHTNAME { get; set; }

        public string FCONFIRMTIMEDISPLAY
        {
            get
            {
                return this.FConfirmTime != DateTime.MinValue ? this.FConfirmTime.ToString("yyyy-MM-dd") : "";
            }
        }

        public string FPREMISENUMBER { get; set; }
        public string FBRANDNUMBER { get; set; }

        public string FPROVINCENAME { get; set; }
        public string FCITYNAME { get; set; }
        public string FDISTRICTNAME { get; set; }

        public int FISTICKET { get; set; }
        public string FCOMPANY { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
