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
using hn.DataAccess.Dal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hn.Mvc.Controllers
{
    public class OrderController : BaseController
    {
        //
        // GET: /Order/

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

            return OrderBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order); 
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return OrderDal.Instance.BatchDelete(rpm.FID).ToString();
        }

        /// <summary>
        /// 报价明细列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Detail(FormCollection context)
        {
            var id = Request["orderid"];
            var json = context["json"];
            var rpm = new RequestParamModel<OrderDetailModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<OrderDetailModel>>(json);
                rpm.CurrentContext = context;
            }

            if (rpm != null && !string.IsNullOrEmpty(rpm.Filter))
            {
                return OrderDetailBll.Instance.GetJson(rpm.Pageindex, 10000, rpm.Filter, rpm.Sort, rpm.Order);
            }
            else
            {
                return OrderDetailBll.Instance.GetJson(0, 0, "", "FID");
            }
        }

        public string Detail()
        {
            var id = Request["orderid"];
            return JSONhelper.ToJson(OrderDetailDal.Instance.GetWhere(new { ORDER_ID = id }).ToList());
        }

      
        private RequestParamModel<OrderModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<OrderModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<OrderModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

    }
}
