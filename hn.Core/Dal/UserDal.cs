using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.SqlServer;
using hn.Common.Provider;
using hn.Core.Model;
using System.Data.OracleClient;

namespace hn.Core.Dal
{
    public class UserDal : BaseRepository<User>
    {

        public  static UserDal Instance
        {
            get { return SingletonProvider<UserDal>.Instance; }
        }

        public int UpdateUserConfig(string userId,string configJson)
        {
            string sql = "update sys_users set configJson=:configJson where FID=:FID";
            return DbUtils.ExecuteNonQuery(sql, new { ConfigJson = configJson, FID = userId });
        }

        public override int Update(User o)
        {
            return DbUtils.Update(o, new[] {"fid", "password", "passsalt","configjson"});
        }

        public int UpdatePassword(string userId, string password)
        {
            string sql = "update sys_users set password=:password where FID=:FID";
            return DbUtils.ExecuteNonQuery(sql, new {Password = password, FID = userId});
        }

        public IEnumerable<Role> GetRolesBy(string userId)
        {
            string s = "select roleid from Sys_UserRoles where userid=:userid";
            DataTable dt = SqlEasy.ExecuteDataTable(s, new OracleParameter("userid", userId));

            var list = from n in RoleDal.Instance.GetAll()
                       where dt.AsEnumerable().Select(r => PublicMethod.GetString(r[0])).ToArray<string>().Contains(n.FID)
                       select n;
            return list;
        }

        public User GetUserBy(string userName)
        {
            return GetAll().ToList().Find(n => n.UserName == userName);
        }

        public User GetUser(string FID) {
            return Get(FID);
        }

        /// <summary>
        /// 为指定的用户分配角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleIds">角色ID</param>
        /// <returns></returns>
        public int AddUserTo(string userId, params string[] roleIds)
        {
            string sql = "insert into Sys_UserRoles (FID,userid,roleid) values('{0}','{1}','{2}')";
            StringBuilder sb = new StringBuilder();
            foreach (var rid in roleIds)
            {
                sb.AppendFormat(sql,Guid.NewGuid().ToString(), userId, rid);
                sb.AppendLine();
            }

            if(!string.IsNullOrEmpty(sb.ToString()))
            {
                return SqlEasy.ExecuteNonQuery(sb.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 删除用户的角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int DeleteRolesBy(string userId)
        {
            return DbUtils.ExecuteNonQuery("delete sys_userroles where userid=:userid", new {userid = userId});
        }

        /// <summary>
        /// 获取指定用户的菜单及按钮
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataTable GetNavBtnsBy(string userId)
        {
            string sql =
                "select a.*,b.ButtonTag from Sys_UserNavBtns a join sys_buttons b on a.btnid=b.FID where a.userid=:userid";
            return SqlEasy.ExecuteDataTable(sql, new OracleParameter("userid", userId));
        }

        #region 为用户设置数据访问权限

        public int ClearDepartmentsBy(int userid)
        {
            string sql = "delete Sys_Users_Departments where userid=:UserID";
            return DbUtils.ExecuteNonQuery(sql, new { UserID = userid });
        }

        public int SetDepartments(int userid, string deps)
        {
            if (string.IsNullOrEmpty(deps))
                return 0;

            string[] arrDep = deps.Split(',');

            string sql = "insert into Sys_Users_Departments (userid,depid) values({0},{1}) ";
            StringBuilder sb = new StringBuilder();
            foreach (string depid in arrDep)
            {
                sb.AppendFormat(sql, userid, depid);
                sb.AppendLine();
            }

            return sb.Length > 0 ? SqlEasy.ExecuteNonQuery(sb.ToString()) : 0;
        }


        public List<string> GetDepIDs(string userid)
        {
            List<string> list = new List<string>();
            var dr = SqlEasy.ExecuteDataReader("select depid from Sys_Users_Departments where userid=:UserID", new OracleParameter("UserID", userid));
            while (dr.Read())
            {
                list.Add(PublicMethod.GetString(dr[0]));
            }
            return list;
        }


        #endregion

        public string GetJson(int page = 1, int rows = 15, string keywords = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!keywords.IsNullOrEmpty())
            {
                query.Append(" and ( ");

                query.AppendFormat(" EMPLOYEEID like '%{0}%' ", keywords);
                query.AppendFormat(" OR USERNAME like '%{0}%' ", keywords);
                query.AppendFormat(" OR TRUENAME like '%{0}%' ", keywords);
                query.AppendFormat(" OR MOBILE like '%{0}%' ", keywords);

                query.Append(" ) ");
            }


            return base.JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }

    }
}
