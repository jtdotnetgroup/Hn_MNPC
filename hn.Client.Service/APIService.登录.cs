using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using hn.Common;

namespace hn.Client.Service
{
    public partial class APIService
    {
        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="username">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        public User Login(string username, string password)
        {
            try
            {
                User u = UserDal.Instance.GetUserBy(username);
                if (u != null)
                {
                    if (u.IsDisabled != 1)
                    {
                        bool flag;
                        if (u.IS_DOMAIN == 1)
                        {
                            string domainname = ConfigurationManager.AppSettings["DomainName"];
                            //采用域验证登陆
                            flag = ADLogin.Login(domainname, username, password);
                        }
                        else
                        {
                            flag = UserBll.Instance.UserLoginByClient(username, password);
                        }

                        if (flag)
                        {
                            return u;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 修改密码接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        /// <returns></returns>
        public string ModifyPassword(string id, string oldpwd, string newpwd)
        {
            try
            {
                return UserBll.Instance.EditPassword(id, oldpwd, newpwd);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
    }
}