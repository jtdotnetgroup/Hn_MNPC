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
    [TableName("V_ICSEOUTBILL")]
    [Description("发货通知单")]
    public class V_ICSEOUTBILLMODEL : ICSEOUTBILLMODEL
    {
        //public string FPREMISENAME { get; set; }
        //public string FCLASSAREA2 { get; set; }
        //public string FPREMISEBRANDID { get; set; }
        public string JYDWNAME { get; set; }
        public string JYDWID { get; set; }
        public bool FCHECK { get; set; }
        public string FCLASSAREA2NAME { get; set; }
        public string FPREMISEBRANDNAME { get; set; }
        public string FCLIENTNAME { get; set; }
        public string FBRANDNAME { get; set; }
        public string FTRANSNAME { get; set; }
        public string FEXPRESSCOMPANYNAME { get; set; }
        public string FEXPRESSCOMPANYCODE { get; set; }
        public string FDELIVERY_METHODNAME { get; set; }
        public string FDELIVERY_METHODNUMBER { get; set; }
        public string FCHECKERNAME { get; set; }
        public string FACCOUNT { get; set; }

        public string FBASEA_NAME { get; set; }
        public string FRECEIVER_PROVINCE_NAME { get; set; }
        public string FRECEIVER_CITY_NAME { get; set; }
        public string FRECEIVER_DISTRICT_NAME { get; set; }
        public string FRECEIVER_COUNTY_NAME { get; set; }
        public string FTRANSPORT_PRICE_TYPE_NAME { get; set; }
        public string FFREIGHT_NAME { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [DbField(false)]
        public string FSTATUSNAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.BILL_FSTATUS), FSTATUS.ToInt());
            }
        }

        /// <summary>
        /// 约车状态名称
        /// </summary>
        [DbField(false)]
        public string FCAR_STATUS_NAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.CAR_STATUS), FCAR_STATUS.ToInt());
            }
        }

        /// <summary>
        /// 约车状态名称
        /// </summary>
        [DbField(false)]
        public string FSYNCSTATUS_NAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.BILL_SYNStatus), FSYNCSTATUS.ToInt());
            }
        }

    }
}
