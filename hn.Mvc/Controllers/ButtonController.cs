using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Bll;
using hn.Core.Model;
using hn.Common;
using hn.Core;
using Omu.ValueInjecter;
using hn.DataAccess.Bll;
using hn.DataAccess.Model;
using hn.Core.Dal;

namespace hn.Mvc.Controllers
{
    public class ButtonController : BaseController
    {
        //
        // GET: /Button/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }


        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return ButtonDal.Instance.JsonDataForEasyUIdataGrid(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);
            var b = new Button();
            b.InjectFrom(rpm.Entity);


            return ButtonBll.Instance.AddButton(b);
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var p = new Button();
            p.InjectFrom(rpm.Entity);
            p.FID = rpm.FID;
            return ButtonBll.Instance.EditButton(p);
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return ButtonBll.Instance.DelButton(rpm.FID);
        }

       

        private RequestParamModel<Button> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<Button>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Button>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
