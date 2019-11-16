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
    [TableName("TB_MASTER")]
    [Description("技师表")]
    public class MasterModel
    {
        public string FID { get; set; }
        public string NAME { get; set; }
        public string PHONE { get; set; }
        public string SEX { get; set; }
        public decimal PLAT_FORM_ID { get; set; }
        public string PLAT_FORM_NAME { get; set; }
        public string SKILL { get; set; }
        public string SKILL_NAME { get; set; }
        public string IDENTITY_CARD { get; set; }
        public string CITY { get; set; }
        public string ORGANIZE_ID { get; set; }
        public string ORGANIZE_NAME { get; set; }
        public string WORK_TYPE { get; set; }
        public string WORK_TYPE_DISPLAY { get; set; }
        public string SERVICE_PROVINCE { get; set; }
        public string SERVICE_CITY { get; set; }
        public string SERVICE_DISTRICT { get; set; }
        public string BANK_ACCOUNT { get; set; }
        public string BANK_NAME { get; set; }
        public string BANK_NUMBER { get; set; }
        public string SOS_LINK_MAN { get; set; }
        public string RELATIONSHIP { get; set; }
        public string LINK_PHONE { get; set; }
        public string SERVICE_PROVINCE_DISPLAY { get; set; }
        public string SERVICE_CITY_DISPLAY { get; set; }
        public string SERVICE_DISTRICT_DISPLAY { get; set; }
        public decimal STATUS { get; set; }
        public string REASON { get; set; }

        public string AGE { get; set; }
        public string CHAT { get; set; }
        public string HAVE_CAR { get; set; }
        public string HAVE_TEAM { get; set; }
        public decimal TEAM_NUM { get; set; }
        public string HAVE_TOOLS { get; set; }
        public string HAVA_INSURANCE { get; set; }
        public string REMARK { get; set; }

        public string CREATE_USER_ID { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string AUDIT_USER_ID { get; set; }
        public DateTime AUDIT_TIME { get; set; }
        public string AUDIT_USER { get; set; } 

        public string IMAGE_URL1 { get; set; }
        public string IMAGE_URL2 { get; set; }
        public string IMAGE_URL3 { get; set; }

        public string REFEREE { get; set; }
        public string REFEREE_PHONE { get; set; }
        public string MASTER_NO { get; set; }

        public string CAR_TYPE { get; set; }

        //技师来源 自有技师，加盟技师
        public string MASTER_FROM { get; set; }
        //支行
        public string BRANCH { get; set; }
        //认证状态
        public int CERTIFICATION_STATUS { get; set; }
        //岗中技能
        public string WORK_SKILL { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
