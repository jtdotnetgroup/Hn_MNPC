using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
using hn.Core.Model;
using hn.Common.Data.Filter;
using hn.Common.Provider;
using System.Data;
using System.Data.SqlClient;
using hn.Common.Data.SqlServer;
using System.Data.OracleClient;

namespace hn.Core.Dal
{
    public class RoleDal : BaseRepository<Role>
    {

        public static RoleDal Instance
        {
            get { return SingletonProvider<RoleDal>.Instance; }
        }

        public DataTable GetNavBtnsBy(params string[] roleid)
        {
            var roleids = roleid.Aggregate("", (current, i) => current + (i.ToString() + ",")).TrimEnd(',');
            string[] array = roleids.Split(',');
            List<string> ids = new List<string>();
            foreach(string id in array)
            {
                ids.Add("'" + id + "'");
            }

            string sql = "select a.*,b.ButtonTag from sys_roleNavBtns a join sys_buttons b on a.btnid=b.FID where a.roleid in ("+string.Join(",",ids.ToArray())+")";
            return SqlEasy.ExecuteDataTable(sql);
        }


        public int SetDefaultRole(string roleid)
        {
            DbUtils.ExecuteNonQuery("update sys_roles set isdefault=0", null);
            return DbUtils.ExecuteNonQuery("update sys_roles set isdefault=1 where FID=:FID",new {FID=roleid} );
        }

        public string JsonDataForEasyUIdataGrid(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage("sys_roles")
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = sortorder.Trim(),
                SQL_WHERE_IN = FilterTranslator.ToSql(filterJson)
            };
            int recordCount;
            DataTable dt = base.GetPageWithSp(pcp, out recordCount);
            dt.Columns.Add(new DataColumn("Departments")); //可以访问的部门数据

            var rolelist = RoleDal.Instance.GetAll();

            foreach (DataRow row in dt.Rows)
            {
                row["Departments"] = rolelist.First(n => n.FID == PublicMethod.GetString(row["FID"])).Departments;
            }


            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, dt);

        }


        public IEnumerable<User> GetUserBy(string FID)
        {
            string s = "select userid from Sys_UserRoles where ROLEID =:FID";
            DataTable dt = SqlEasy.ExecuteDataTable(s, new OracleParameter("FID", FID));

            var list = from n in UserDal.Instance.GetAll()
                       where dt.AsEnumerable().Select(r => PublicMethod.GetString(r[0])).ToArray<string>().Contains(n.FID)
                       select n;
            return list;
        }


        #region 为角色设置数据访问权限

        public int ClearDepartmentsBy(string roleid)
        {
            string sql = "delete Sys_Roles_Departments where roleid=:RoleId";
            return DbUtils.ExecuteNonQuery(sql, new {RoleId = roleid});
        }

        public int SetDepartments(string roleid, string deps)
        {
            if (string.IsNullOrEmpty(deps))
                return 0;

            string[] arrDep = deps.Split(',');

            string sql = "insert into Sys_Roles_Departments (roleid,depid) values({0},{1}) ";
            StringBuilder sb = new StringBuilder();
            foreach (string depid in arrDep)
            {
                sb.AppendFormat(sql, roleid, depid);
                sb.AppendLine();
            }

            return sb.Length > 0 ? SqlEasy.ExecuteNonQuery(sb.ToString()) : 0;
        }

        public List<Department> GetDepsBy(int roleid)
        {
            string sql =
                "select a.* from Sys_Departments a join Sys_Roles_Departments b on a.depid=b.FID where b.roleid=:roleid";

            return DbUtils.GetList<Department>(sql,new {RoleID = roleid}).ToList();
        }

        public string GetDepIDs(string roleid)
        {
            string temp = "";
            var dr = SqlEasy.ExecuteDataReader("select depid from Sys_Roles_Departments where roleid=:RoleID",new OracleParameter("RoleID",roleid));
            while (dr.Read())
            {
                temp += dr.GetString(0) + ",";
            }
            return temp.TrimEnd(',');
        }
                                             

        #endregion

    }
}
