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
using hn.DataAccess.Model;
using hn.DataAccess.Bll;


namespace hn.Mvc.Controllers
{
    public class EmployeeController : BaseController
    {
        //
        // GET: /Employee/

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

            return EmployeeBll.Instance.GetJsonData(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order); 
        }

        [HttpPost]
        public string DptTree(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return EmployeeBll.Instance.GetDepartmentTreeData();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {

            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return EmployeeBll.Instance.AddEmployee(rpm.Entity);
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
            EmployeeModel u = new EmployeeModel();
            u.InjectFrom(rpm.Entity);
            u.FID = rpm.FID;

            return EmployeeBll.Instance.EditEmployee(u);
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return EmployeeBll.Instance.Delete(rpm.FID);
        }

        private RequestParamModel<EmployeeModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<EmployeeModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<EmployeeModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

    }
}
