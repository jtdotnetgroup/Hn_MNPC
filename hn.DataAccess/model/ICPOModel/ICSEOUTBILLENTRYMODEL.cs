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
    [TableName("ICSEOUTBILLENTRY")]
    [Description("发货通知单-明细")]
    public class ICSEOUTBILLENTRYMODEL
    {
        public string FID { get; set; }
        public string FICSEOUTID { get; set; }
        public string FSRCID { get; set; }
        public string FPRICEPOLICYENTRYID { get; set; }
        public string FICPRID { get; set; }
        public decimal FENTRYID { get; set; }
        public decimal FCOMMITQTY { get; set; }
        public decimal FHNAMOUNT { get; set; }
        public string FSTOCKNUMBER { get; set; }
        public string FSTOCK { get; set; }
        public string FSTOCKPLACE { get; set; }
        public decimal FWEIGHT { get; set; }
        public decimal FVOLUME { get; set; }
        public string FREMARK { get; set; }
        public string FBATCHNO { get; set; }
        public string FCOLORNO { get; set; }
        public string FLEVEL { get; set; }
        public string FWDR { get; set; }
        public string FITEMID { get; set; }
        public decimal FPRICE { get; set; }
        public decimal FAMOUNT { get; set; }
        public string FERR_MESSAGE { get; set; }
        public DateTime FNEEDDATE { get; set; }
        public string FORDERREMARK1 { get; set; }
        public string FORDERREMARK2 { get; set; }
        
        /// <summary>
        /// 备注描述
        /// </summary>
        public string FDESCRIPTION { get; set; }
        /// <summary>
        /// 品牌部
        /// </summary>
        public string FBRAND { get; set; }

        /// <summary>
        /// 是否托管库开单
        /// </summary>
        public string FTRUSTEESHIP { get; set; }

        /// <summary>
        /// 厂家账户
        /// </summary>
        public string FCLIENTID { get; set; }

        /// <summary>
        /// 仓位号
        /// </summary>
        public string FLOCATION_NO { get; set; }

        /// <summary>
        /// 拆单数量
        /// </summary>
        [DbField(false)]
        public decimal FDISMANTLING { get; set; }

        public int FINFO_RE_STATUS { get; set; }
        public decimal FINFO_RE_QTY { get; set; }

        public int FGROUP_STATUS { get; set; }

        /// <summary>
        /// 价格政策编号
        /// </summary>
        public string FPRICENUMBER { get; set; }

        /// <summary>
        ///tihuodan bm
        /// </summary>
        public string thdbm { get; set; }
    }
}
