using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using hn.Common.Provider;
using hn.Core.Dal;
using hn.Common;

using Newtonsoft.Json.Linq;
using hn.Core.Model;
using hn.Common.Data.SqlServer;
using System.Data;
using System.Data.OracleClient;

namespace hn.Core.Bll
{
    public class RoleBll
    {
        public static RoleBll Instance
        {
            get { return SingletonProvider<RoleBll>.Instance; }
        }

        public bool HasRoleName(string roleName, string roleId = "0")
        {
            var allRoles = from n in RoleDal.Instance.GetAll()
                           where n.RoleName == roleName && n.FID != roleId
                           select n;
            return allRoles.Any();
        }

        public int SetDefaultRole(string roleid)
        {
            return RoleDal.Instance.SetDefaultRole(roleid);
        }


        public string Add(Role r)
        {
            string msg = "添加失败";
            if (HasRoleName(r.RoleName))
            {
                msg = "角色名称已存在。";
            }
            string k = RoleDal.Instance.Insert(r);
            if(k!="" )
            {
                msg = "添加成功";
                LogBll<Role> log = new LogBll<Role>();
                r.FID = k;
                log.AddLog(r);
            }
            if (r.IsDefault == 1)
                SetDefaultRole(r.FID);
            return new JsonMessage {Success = true, Data = k.ToString(), Message = msg}.ToString();
        }

        public string Update(Role r)
        {
            string msg = "编辑失败";
            if (HasRoleName(r.RoleName, r.FID))
                msg = "角色名称已存在。";
            Role old = RoleDal.Instance.Get(r.FID);
            int k = RoleDal.Instance.Update(r);
            if(k > 0)
            {
                msg = "编辑成功";
                LogBll<Role> log = new LogBll<Role>();
                log.UpdateLog(old, r);
            }

            if (r.IsDefault == 1)
                SetDefaultRole(r.FID);

            return new JsonMessage { Success = true, Data = k.ToString(), Message = msg }.ToString();
        }

        public string Delete(string roleid)
        {
            string msg = "删除失败。";
            var r = RoleDal.Instance.Get(roleid);
            //先删除角色中分配的权限
            SqlEasy.ExecuteNonQuery("delete Sys_RoleNavBtns where roleid=:roleid", new OracleParameter("roleid", roleid));
            int k = RoleDal.Instance.Delete(roleid);
            if(k > 0 )
            {
                msg = "删除成功。";
                LogBll<Role> log = new LogBll<Role>();
                log.DeleteLog(r);
            }
            return new JsonMessage { Success = true, Data = k.ToString(), Message = msg }.ToString();
        }

        /// <summary>
        /// 创建treegrid的所有按钮列
        /// </summary>
        /// <returns></returns>
        public string BuildNavBtnsColumns()
        {
            var list = ButtonDal.Instance.GetAll();
            var json = from n in list
                       orderby n.Sortnum ascending
                       select new {title = n.ButtonText, field = n.ButtonTag, width = 60,align="center",
                                   editor = new { type = "checkbox", options = new { @on = "√", off = "x" } }
                       };
            return JSONhelper.ToJson(json);
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="navJsonData">导航菜单、按钮数据</param>
        /// <returns></returns>
        public int RoleAuthorize(string navJsonData)
        {
            JObject jobj = JObject.Parse(navJsonData);
            var buttons = ButtonDal.Instance.GetAll().ToList();
            var roleid = jobj["roleId"];
            var menus = jobj["menus"];
            var navs = menus.Select(menu => new{
                                        navid = menu["navid"],
                                        btns = buttons.Where(n =>
                                                menu["buttons"].Select(m => (string) m).Contains<string>(n.ButtonTag)
                                                ).Select(k => k)
                        });
            const string sql = "insert into Sys_RoleNavBtns(roleid,navid,btnid) values ('{0}','{1}','{2}')";
            List<string> sb = new List<string>();

            foreach (var nav in navs )
            {
                foreach (var btn in nav.btns)
                {
                    sb.Add(string.Format(sql, roleid, nav.navid, btn.FID));
                }
            }

            SqlEasy.ExecuteNonQuery("delete sys_roleNavBtns where roleid=:roleid", 
                                                        new OracleParameter("roleid", (string)roleid));
            
            foreach(string s in sb)
            {
                SqlEasy.ExecuteNonQuery(s);
            }
            return 1;
          //  return !string.IsNullOrEmpty(sb.ToString()) ? SqlEasy.ExecuteNonQuery( sb.ToString()) : 0;
        }


        public JArray GetRoleNavBtns(IEnumerable<DataRow> btns, string parentId)
        {
            var navList = NavigationDal.Instance.GetList(parentId);

            var dataRows = btns as DataRow[] ?? btns.ToArray();
            var navigations = navList as Navigation[] ?? navList.ToArray();
            
            JArray jArr = new JArray();
            foreach (var n in navigations)
            {
                var jobj = new JObject(new JProperty("FID", n.FID),
                                       new JProperty("NavTitle", n.NavTitle),
                                       new JProperty("iconCls", n.iconCls),
                                       new JProperty("Buttons", new JArray(from b in n.Buttons
                                                                           select new JValue(b.ButtonTag))),
                                       new JProperty("children", GetRoleNavBtns(dataRows, n.FID)));

                var n1 = n;

                var navbtns = dataRows.Where(b => (string)b["navid"] == n1.FID).Select(c => c["ButtonTag"]).ToArray();

                foreach (var button in ButtonDal.Instance.GetAll())
                    jobj.Add(new JProperty(button.ButtonTag, navbtns.Contains(button.ButtonTag) ? "√" : "x"));

                jArr.Add(jobj);
            }
            return jArr;
        }


        public string GetRoleNavBtns(string roleid)
        {
            DataTable dt = RoleDal.Instance.GetNavBtnsBy(roleid);
            return GetRoleNavBtns(dt.AsEnumerable(), "0").ToString().Replace("icon ","");
        }

        /// <summary>
        /// 设置数据访问权限
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="deps">部门IDs</param>
        /// <returns></returns>
        public int SetDepartments(string roleid, string deps)
        {
            RoleDal.Instance.ClearDepartmentsBy(roleid);
            if (string.IsNullOrEmpty(deps))
                return 1; //取消数据访问权限
            return RoleDal.Instance.SetDepartments(roleid, deps);
        }
    }
}
