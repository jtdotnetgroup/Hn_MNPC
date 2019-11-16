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
    [TableName("base_Employee")]
    [Description("职员")]
    public class EmployeeModel
    {
        public string FID { get; set; }
        [Description("员工工号")]
        public string FEmployeeNo { get; set; }
        [Description("姓名")]
        public string FName { get; set; }
        [Description("性别")]
        public string FSex { get; set; }
        [Description("是否离职")]
        public bool FIsLeave { get; set; }
        [Description("电子邮箱")]
        public string FEmail { get; set; }
        [Description("部门Id")]
        public string FDepartmentID { get; set; }
        [Description("手机号码1")]
        public string FTel1 { get; set; }
        [Description("手机号码2")]
        public string FTel2 { get; set; }
        [Description("QQ号码")]
        public string FQQ { get; set; }
        [Description("出生日期")]
        public string FBirthday { get; set; }
        [Description("籍贯")]
        public string FHail { get; set; }
        [Description("入职日期")]
        public string FJoinDate { get; set; }
        [Description("备注")]
        public string FRemark { get; set; }

        [DbField(false)]
        public Department Department
        {
            get { return DepartmentDal.Instance.Get(FDepartmentID); }
        }
     
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
