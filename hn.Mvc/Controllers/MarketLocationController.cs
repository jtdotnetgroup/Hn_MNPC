using hn.Common;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.bll;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    /// <summary>
    /// 经营场所
    /// </summary>
    public class MarketLocationController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ToolBar = UserBll.Instance.PageButtons(SysVisitor.Instance.UserId, PublicMethod.GetString(Request["navid"]));
            return View();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            string keywords = context["keywords"];

            return V_PREMISEBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, keywords, rpm.Sort, rpm.Order);
        }


        public ActionResult Add()
        {
            return View(new TB_PREMISEModel());
        }

        public ActionResult Dialog()
        {
            return View();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {

            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return TB_PREMISEBll.Instance.AddPREMISE(rpm.Entity);
        }


        public ActionResult Edit()
        {
            UserBll.Instance.CheckUserOnlingState();

            TB_PREMISEModel model = TB_PREMISEDal.Instance.Get(Request.QueryString["id"]);
            if (model == null)
            {
                model = new TB_PREMISEModel();
            }
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public string Edit(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);
                TB_PREMISEModel model = TB_PREMISEDal.Instance.Get(rpm.FID);
                TB_PREMISEModel d = new TB_PREMISEModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;

                return TB_PREMISEBll.Instance.EditPREMISE(d);
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

            return TB_PREMISEDal.Instance.Delete(rpm.FID).ToString();
        }

        private RequestParamModel<TB_PREMISEModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_PREMISEModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_PREMISEModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
