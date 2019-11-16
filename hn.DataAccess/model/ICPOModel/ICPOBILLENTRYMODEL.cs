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
    [TableName("ICPOBILLENTRY")]
    [Description("采购订单明细")]
    public class ICPOBILLENTRYMODEL
    {


        public string ICPRBILLENTRYIDS { get; set; }

        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 采购订单ID
        /// </summary>
        public string FICPOBILLID { get; set; }

        /// <summary>
        /// 请购明细ID
        /// </summary>
        public string FPLANID { get; set; }

   

       

        /// <summary>
        /// 批号
        /// </summary>
        public string FBATCHNO { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal FPRICE { get; set; }

        /// <summary>
        /// 参考数量
        /// </summary>
        public decimal FADVQTY { get; set; }


        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal FSRCQTY { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal FSRCCOST { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        public int FSTATUS { get; set; }


        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime FNEEDDATE { get; set; }

        /// <summary>
        /// 厂家确认数量
        /// </summary>
        public decimal FCOMMITQTY { get; set; }

     

        /// <summary>
        /// 启用状态
        /// </summary>
        public int FSTATE { get; set; }

        #region 蒙厂需要的字段

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

        [DbField(false)]
        public string PZ { get {
                if (string.IsNullOrEmpty(FSRCCODE)) return "";
                string[] arrCode = FSRCCODE.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrCode.Length == 3) return arrCode[0];
                else return "";
            }

        }
        [DbField(false)]
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
        [DbField(false)]
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

        public string THDBMDETAIL { get; set; }

        #endregion

    }
}
