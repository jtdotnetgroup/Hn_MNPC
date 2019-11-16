using hn.Common;
using hn.Common.Data;
using hn.Core.Dal;
using hn.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("sys_roles")]
   public class TB_TreeUserModel
    {
        [DefaultValue(0)]
        public string FID { get; set; }

        [Description("角色名称")]
        public string RoleName { get; set; }

        [DefaultValue(0)]
        [Description("排序")]
        public int Sortnum { get; set; }
        [Description("描述")]
        public string Remark { get; set; }

        [Description("是否为默认角色")]
        public int IsDefault { get; set; }

        [DbField(false)]
        public IEnumerable<Navigation> Navigations { get; set; }


        [DbField(false)]
        public IEnumerable<User> children
        {
            get { return RoleDal.Instance.GetUserBy(FID); }
        }

        /// <summary>
        /// 角色可以访问的部门列表
        /// </summary>
        [DbField(false)]
        public string Departments
        {
            get { return RoleDal.Instance.GetDepIDs(FID); }
        }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
