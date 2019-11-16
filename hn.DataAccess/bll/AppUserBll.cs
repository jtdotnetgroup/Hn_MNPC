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
    public class AppUserBll
    {
        public static AppUserBll Instance
        {
            get { return SingletonProvider<AppUserBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return AppUserDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="AppUsername">用户名</param>
        /// <param name="AppUserId">用户Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasAppUserNo(string phone, string id = "0")
        {
            var AppUsers = from n in AppUserDal.Instance.GetAll()
                            where n.PHONE == phone && n.FID != id
                           select n;
            return AppUsers.Any();
        }

        public string AddAppUser(AppUserModel u)
        {
            string uid = "0";
            string msg = "用户添加失败！";
            if (HasAppUserNo(u.PHONE, u.FID))
            {
                uid = "-2";
                msg = "手机号码已存在。";
            }
            else
            {
                uid = AppUserDal.Instance.Insert(u);
                if (uid != "")
                {
                    msg = "添加APP用户成功！";
                    LogBll<AppUserModel> log = new LogBll<AppUserModel>();
                    u.FID = uid;
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        }

        public string EditAppUser(AppUserModel u)
        {
           
            int k;
            string msg = "用户编辑失败。";
            if (HasAppUserNo(u.PHONE, u.FID))
            {
                k = -2;
                msg = "手机号码已存在。";
            }
            else
            {
                var oldAppUser = AppUserDal.Instance.Get(u.FID);
                k = AppUserDal.Instance.Update(u);
                if (k > 0)
                {
                    msg = "编辑APP用户成功。";
                    LogBll<AppUserModel> log = new LogBll<AppUserModel>();
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }
    }

}