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
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using hn.Common.Data.SqlServer;
using Omu.ValueInjecter;
using System.Data.OracleClient;
namespace hn.Core.Bll
{
    public class UserBll
    {
        // 浏览默认权限，有此权限方可访问页面
        private const string browser = "18";

        public static UserBll Instance
        {
            get { return SingletonProvider<UserBll>.Instance; }
        }

        /// <summary>
        /// 检查用户登录状态
        /// </summary>
        public void CheckUserOnlingState()
        {
            if (SysVisitor.Instance.IsGuest)
            {
                HttpContext.Current.Response.Write(
                    new JsonMessage { Success = false, Data = "-99", Message = "登录已过期，请重新登录" }.ToString()
                    );
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 获部门树，easyui Tree JSON data
        /// </summary>
        /// <returns></returns>
        public string GetDepartmentTreeData()
        {
            return DepartmentBll.Instance.GetDepartmentTreegridData().Replace("FID", "id").Replace("DepartmentName", "text");
        }

        /// <summary>
        /// 获取所有角色JSON数据
        /// </summary>
        /// <returns></returns>
        public string GetAllRoles()
        {
            return JSONhelper.ToJson(RoleDal.Instance.GetAll());
        }

        public User GetUser(string userId)
        {
            return UserDal.Instance.Get(userId);
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="userId">用户Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasUserName(string username, string userId = "0")
        {
            var users = from n in UserDal.Instance.GetAll()
                        where n.UserName == username && n.FID != userId
                        select n;
            return users.Any();
        }

        public string AddUser(User u, string roleIds)
        {
            string uid = "";
            string msg = "用户添加失败！";
            if (HasUserName(u.UserName, u.FID))
            {
                uid = "-2";
                msg = "用户名已存在。";
            }
            else
            {
                u.ConfigJson = "{\"theme\":{\"title\":\"默认皮肤\",\"name\":\"default\"},\"showType\":\"tree\",\"gridRows\":\"20\"}";
                u.Password = StringHelper.MD5string(u.Password/* + u.PassSalt*/);
                uid = UserDal.Instance.Insert(u);
                if (!string.IsNullOrEmpty(roleIds))
                {
                    var roleIdArr = roleIds.Split(',');
                    var roleIdList = roleIdArr.Select(n => PublicMethod.GetString(n)).ToArray();
                    UserDal.Instance.AddUserTo(uid, roleIdList);
                }

                if (uid != "")
                {
                    msg = "添加新用户成功！";
                    LogBll<User> log = new LogBll<User>();
                    u.FID = uid;
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        }

        public string EditUser(User u)
        {
            int k;
            string msg = "用户编辑失败。";
            if (HasUserName(u.UserName, u.FID))
            {
                k = -2;
                msg = "用户名已存在。";
            }
            else
            {
                var oldUser = UserDal.Instance.Get(u.FID);
                k = UserDal.Instance.Update(u);
                if (k > 0)
                {
                    msg = "用户编辑成功。";
                    LogBll<User> log = new LogBll<User>();
                    log.IgnoreFields(new[] { "FID", "password", "passsalt", "configjson" }).UpdateLog(oldUser, u);
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public string DeleteUser(string userId)
        {
            string msg = "用户删除失败！";
            User u = UserDal.Instance.Get(userId);
            int k = 0;
            if (u != null)
            {
                if (u.UserName.ToLower() == "admin")
                    msg = "系统内置帐号不能删除";
                else
                {
                    //删除用户时，同时用户的角色
                    UserDal.Instance.DeleteRolesBy(userId);
                    k = UserDal.Instance.Delete(userId);
                    if (k > 0)
                    {
                        msg = "用户删除成功。";
                    }
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public string GetJsonData(int pageindex, int pagesize, string filterJson, string sort, string order)
        {
            var pcp = new ProcCustomPage("v_user")
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = (sort + " " + order).Trim(),
                SQL_WHERE_IN = FilterTranslator.ToSql(filterJson)
            };
            //var users = UserDal.Instance.GetAll();
            int recordCount;
            if (pcp.SQL_WHERE_IN == "()")
            {
                pcp.SQL_WHERE_IN = "";
            }
            DataTable dt = UserDal.Instance.GetPageWithSp(pcp, out recordCount);
            dt.Columns.Add(new DataColumn("depname"));
            dt.Columns.Add(new DataColumn("Departments")); //可以访问的部门数据

            //var departments = DepartmentDal.Instance.GetAll().ToList();
            //foreach (DataRow row in dt.Rows)
            //{
            //    var row1 = row;
            //    var dep = departments.Where(n => row1 != null && n.FID == PublicMethod.GetString(row1["departmentid"]));
            //    var enumerable = dep as Department[] ?? dep.ToArray();
            //    if (enumerable.Any())
            //        row["depname"] = enumerable.First().DepartmentName;
            //    else
            //    {
            //        row["depname"] = "";
            //    }

            //    var userList = users as IList<User> ?? users.ToList();
            //    row["Departments"] = userList.First(n => n.FID == PublicMethod.GetString(row["FID"])).Departments;
            //}
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, dt);
        }

        /// <summary>
        /// 修改帐号密码
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="newpasswd">新密码</param>
        /// <returns></returns>
        public int EditPassword(string userId, string newpasswd)
        {
            if (!string.IsNullOrEmpty(newpasswd))
            {
                User u = UserDal.Instance.Get(userId);
                if (u != null)
                {
                    var newMD5Pass = StringHelper.MD5string(newpasswd/* + u.PassSalt*/);
                    return UserDal.Instance.UpdatePassword(userId, newMD5Pass);
                }
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="oldPass">旧密码</param>
        /// <param name="newPass">新密码</param>
        /// <returns></returns>
        public string EditPassword(string userId, string oldPass, string newPass)
        {
            string msg = "密码修改失败！";
            User u = UserDal.Instance.Get(userId);
            int k = 0;
            if (u != null)
            {
                string oldMd5Pass = StringHelper.MD5string(oldPass/* + u.PassSalt*/);
                if (string.Equals(oldMd5Pass, u.Password, StringComparison.CurrentCultureIgnoreCase))
                {
                    var newMD5Pass = StringHelper.MD5string(newPass/* + u.PassSalt*/);
                    k = UserDal.Instance.UpdatePassword(userId, newMD5Pass);
                    if (k > 0)
                    {
                        msg = "密码修改成功。";
                    }
                }
                else
                {
                    msg = "旧密码不正确。";
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        /// <summary>
        /// 为指定的用户分配角色
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <param name="roleIds">角色</param>
        /// <returns></returns>
        public int AddUserToRoles(string userid, string roleIds)
        {
            UserDal.Instance.DeleteRolesBy(userid);
            if (string.IsNullOrEmpty(roleIds))
                return 1;

            var roleIdArr = roleIds.Split(',');
            var roleIdList = roleIdArr.Select(n => PublicMethod.GetString(n)).ToArray();
            return UserDal.Instance.AddUserTo(userid, roleIdList);
        }


        public string GetRolesBy(string userId)
        {
            var roles = UserDal.Instance.GetRolesBy(userId);
            return JSONhelper.ToJson(roles);
        }

        #region 获取用户拥有的菜单及按钮

        private DataTable GetNavBtns(string userId)
        {
            var roles = UserDal.Instance.GetRolesBy(userId);
            var enumerable = roles as Role[] ?? roles.ToArray();
            var roleIds = enumerable.Any() ? enumerable.Select(n => n.FID).ToArray() : new[] { "0" };

            DataTable roleNavBtns = RoleDal.Instance.GetNavBtnsBy(roleIds);

            DataTable userNavBtns = UserDal.Instance.GetNavBtnsBy(userId);

            var navBtns = roleNavBtns.Clone();

            navBtns.Columns.Remove("roleid");
            navBtns.Columns.Remove("FID");

            foreach (DataRow rnb in roleNavBtns.Rows)
            {
                var dr = navBtns.NewRow();
                dr["navid"] = rnb["navid"];
                dr["btnid"] = rnb["btnid"];
                dr["ButtonTag"] = rnb["ButtonTag"];
                navBtns.Rows.Add(dr);
            }

            foreach (DataRow unb in userNavBtns.Rows)
            {
                if (navBtns.Select("navid=" + unb["navid"] + " and btnid=" + unb["btnid"]).Length != 0) continue;
                var dr = navBtns.NewRow();
                dr["navid"] = unb["navid"];
                dr["btnid"] = unb["btnid"];
                dr["ButtonTag"] = unb["ButtonTag"];
                navBtns.Rows.Add(dr);
            }

            return navBtns;
        }

        public List<Button> GetPageButtons(string userId, string navId)
        {
            var user = UserDal.Instance.Get(userId);
            if (user == null)
                return new List<Button>();

            if (user.IsAdmin == 1 && navId != "")
            {
                return NavigationDal.Instance.Get(navId).Buttons.ToList();
            }

            var userNavBtns = GetNavBtns(userId).AsEnumerable();
            if (userNavBtns.Any())
            {
                var btnIds =
                    userNavBtns.Where(row => (string)row["navid"] == navId).Select(row => (string)row["btnid"]).ToArray();

                return ButtonDal.Instance.GetAll().ToList().FindAll(n => btnIds.Contains(n.FID));
            }

            return new List<Button>();
        }


        /// <summary>
        /// 获取用户拥有的菜单及按钮
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public string GetNavBtnsJson(string userId)
        {
            var navBtns = GetNavBtns(userId);
            return RoleBll.Instance.GetRoleNavBtns(navBtns.AsEnumerable(), "0").ToString().Replace("icon ", "");
        }

        #endregion

        #region 获取指定用户拥有的导航菜单
        /// <summary>
        /// 获取指定用户拥有的导航菜单
        /// </summary>
        /// <param name="u">用户</param>
        /// <returns></returns>
        public IEnumerable<Navigation> GetNavsBy(User u)
        {
            if (u != null)
            {
                var userId = u.FID;
                var allNavs = NavigationDal.Instance.GetAll();

                if (u.IsAdmin == 1) //如果是超管则返回所有导航菜单
                    return allNavs;

                DataTable userNavBtns = UserDal.Instance.GetNavBtnsBy(userId);
                var navArr = new string[] { };
                //用户拥有的角色
                var roles = UserDal.Instance.GetRolesBy(userId);
                var enumerable = roles as Role[] ?? roles.ToArray();
                var roleIds = enumerable.Any() ? enumerable.Select(n => n.FID).ToArray() : new[] { "0" };
                DataTable roleNavBtns = RoleDal.Instance.GetNavBtnsBy(roleIds);
                navArr = roleNavBtns.AsEnumerable().Select(n => PublicMethod.GetString(n["navid"])).ToArray();

                if (userNavBtns.Rows.Count > 0)
                {
                    var userNavArr = userNavBtns.AsEnumerable().Select(n => PublicMethod.GetString(n["navid"])).ToArray();
                    if (navArr.Any())
                    {
                        foreach (string i in userNavArr.Where(i => !navArr.Contains(i.ToString())))
                        {
                            navArr.ToList().Add(i.ToString());
                        }
                    }
                    else
                    {
                        navArr = userNavArr;
                    }

                }

                return navArr.Any() ? allNavs.Where(n => navArr.Contains(n.FID)) : new List<Navigation>();
            }
            return new List<Navigation>();
        }

        #endregion

        #region 为用户授权

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="navJsonData">菜单、按钮JSON数据</param>
        /// <returns></returns>
        public int UserAuthorize(string navJsonData)
        {
            JObject jobj = JObject.Parse(navJsonData);
            var buttons = ButtonDal.Instance.GetAll().ToList();
            var userId = jobj["userId"];
            var menus = jobj["menus"];
            var navs = menus.Select(menu => new
            {
                navid = menu["navid"],
                btns = buttons.Where(n =>
                        menu["buttons"].Select(m => (string)m).Contains<string>(n.ButtonTag)
                        ).Select(k => k)
            });
            const string sql = "insert into Sys_UserNavBtns(userid,navid,btnid) values ({0},{1},{2})";
            var sb = new StringBuilder();

            foreach (var nav in navs)
            {
                foreach (var btn in nav.btns)
                {
                    sb.AppendFormat(sql, userId, nav.navid, btn.FID);
                    sb.AppendLine();
                }
            }

            SqlEasy.ExecuteNonQuery("delete Sys_UserNavBtns where userid=:userid",
                                                        new OracleParameter("userid", (int)userId));

            return !string.IsNullOrEmpty(sb.ToString()) ? SqlEasy.ExecuteNonQuery(sb.ToString()) : 0;
        }

        #endregion

        #region 用户登录

        public bool UserLogin(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return false;

            User u = UserDal.Instance.GetUserBy(userName);
            if (u == null)
                return false;

            string md5pass = StringHelper.MD5string(password/* + u.PassSalt*/);
            if (u.Password.Equals(md5pass, StringComparison.OrdinalIgnoreCase))
            {
                SysVisitor.Instance.UserId = u.FID;
                SysVisitor.Instance.MOBILE = u.Mobile;
                SysVisitor.Instance.UserName = u.UserName;
                SysVisitor.Instance.IsAdmin = (u.IsAdmin == 1);
                // SysVisitor.Instance.EmployeeID = u.EmployeeID;
                SysVisitor.Instance.CurrentUser = u;
                SysVisitor.Instance.Departments = string.Join(",", GetDepIDs(u.FID, true));

                return !SysVisitor.Instance.IsGuest;
            }
            return false;
        }


        public bool UserLoginByClient(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return false;
            string md5pass = StringHelper.MD5string(password/* + u.PassSalt*/);
            User u = UserDal.Instance.GetWhere(new{USERNAME=userName,PASSWORD=md5pass}).SingleOrDefault();
            if (u == null)
                return false;


            return true;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="saveCookieDays">保存cookie天数</param>
        /// <returns></returns>
        public bool UserLogin(string username, string password, int saveCookieDays = 1)
        {
            if (UserLogin(username, password))
            {
                //写入cookie
                CookieHelper.WriteCookie(SysVisitor.CookieNameKey, SysVisitor.CookieUserNameKey, username, "", saveCookieDays);
                var desPass = StringHelper.EncryptDES(password, Key.DESkey);
                CookieHelper.WriteCookie(SysVisitor.CookieNameKey, SysVisitor.CookiePasswordKey, desPass, "", saveCookieDays);


                //写入登录日志
                LogModel log = new LogModel();
                log.BusinessName = "用户登录";
                log.OperationIp = PublicMethod.GetClientIP();
                log.OperationTime = DateTime.Now;
                log.PrimaryKey = "";
                log.UserId = SysVisitor.Instance.UserId;
                log.TableName = "";
                log.OperationType = (int)OperationType.Login;
                LogDal.Instance.Insert(log);

                return true;
            }
            return false;
        }

        public bool UserLogin()
        {
            string username = CookieHelper.GetCookie(SysVisitor.CookieNameKey, SysVisitor.CookieUserNameKey);
            string desPassword = CookieHelper.GetCookie(SysVisitor.CookieNameKey, SysVisitor.CookiePasswordKey);
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(desPassword))
                return false;

            var password = StringHelper.DecryptDES(desPassword, Key.DESkey);
            return UserLogin(username, password);
        }

        #endregion

        #region 用户导航菜单

        /// <summary>
        /// 根据用户名获取用户的导航菜单
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public string GetNavJson(string username)
        {
            User u = UserDal.Instance.GetUserBy(username);
            var navList = GetNavsBy(u);
            if (navList == null) throw new ArgumentNullException("username");
            var navigations = navList as Navigation[] ?? navList.ToArray();
            return navigations.Any() ? JSONhelper.ToJson(BuildNavJson(navigations, "0", u.FID)) : "";
        }

        private IEnumerable<object> BuildNavJson(IEnumerable<Navigation> navs, string parentId, string userid)
        {
            var navigations = navs as Navigation[] ?? navs.ToArray();
            var menus =
                navigations.Where(n => n.ParentID == parentId && n.IsVisible && HasMenu(userid, n.FID))
                           .OrderBy(n => n.Sortnum)
                           .Select(n => new
                           {
                               id = n.FID,
                               text = n.NavTitle,
                               n.iconCls,
                               attributes = new
                               {
                                   url = n.Linkurl,
                                   n.iconUrl,
                                   parentid = n.ParentID,
                                   sortnum = n.Sortnum,
                                   n.BigImageUrl
                               },
                               state =
                                            parentId == "0"
                                                ? "open"
                                                : (navs.Any(a => a.ParentID == n.FID) ? "closed" : "open"),
                               children = BuildNavJson(navigations, n.FID, userid)
                           });
            return menus;
        }

        #endregion

        #region 权限判断
        /// <summary>
        /// 判断指定的用户是否可以访问指定的功能菜单
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="navId">导航菜单</param>
        /// <returns></returns>
        public bool HasMenu(string userId, string navId)
        {
            var user = UserDal.Instance.Get(userId);
            if (user != null)
            {
                if (user.IsAdmin == 1)
                    return true;
                DataTable userNavBtns = this.GetNavBtns(userId);
                return userNavBtns.Select("navid='" + navId + "' and btnid='" + browser + "'").Any();

            }

            return false;
        }

        /// <summary>
        /// 判断用户是否可以访问指定页面中的功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="navId">导航菜单Id</param>
        /// <param name="btnId">按钮ID</param>
        /// <returns></returns>
        public bool HasButton(string userId, string navId, string btnId)
        {
            DataTable userNavBtns = UserDal.Instance.GetNavBtnsBy(userId);
            var canBrowser =
                userNavBtns.AsEnumerable().Select(
                    row => (string)row["navid"] == navId && (string)row["btnid"] == browser).Any();
            if (canBrowser)
                return
                    userNavBtns.AsEnumerable().Select(row => (string)row["navid"] == navId && (string)row["btnid"] == btnId)
                        .Any();
            return false;
        }

        /// <summary>
        /// 创建指定功能页面的按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="navId">菜单Id</param>
        /// <returns></returns>
        public string PageButtons(string userId, string navId)
        {
            List<Button> btns = GetPageButtons(userId, navId);
            const string splitTag = @"<div class='datagrid-btn-separator'></div>";
            if (btns.Any())
            {
                string[] btnHtmlArr = (from btn in btns
                                       where btn.FID != browser   //隐藏浏览按钮
                                       //orderby btn.Sortnum
                                       select btn.ButtonHtml).ToArray<string>();
                return string.Join(splitTag, btnHtmlArr);
            }
            return "";
        }
        #endregion

        #region 数据权限

        /// <summary>
        /// 设置数据访问权限
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="deps">部门IDs</param>
        /// <returns></returns>
        public int SetDepartments(int userid, string deps)
        {
            UserDal.Instance.ClearDepartmentsBy(userid);
            if (string.IsNullOrEmpty(deps))
                return 1; //取消数据访问权限
            return UserDal.Instance.SetDepartments(userid, deps);
        }


        public List<string> GetDepIDs(string userid, bool withRoles = false)
        {
            User u = GetUser(userid);
            if (u != null)
            {
                List<string> deps = UserDal.Instance.GetDepIDs(u.FID);

                if (withRoles)
                {
                    var roles = u.Roles;
                    string[] strArr;
                    if (roles.Any())
                    {
                        foreach (Role r in roles)
                        {
                            if (!string.IsNullOrEmpty(r.Departments))
                            {
                                strArr = r.Departments.Split(',');
                                foreach (string depid in strArr)
                                {
                                    string depID = PublicMethod.GetString(depid);
                                    if (deps.Contains(depID))
                                        continue;
                                    deps.Add(depID);
                                }
                            }
                        }
                    }
                }

                return deps;
            }
            return new List<string>();
        }



        #endregion

        public IEnumerable<User> GetAll()
        {
            return UserDal.Instance.GetAll();
        }

        public string GetEasyUIJson(int page = 1, int rows = 1, string keywords = null, string sort = "FID", string order = "asc")
        {
            return UserDal.Instance.GetJson(page, rows, keywords, sort, order);
        }
    }
}
