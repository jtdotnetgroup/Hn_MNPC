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
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using hn.Core.Dal;
using hn.DataAccess.Bll;
using hn.DataAccess.model;
using hn.DataAccess.bll;
using hn.DataAccess.dal;
using hn.Mvc.Models;

namespace hn.Mvc.Controllers
{
    public class ShippingPolicyController : BaseController
    {
        //
        // GET: /TB_EXPRESSPOLICY/
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

            return TB_EXPRESSPOLICYBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }

        public ActionResult Add()
        {
            return View(new TB_EXPRESSPOLICYModel());
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            try
            {

                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);

                int count = TB_EXPRESSPOLICYDal.Instance.CountWhere(new { FNAME = rpm.Entity.FNAME });
                if (count > 0)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "政策已经存在" });
                }

                string result = TB_EXPRESSPOLICYDal.Instance.Insert(rpm.Entity);

                return JSONhelper.ToJson(new { errCode = 0, errMsg = result });
            }
            catch (Exception ex)
            {

                LogHelper.WriteLog(ex);
                return ex.Message;
            }
        }

        public ActionResult Edit()
        {
            UserBll.Instance.CheckUserOnlingState();

            TB_EXPRESSPOLICYModel model = TB_EXPRESSPOLICYDal.Instance.Get(Request.QueryString["id"]);
            if (model == null)
            {
                model = new TB_EXPRESSPOLICYModel();
            }
            return View(model);
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);
                TB_EXPRESSPOLICYModel model = TB_EXPRESSPOLICYDal.Instance.Get(rpm.FID);
                TB_EXPRESSPOLICYModel d = new TB_EXPRESSPOLICYModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;

                TB_EXPRESSPOLICYDal.Instance.Update(d);

                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {

                LogHelper.WriteLog(ex);
                return ex.Message;
            }
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return TB_EXPRESSPOLICYDal.Instance.Delete(rpm.FID).ToString();
        }

        private RequestParamModel<TB_EXPRESSPOLICYModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_EXPRESSPOLICYModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_EXPRESSPOLICYModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }



    }
}
