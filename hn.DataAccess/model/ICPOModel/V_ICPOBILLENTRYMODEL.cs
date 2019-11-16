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
    [TableName("V_ICPOBILLENTRY")]
    [Description("采购订单明细")]
    public class V_ICPOBILLENTRYMODEL: ICPOBILLENTRYMODEL
    {

        /// <summary>
        /// 产品系列
        /// </summary>
        public string FPRODUCTTYPE { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        public string FPRODUCTCODE { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string FPRODUCTNAME { get; set; }


        /// <summary>
        /// 产品型号
        /// </summary>
        public string FMODEL { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public string FUNITID { get; set; }

        /// <summary>
        /// 采购单位名称
        /// </summary>
        public string FUNITNAME { get; set; }

        public string FORDERUNIT { get; set; }

        /// <summary>
        /// 基本单位名称
        /// </summary>
        public decimal FASKQTY { get; set; }

        [DbField(false)]
        public int cjkcs { get; set; }
        public string FNEEDDATESTR { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DbField(false)]
        public string FSTATUSNAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.BILL_FSTATUS), FSTATUS);
            }
        }

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



        public string PZ
        {
            get
            {
                if (string.IsNullOrEmpty(FSRCCODE)) return "";
                string[] arrCode = FSRCCODE.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrCode.Length == 3) return arrCode[0];
                else return "";
            }

        }

        public string XH
        {
            get
            {
                if (string.IsNullOrEmpty(FSRCCODE)) return "";
                string[] arrCode = FSRCCODE.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrCode.Length == 3) return arrCode[1];
                else return "";
            }

        }

        public string GG
        {
            get
            {
                if (string.IsNullOrEmpty(FSRCCODE)) return "";
                string[] arrCode = FSRCCODE.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrCode.Length == 3) return arrCode[2];
                else return "";
            }

        }

    }
}
