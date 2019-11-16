using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using hn.Common;
using hn.Core;
using Omu.ValueInjecter;

namespace hn.Mvc.Controllers
{
    public class RoleController : BaseController
    {
        //
        // GET: /Role/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        public string ButtonColumns()
        {
            return "var btns = " + RoleBll.Instance.BuildNavBtnsColumns();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return RoleDal.Instance.JsonDataForEasyUIdataGrid(rpm.Pageindex, rpm.Pagesize, "", rpm.Sort, rpm.Order);
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

            return RoleBll.Instance.Add(rpm.Entity);
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);
            
            var r = new Role();
            r.InjectFrom(rpm.Entity);
            r.FID = rpm.FID;
            return RoleBll.Instance.Update(r);
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return RoleBll.Instance.Delete(rpm.FID);
        }

        [HttpPost]
        public string Authorize(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var data = rpm.Request("data");
            if (string.IsNullOrEmpty(data))
            {
                return "参数错误！";
            }

            return RoleBll.Instance.RoleAuthorize(data).ToString();
        }

        [HttpPost]
        public string Menus(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return RoleBll.Instance.GetRoleNavBtns(rpm.FID);
        }

        [HttpPost]
        public int SetDepartment(FormCollection context)
        {
            var roleid = PublicMethod.GetString(Request["FID"]);
            var deps = Request["deps"];
            return RoleBll.Instance.SetDepartments(roleid, deps);
        }

        private RequestParamModel<Role> GetRpm(FormCollection context)
        {
            var json = Request["json"];
            var rpm = new RequestParamModel<Role>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Role>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
