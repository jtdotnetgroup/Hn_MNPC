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
    [TableName("TB_FRANCHISEE")]
    [Description("加盟商")]
    public class FranchiseeModel
    {
        public string FID { get; set; }
        public string COMPANY_NAME { get; set; }
        public string REPRESENTATIVE { get; set; }
        public string ID_NUMBER { get; set; }
        public string LINK_MAN { get; set; }
        public string PROVINCE_ID { get; set; }
        public string CITY_ID { get; set; }
        public string DISTRICT_ID { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string CITY_NAME { get; set; }
        public string DISTRICT_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ZIP_CODE { get; set; }
        public string TEL { get; set; }
        public string CHAT_ID { get; set; }
        public string SERVICE_ITEM { get; set; }
        public string SERVICE_AREA { get; set; }
        public string HAVE_WAREHOUSE { get; set; }
        public decimal MASTER_COUNT { get; set; }
        public decimal CAR_COUNT { get; set; }
        public string CAR_TYPE { get; set; }
        public decimal DRIVER_COUNT { get; set; }
        public string HAVE_TOOLS { get; set; }
        public string HAVE_INSURANCE { get; set; }
        public string BANK_NAME { get; set; }
        public string BANK_ACCOUNT { get; set; }
        public string BANK_USERNAME { get; set; }
        public string REMARK { get; set; }
        public string BUSINESS_LICENSE { get; set; }

        public string CREATE_USER_ID { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string AUDIT_USER_ID { get; set; }
        public DateTime AUDIT_TIME { get; set; }
        public string AUDIT_USER { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
