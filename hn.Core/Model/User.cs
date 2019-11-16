using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
namespace hn.Core.Model
{
    [Serializable]
    [TableName("sys_users")]
    [Description("系统用户")]
    public class User
    {
        public string FID { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public string FOrgID { get; set; }


        /// <summary>
        /// 所属部门
        /// </summary>
        public string FDepartment { get; set; }

        //[Description("密码佐料")]
        //public string PassSalt { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int FGender { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public decimal IsDisabled { get; set; }

        /// <summary>
        /// 超管用户
        /// </summary>
        public decimal IsAdmin { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public decimal FIsSystem { get; set; }

        /// <summary>
        /// 域登录
        /// </summary>
        public string FAreaLogin { get; set; }

        /// <summary>
        /// 是否启用域登陆验证
        /// </summary>
        public int IS_DOMAIN { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public bool FIsLeaving { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime FLeavingTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime FUpdateTime { get; set; }

        /// <summary>
        /// 菜单JSon
        /// </summary>
        public string MenusJson { get; set; }

        /// <summary>
        /// 配置JSon
        /// </summary>
        public string ConfigJson { get; set; }

        ///// <summary>
        ///// 用户名
        ///// </summary>
        //[Description("用户名")]
        //public string UserName { get; set; }
        //[Description("密码")]
        //public string Password { get; set; }
        //[Description("真实姓名")]
        //public string TrueName { get; set; }
        //[Description("密码佐料")]
        //public string PassSalt { get; set; }

        //public string Email { get; set; }
        //[Description("是否超管")]
        //public decimal IsAdmin { get; set; }
        //[Description("是否禁用")]
        //public decimal IsDisabled { get; set; }
        ////[Description("部门Id")]
        ////public string DepartmentId { get; set; }
        ////[Description("关联职员")]
        ////public string EmployeeID { get; set; }
        //[Description("手机")]
        //public string Mobile { get; set; }

        //public string QQ { get; set; }
        //[Description("备注")]
        //public string Remark { get; set; }
        //[Description("个性化设置")]
        //public string ConfigJson { get; set; }

        //public string ORGANIZE_ID { get; set; }
        //public string ORGANIZE_NAME { get; set; }

        //[DbField(false)]
        //public Department Department
        //{
        //    get { return DepartmentDal.Instance.Get(DepartmentId); }
        //}
        public string LINKMAN { get; set; }
        public string LINKPHONE { get; set; }
        [DbField(false)]
        public List<Navigation> Navigations { get; set; }

        [DbField(false)]
        public IEnumerable<Role> Roles
        {
            get { return UserDal.Instance.GetRolesBy(FID); }
        }

        /// <summary>
        /// 用户可以访问的部门列表
        /// </summary>
        [DbField(false)]
        public string Departments
        {
            get { return string.Join(",", UserDal.Instance.GetDepIDs(FID)); }
        }


        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
