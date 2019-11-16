using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.Filter;
using hn.Common.Provider;
using hn.Core.Dal;
using hn.Core.Model;
using System.Data.SqlClient;
using hn.Common.Data.SqlServer;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Bll
{
    public class EmployeeBll
    {
        // 浏览默认权限，有此权限方可访问页面
        private const int browser = 18; 

        public static EmployeeBll Instance
        {
            get { return SingletonProvider<EmployeeBll>.Instance; }
        }

        /// <summary>
        /// 获部门树，easyui Tree JSON data
        /// </summary>
        /// <returns></returns>
        public string GetDepartmentTreeData()
        {
            return DepartmentBll.Instance.GetDepartmentTreegridData().Replace("FID", "id").Replace("DepartmentName", "text");
        }

        public EmployeeModel GetEmployee(string EmployeeId)
        {
            return EmployeeDal.Instance.Get(EmployeeId);
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="Employeename">用户名</param>
        /// <param name="EmployeeId">用户Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasEmployeeNo(string employeeno, string EmployeeId = "0")
        {
            var Employees = from n in EmployeeDal.Instance.GetAll()
                            where n.FEmployeeNo == employeeno && n.FID != EmployeeId
                        select n;
            return Employees.Any();
        }

        public string AddEmployee(EmployeeModel u)
        {
            string uid = "0";
            string msg = "用户添加失败！";
            if (HasEmployeeNo(u.FEmployeeNo, u.FID))
            {
                uid = "-2";
                msg = "员工编号已存在。";
            }
            else
            {
                uid = EmployeeDal.Instance.Insert(u);
                if (uid != "")
                {
                    msg = "添加新员工成功！";
                    LogBll<EmployeeModel> log = new LogBll<EmployeeModel>();
                    u.FID = uid;
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid !="" }.ToString();
        }

        public string EditEmployee(EmployeeModel u)
        {
            int k;
            string msg = "用户编辑失败。";
            if (HasEmployeeNo(u.FEmployeeNo, u.FID))
            {
                k = -2;
                msg = "员工编号已存在。";
            }
            else
            {
                var oldEmployee = EmployeeDal.Instance.Get(u.FID);
                k = EmployeeDal.Instance.Update(u);
                if (k > 0)
                {
                    msg = "员工编辑成功。";
                    LogBll<EmployeeModel> log = new LogBll<EmployeeModel>();
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public string Delete(string FID)
        {
            int k= EmployeeDal.Instance.Delete(FID);

            return new JsonMessage { Data = k.ToString(), Message = "", Success = k > 0 }.ToString();
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return EmployeeDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetJsonData(int pageindex,int pagesize,string filterJson,string sort,string order)
        {
            var pcp = new ProcCustomPage("base_Employee")
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = sort + " " + order,
                SQL_WHERE_IN = FilterTranslator.ToSql(filterJson)
            };
            var Employees = EmployeeDal.Instance.GetAll();
            int recordCount;
            DataTable dt = EmployeeDal.Instance.GetPageWithSp(pcp, out recordCount);
            dt.Columns.Add(new DataColumn("DepartmentName"));
                   
            var departments = DepartmentDal.Instance.GetAll().ToList();
            foreach (DataRow row in dt.Rows)
            {
                var dep = departments.Where(n => row != null && n.FID == (string)row["FDepartmentID"]);
                var enumerable = dep as Department[] ?? dep.ToArray();
                if (enumerable.Any())
                    row["DepartmentName"] = enumerable.First().DepartmentName;
                else
                {
                    row["DepartmentName"] = "";
                }
            }
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, dt);
        }

    
    }
}
