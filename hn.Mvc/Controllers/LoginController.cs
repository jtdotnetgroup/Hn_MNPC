using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Dal;
using hn.Core.Model;
using hn.Core.Bll;
using hn.Common;
using hn.Core;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
using System.Configuration;

namespace hn.Mvc.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string isVcodeSessionKey = Key.SessionKeys.IsValidateCode.ToString();
            string vcodeSessionKey = Key.SessionKeys.ValidateCode.ToString();
            ViewBag.Forcescript = "";
            ViewBag.IsVcodeSessionKey = isVcodeSessionKey;
            ViewBag.ErrMsg = "";
            string account = collection["Account"];
            string password = collection["Password"];
            string force = collection["Force"];
            string vcode = collection["VCode"];

            string domainname = ConfigurationManager.AppSettings["DomainName"];

           // LogHelper.WriteLog("domainname=" + domainname);

            //if (System.Web.HttpContext.Current.Session[isVcodeSessionKey] != null
            //    && "1" == System.Web.HttpContext.Current.Session[isVcodeSessionKey].ToString()
            //    && (System.Web.HttpContext.Current.Session[vcodeSessionKey] == null
            //    || string.Compare(System.Web.HttpContext.Current.Session[vcodeSessionKey].ToString(), vcode.Trim(), true) != 0))
            //{
            //    ViewBag.ErrMsg = "alert('验证码错误!');";
            //}
            //else
            {
                User u = UserDal.Instance.GetUserBy(account);
                if (u != null)
                {
                    if (u.IsDisabled != 1)
                    {
                        bool flag = false;
                        if (u.IS_DOMAIN == 1)
                        {
                            //LogHelper.WriteLog("启用域验证登陆，用户名：" + account + "，密码：" + password);
                            //采用域验证登陆
                            flag = ADLogin.Login(domainname, account, password);
                            //LogHelper.WriteLog("域验证返回值：" + flag);

                            SysVisitor.Instance.UserId = u.FID;
                            SysVisitor.Instance.MOBILE = u.Mobile;
                            SysVisitor.Instance.UserName = u.UserName;
                            SysVisitor.Instance.IsAdmin = (u.IsAdmin == 1);
                            SysVisitor.Instance.CurrentUser = u;
                            //SysVisitor.Instance.Departments = string.Join(",", GetDepIDs(u.FID, true));
                        }
                        else
                        {
                            flag = UserBll.Instance.UserLogin(account, password);
                        }

                        if (flag)
                        {
                            ViewBag.Forcescript = "top.location='" + Url.Content("~/Home") + "';";
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session[isVcodeSessionKey] = "1";
                            ViewBag.ErrMsg = "alert('亲，用户名或密码不正确哦。');";
                        }
                    }
                    else
                    {
                        ViewBag.ErrMsg = "alert('亲，您的帐号已被禁用，请联系管理员吧。');";
                    }
                }
                else
                {
                    ViewBag.ErrMsg = "alert('亲,用户名不存在哦！仔细猜一哈。');";
                }
            }
            return View();
        }

        /// <summary>
        /// 验证码图像
        /// </summary>
        public void VCode()
        {
            string code;
            System.IO.MemoryStream ms = ValidateImgHelper.GetValidateImg(out code, Url.Content("~/Images/vcodebg.png"));
            System.Web.HttpContext.Current.Session[Key.SessionKeys.ValidateCode.ToString()] = code;
            Response.ClearContent();
            Response.ContentType = "image/gif";
            Response.BinaryWrite(ms.ToArray());
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        public ActionResult Quit()
        {
            // Response.ContentType = "text/plain";

            SysVisitor.Instance.LoginOut();

            //Response.Redirect(Url.Content("~/Login"));
            return RedirectToAction("Index", "Login");//
        }

        public ActionResult App()
        {
            SettingModel setting = new SettingModel();
            List<SettingModel> lstSetting = SettingDal.Instance.GetAll().ToList();
            if (lstSetting.Count > 0)
            {
                ViewBag.Url = lstSetting[0].VER_URL;
            }
            return View();
        }

        public ActionResult Apps()
        {
            SettingModel setting = new SettingModel();
            List<SettingModel> lstSetting = SettingDal.Instance.GetAll().ToList();
            if (lstSetting.Count > 0)
            {
                ViewBag.Url = lstSetting[0].VER_URL;
            }
            return View();
        }
    }
}
