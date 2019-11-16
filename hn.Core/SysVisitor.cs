using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using hn.Core.Dal;
using hn.Core.Model;
using hn.Common.Provider;
using hn.Common;
using hn.Core.Bll;
using hn.Common.Data;
namespace hn.Core
{
    public class SysVisitor
    {

        public static SysVisitor Instance
        {
            get { return SingletonProvider<SysVisitor>.Instance; }
        }

        public enum enmDataAuthorized
        {
            SCHEDULE,
            SIGN,
            SETTLEMENT,
            CONTACT,
            DRIVER,
            MESSAGE
        }


        #region Session Key

        public const string SessionUserIdKey = "HXLING-BPM-ADMIN-USERID";
        public const string SessionUserNameKey = "HXLING-BPM-ADMIN-USERNAME";
        public const string SessionIsAdminKey = "HXLING-BPM-ADMIN-ISADMIN";
        public const string SessionEmployeeIdKey = "HXLING-BPM-ADMIN-EMPLOYEEID";
        public const string SessionDataStartDatedKey = "HXLING-BPM-ADMIN-DATASTARTDATE";
        public const string SessionUserMOBILE = "HXLING-BPM-ADMIN-MOBILE";
        /// <summary>
        /// 用户可访问的部门列表
        /// </summary>
        public const string SessionDepartmentsKey = "HXLING-BPM-USER-DEPARTMENTS";
        #endregion

        #region CookieName Key

        public const string CookieNameKey = "HXLING-BPM-COOKIE-NAME";
        public const string CookieUserNameKey = "HXLING-BPM-COOKIE-USERNAME";
        public const string CookiePasswordKey = "HXLING-BPM-COOKIE-PASSWORD";
        public const string CookieDepartmentsKey = "HXLING-BPM-COOKIE-USER-DEPARTMENTS";
        #endregion

        /// <summary>
        /// 得到当前主题
        /// </summary>
        public static string Theme
        {
            get
            {
                if (SysVisitor.Instance.CurrentUser == null) return "default";

                var configJson = SysVisitor.Instance.CurrentUser.ConfigJson;
                if (string.IsNullOrEmpty(configJson))
                    return "default";
                if (JSONhelper.ConvertToObject<ConfigModel>(configJson).Theme == null)
                {
                    return "default";
                }
                return JSONhelper.ConvertToObject<ConfigModel>(configJson).Theme.Name;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get
            {
                return PublicMethod.GetString(HttpContext.Current.Session[SessionUserIdKey]);
            }
            set
            {
                HttpContext.Current.Session[SessionUserIdKey] = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string MOBILE
        {
            get
            {
                return PublicMethod.GetString(HttpContext.Current.Session[SessionUserMOBILE]);
            }
            set
            {
                HttpContext.Current.Session[SessionUserMOBILE] = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return PublicMethod.GetString(HttpContext.Current.Session[SessionUserNameKey]); }
            set { HttpContext.Current.Session[SessionUserNameKey] = value; }
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsAdmin
        {
            get { return PublicMethod.GetBool(HttpContext.Current.Session[SessionIsAdminKey]); }
            set { HttpContext.Current.Session[SessionIsAdminKey] = value; }
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        public User CurrentUser
        {
            get { return (User)HttpContext.Current.Session["HXLING-BPM-ADMIN"]; }
            set { HttpContext.Current.Session["HXLING-BPM-ADMIN"] = value; }
        }

        /// <summary>
        /// 当前用户可以访问的部门数据
        /// </summary>
        public string Departments
        {
            get { return HttpContext.Current.Session[SessionDepartmentsKey] as string; }
            set { HttpContext.Current.Session[SessionDepartmentsKey] = value; }
        }

        /// <summary>
        /// 职员ID
        /// </summary>
        public string EmployeeID
        {
            get { return PublicMethod.GetString(HttpContext.Current.Session[SessionEmployeeIdKey]); }
            set { HttpContext.Current.Session[SessionEmployeeIdKey] = value; }
        }

        /// <summary>
        /// 皮肤名称
        /// </summary>
        public string ThemeName
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentUser.ConfigJson))
                    return "default";
                return JSONhelper.ConvertToObject<ConfigModel>(CurrentUser.ConfigJson).Theme.Name;
            }
        }

        public int GridRows
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentUser.ConfigJson))
                    return 20;
                return JSONhelper.ConvertToObject<ConfigModel>(CurrentUser.ConfigJson).GridRows;
            }
        }

        public bool IsGuest
        {
            get
            {
                if (string.IsNullOrEmpty(UserName))
                    return !UserBll.Instance.UserLogin();
                return false;
            }
        }

        public string TmsUser
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["tmsUser"];
            }
        }

        public string Host
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["hostUrl"];
            }
        }

        public string PushAppKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["pushAppKey"];
            }
        }

        public string PushMasterSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["pushMasterSecret"];
            }
        }

        public string MsfAppKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["msfAppKey"];
            }
        }

        public string MsfSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["msfSecret"];
            }
        }

        public string OmsHost
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["omsHost"];
            }
        }

        public string DataStartDate
        {
            get { return PublicMethod.GetString(HttpContext.Current.Session[SessionDataStartDatedKey]); }
            set { HttpContext.Current.Session[SessionDataStartDatedKey] = value; }
        }

        public void LoginOut()
        {

            //写入退出日志
            LogModel log = new LogModel();
            log.BusinessName = "用户退出";
            log.OperationIp = PublicMethod.GetClientIP();
            log.OperationTime = DateTime.Now;
            log.PrimaryKey = "";
            log.UserId = UserId;
            log.TableName = "";
            log.OperationType = (int)OperationType.LoginOut;
            LogDal.Instance.Insert(log);


            CookieHelper.ClearUserCookie("", SysVisitor.CookieNameKey);
            UserName = null;
            UserId = "0";

            //HttpContext.Current.Response.Redirect("/");
        }

        /// <summary>
        /// 取到新的采番番号
        /// </summary>
        /// <param name="seqcode"></param>
        /// <returns></returns>
        public static string GetNewSeq(string fieldname)
        {
            SeqModel model = null;
            decimal newSeq = -1;

            IEnumerable<SeqModel> list = DbUtils.GetWhere<SeqModel>(new { FFIELDNAME = fieldname });
            foreach (object item in list)
            {
                model = (SeqModel)item;

                newSeq = model.FLastNo + model.FIncr;

                model.FLastNo = newSeq;

                break;
            }

            if (model != null)
            {
                SeqDal.Instance.Update(model);
            }
            else
            {
                throw new Exception("采番不正确！");
            }

            return newSeq.ToString("00000");
        }
    }
}
