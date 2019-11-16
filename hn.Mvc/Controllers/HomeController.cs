using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Dal;
using hn.Core;
using hn.Core.Model;
using hn.Common;
using hn.Core.Bll;
using hn.DataAccess.Dal;
using hn.DataAccess.Bll;

namespace hn.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            string configData = SysVisitor.Instance.CurrentUser.ConfigJson;

            string themePath = Server.MapPath("~/Content/theme/navtype/");
            NVelocityHelper vel = new NVelocityHelper(themePath);
            vel.Put("username", SysVisitor.Instance.CurrentUser.TrueName);
            string navHTML = "Accordion.html";
            if (!string.IsNullOrEmpty(configData))
            {
                ConfigModel sysconfig = JSONhelper.ConvertToObject<ConfigModel>(configData);
                if (sysconfig != null)
                {

                    switch (sysconfig.ShowType)
                    {
                        case "menubutton":
                            navHTML = "menubutton.html";
                            break;
                        case "tree":
                            navHTML = "tree.html";
                            break;
                        case "menuAccordion":
                        case "menuAccordion2":
                        case "menuAccordionTree":
                            navHTML = "topandleft.html";
                            break;
                        default:
                            navHTML = "Accordion.html";
                            break;
                    }

                }
            }

            ViewBag.NavContent = vel.FileToString(navHTML);

            //============APP用户总数================
            ViewBag.AppCount = 0;// DriverUserDal.Instance.GetAppCount();

            //============订单签收总数================
            ViewBag.SignCount = 0;// SignDal.Instance.GetCount("");


            //系统更新日志
            int pdateLogCount;

            ViewBag.UpdateLogList = SYS_UPDATELOGBLL.Instance.GetPageWithRecordCount(1, 3, out pdateLogCount);
            ViewBag.UpdateLogCount = pdateLogCount;

            return View();
        }

        [HttpPost]
        public string GetSystemUpdateLog()
        {
            return SYS_UPDATELOGBLL.Instance.GetEasyUIJson();
        }

        public string ConfigJs()
        {
            User u = UserDal.Instance.Get(SysVisitor.Instance.UserId);
            string cj = u.ConfigJson;
            if (string.IsNullOrEmpty(cj))
                return "var sys_config ={\"theme\":{\"title\":\"默认皮肤\",\"name\":\"default\",\"selected\":true},\"showType\":\"Accordion\",\"gridRows\":20}";
            else
            {
                return "var sys_config = " + cj;
            }
        }

        public string MenuData()
        {
            if (!SysVisitor.Instance.IsGuest)
            {
                var userName = SysVisitor.Instance.UserName;
                var menuJSON = "var menus = " + UserBll.Instance.GetNavJson(userName);
                return menuJSON;
            }
            else
            {
                return "var menus = -1;"; //没有登录
            }
        }

    }
}
