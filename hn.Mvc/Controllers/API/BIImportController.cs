using hn.Common;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.Bll;
using hn.DataAccess.Dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using hn.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class BIImportController : Controller
    {

        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return BIINTERFACEBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }


        private RequestParamModel<BIINTERFACEMODEL> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<BIINTERFACEMODEL>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<BIINTERFACEMODEL>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
