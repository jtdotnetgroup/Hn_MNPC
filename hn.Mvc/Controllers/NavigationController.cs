using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Bll;
using hn.Core;
using hn.Core.Model;
using hn.Common;
using Omu.ValueInjecter;
using System.Text;
using System.IO;

namespace hn.Mvc.Controllers
{
    public class NavigationController : BaseController
    {
        //
        // GET: /System/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            return NavigationBll.Instance.BuildNavTreeJSON();
        }

        // GET: /System/
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var json = context["json"];
            var rpm = new RequestParamModel<Navigation>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Navigation>>(json);
                rpm.CurrentContext = context;
            }

            return NavigationBll.Instance.AddNewNav(rpm.Entity);
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var json = context["json"];
            var rpm = new RequestParamModel<Navigation>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Navigation>>(json);
                rpm.CurrentContext = context;
            }

            if (rpm.FID == rpm.Entity.ParentID)
            {
                return "上级菜单不能与当前菜单相同。";
            }

            var nav = new Navigation();
            nav.InjectFrom(rpm.Entity);
            nav.FID = rpm.FID;
            return NavigationBll.Instance.EditNav(nav);
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var json = context["json"];
            var rpm = new RequestParamModel<Navigation>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Navigation>>(json);
                rpm.CurrentContext = context;
            }

            return NavigationBll.Instance.DeleteNav(rpm.KeyIds);
        }

        [HttpPost]
        public int SetButton(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var json = context["json"];
            var rpm = new RequestParamModel<Navigation>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Navigation>>(json);
                rpm.CurrentContext = context;
            }

            return NavigationBll.Instance.SetNavButtons(rpm.FID, rpm.KeyIds);
        }

        [HttpPost]
        public string Icon(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var json = context["json"];
            var rpm = new RequestParamModel<Navigation>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Navigation>>(json);
                rpm.CurrentContext = context;
            }

            string path = Server.MapPath("~/css/icon/32/");
            string[] files = Directory.GetFiles(path);

            FileInfo fileinfo;
            StringBuilder sb = new StringBuilder();
            foreach (string file in files)
            {
                fileinfo = new FileInfo(file);
                sb.AppendFormat("<li title=\"{0}\"><img src=\"{0}\"/></li>", "/css/icon/32/" + fileinfo.Name);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public ActionResult Buttons()
        {
            return View();
        }
        
        [HttpPost]
        public string Buttons(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            return NavigationBll.Instance.GetAllButtons();
        }

    }
}
