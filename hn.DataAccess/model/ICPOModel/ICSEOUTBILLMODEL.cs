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
    [TableName("ICSEOUTBILL")]
    [Description("发货通知单")]
    public class ICSEOUTBILLMODEL
    {
        public string JYDWNAME { get; set; }
        public string JYDWID { get; set; }
        public string FID { get; set; }
        public string FPREMISEID { get; set; }
        public string FCLIENTID { get; set; }
        public string FBRANDID { get; set; }
        public string FBILLNO { get; set; }
        public string FCARNUMBER { get; set; }
        public string FLOADCAPACITY { get; set; }
        public string FDELIVERER { get; set; }
        public string FDELIVERERTEL { get; set; }
        public string FDELIVERERIDNO { get; set; }
        public string FDELIVERERADDR { get; set; }
        public string FRECEIVER { get; set; }
        public string FRECEIVERTEL { get; set; }
        public string FRECEIVERADDR { get; set; }
        public decimal FALLWEIGHT { get; set; }
        public decimal FALLVOLUME { get; set; }
        public string FBILLERID { get; set; }
        public DateTime FBILLDATE { get; set; }
        public decimal FSTATUS { get; set; }
        public string FCHECKERID { get; set; }
        public DateTime FCHECKDATE { get; set; }
        public string FREMARK { get; set; }
        public string FTRANSTYPE { get; set; }
        public string FTRANSID { get; set; }
        public DateTime FDELIVERDATE { get; set; }
        public decimal FFACTORYSTATUS { get; set; }
        public decimal FSYNCSTATUS { get; set; }

        public string FCENTER_WAREHOUSE { get; set; }
        public int FIS_CONSIGN { get; set; }
        public string FDELIVERY_METHOD { get; set; }
        public string FEXPRESSCOMPANYID { get; set; }
        public string FPROJECTNAME { get; set; }
        /// <summary>
        /// 采购订单
        /// </summary>
        public string FPURCHASE_NO { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string FPLANDESC { get; set; }

        /// <summary>
        /// 厂家单据编号
        /// </summary>
        public string FSRCBILLNO { get; set; }

        ///// <summary>
        ///// 厂家同步时间
        ///// </summary>
        //public DateTime FSYNCTIME { get; set; }

        /// <summary>
        /// 开单类型
        /// </summary>
        public decimal FBILLING_TYPE { get; set; }

        /// <summary>
        /// 租柜编号
        /// </summary>
        public string FGROUP_NO { get; set; }

        /// <summary>
        /// 结算组织
        /// </summary>
        public string FSETTLE_ORG { get; set; }

        /// <summary>
        /// 销区发货要求
        /// </summary>
        public string FDELIVERY_REQUIRE { get; set; }

        /// <summary>
        /// 品牌部
        /// </summary>
        public string FBRAND_DEPART { get; set; }

        /// <summary>
        /// 计划信息
        /// </summary>
        public string FPLAN_INFO { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string FIDENTIFICATION { get; set; }

        public string FCOMPANY { get; set; }
        public string FCOMPANY_NO { get; set; }
        public int FISTICKET { get; set; }
        public string FTRANSPORT_PRICE_TYPE { get; set; }
        public string FDELIVERY_TYPE { get; set; }
        public DateTime FREQUEST_DELIVERY_DATE { get; set; }
        public DateTime FESTIMATED_DELIVERY_DATE { get; set; }
        public int FIS_SIGN_BACK { get; set; }
        public string FFREIGHTID { get; set; }
        public string FCLIENTELE_PHONE { get; set; }

        public string FPROVINCEID { get; set; }
        public string FCITYID { get; set; }
        public string FDISTRICTID { get; set; }
        public string FCOUNTYID { get; set; }
        public string FCONSIGNEE { get; set; }
        public string FCONSIGNEE_TEL { get; set; }
        public string FDELIVER_BASE_ID { get; set; }
        public int FCAR_STATUS { get; set; }
        public int FIS_PRICE { get; set; }

        public decimal FTOTALVALUE { get; set; }
        public decimal FPUBLISH_COUNT { get; set; }

        public decimal FACTUAL_WEIGHT { get; set; }
        public decimal FACTUAL_VOLUME { get; set; }

        public string FPRICE_BATCHNO { get; set; }
        public int IS_REPLENISH { get; set; }
        public int FPRICE_BY_OFFLINE { get; set; }

        public string FCARNUMBER_CHANGE { get; set; }

        public int FCARNUMBER_CHANGE_COUNT { get; set; }

        public decimal FSTANDARD_FREIGHT { get; set; }

        public string DRIVERNAME_CHANGE { get; set; }
        public string DRIVERTEL_CHANGE { get; set; }

        public decimal FFREIGHT_PRICE { get; set; }
         
        [DbField(false)]
        public string FDELIVERDATESTR
        {
            get
            {
                return FDELIVERDATE != null ? FDELIVERDATE.ToString("yyyy-MM-dd") : "";
            }
        }
    }
}
