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
    [TableName("TB_APP_USER")]
    [Description("APP用户表")]
    public class AppUserModel
    {
        public string FID { get; set; }
        public string PHONE { get; set; }
        public string PASSWORD { get; set; }
        public string NAME { get; set; }
        public string SEX { get; set; }
        public DateTime ADD_DATE { get; set; }
        public decimal IS_DISABLE { get; set; }
        public DateTime LAST_LOGIN_TIME { get; set; }
        public decimal STATUS { get; set; }
        public decimal DRIVER_ID { get; set; }
        public string MASTER_ID { get; set; }
        public string DRIVER_NAME { get; set; }
        public string MASTER_NAME { get; set; }
        public decimal IS_MODIFY_PWD { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
