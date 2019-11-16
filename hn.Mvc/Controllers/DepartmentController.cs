using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Common;
using hn.Core.Bll;
using hn.Core;
using hn.Core.Model;
using Omu.ValueInjecter;


namespace hn.Mvc.Controllers
{
    public class DepartmentController : BaseController
    {
        //
        // GET: /Department/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            return DepartmentBll.Instance.GetDepartmentTreegridData();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return DepartmentBll.Instance.AddNewDepartment(rpm.Entity);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {

            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);
            Department d = new Department();
            d.InjectFrom(rpm.Entity);
            d.FID = rpm.FID;
            return DepartmentBll.Instance.EditDepartment(d);
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return DepartmentBll.Instance.DeleteDepartment(rpm.FID);
        }

        [HttpPost]
        public string TreeJson(FormCollection context)
        {
            return DepartmentBll.Instance.GetDepartmentTreeJson();
        }

        private RequestParamModel<Department> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<Department>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Department>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

    }
}
