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
    [TableName("TMS_PLATFORM")]
    [Description("调度平台")]
    public class PlatFormModel
    {
        public decimal ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string SHORTNAME { get; set; }
        public string LINKMAN { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string POSTCODE { get; set; }
        public string TELEPHONE { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public decimal DISABLED { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal CREATE_OPERATOR_ID { get; set; }
        public string CREATE_OPERATOR_NAME { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public decimal UPDATE_OPERATOR_ID { get; set; }
        public string UPDATE_OPERATOR_NAME { get; set; }
        public decimal FROM_ADDRESS_ID { get; set; }
        public string EXT_INFO1 { get; set; }
        public string EXT_INFO2 { get; set; }
        public string EXT_INFO3 { get; set; }
        public string EXT_INFO4 { get; set; }
        public string EXT_INFO5 { get; set; }
     
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
