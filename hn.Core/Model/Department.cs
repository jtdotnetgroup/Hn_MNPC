using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Core.Dal;

namespace hn.Core.Model
{
    [TableName("Sys_Departments")]
    [Description("部门管理")]
    public class Department
    {
        [DefaultValue(0)]
        public string FID { get; set; }

        [Description("部门名称")]
        public string DepartmentName { get; set; }

        [DefaultValue(0)]
        [Description("上级ID")]
        public string ParentId { get; set; }

        [DefaultValue(0)]
        [Description("排序")]
        public int Sortnum { get; set; }

        [DefaultValue(false)]
        [Description("是否是业务部门")]
        public bool IsBizDepartment { get; set; }

        [Description("备注")]
        public string Remark { get; set; }

        [DbField(false)]
        public IEnumerable<Department> children
        {
            get { return DepartmentDal.Instance.GetChildren(FID); }
        }

        /// <summary>
        /// tree 节点状态
        /// </summary>
        [DbField(false)]
        public string state
        {
            get
            {
                if (ParentId == "0")
                    return "open";
                return children.Any() ? "closed" : "open";
            }
        }
    }
}
