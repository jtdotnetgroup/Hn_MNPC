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
    [TableName("TB_ORGANIZE")]
    [Description("组织架构")]
    public class OrganizeModel
    {
        public string FID { get; set; }
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
        public string CREATE_OPERATOR_ID { get; set; }
        public string CREATE_OPERATOR_NAME { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string UPDATE_OPERATOR_ID { get; set; }
        public string UPDATE_OPERATOR_NAME { get; set; }
        public decimal TYPE { get; set; }
        public string FRANCHISEE_ID { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
