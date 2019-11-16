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
    public class DeliverBaseController : BaseController
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

            //string keywords = context["keywords"];

            return TB_DELIVER_BASEDal.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }


        public ActionResult Add()
        {
            TB_DELIVER_BASEModel model = TB_DELIVER_BASEDal.Instance.Get(Request.QueryString["id"]);
            if (model == null)
            {
                model = new TB_DELIVER_BASEModel();
            }

            return View(model);
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

            return TB_DELIVER_BASEBll.Instance.Add(rpm.Entity);
        }


        public ActionResult Edit()
        {
            UserBll.Instance.CheckUserOnlingState();

            TB_DELIVER_BASEModel model = TB_DELIVER_BASEDal.Instance.Get(Request.QueryString["id"]);
            if (model == null)
            {
                model = new TB_DELIVER_BASEModel();
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
                TB_DELIVER_BASEModel model = TB_DELIVER_BASEDal.Instance.Get(rpm.FID);
                TB_DELIVER_BASEModel d = new TB_DELIVER_BASEModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;

                return TB_DELIVER_BASEBll.Instance.Edit(d);
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

            return TB_DELIVER_BASEDal.Instance.Delete(rpm.FID).ToString();
        }

        private RequestParamModel<TB_DELIVER_BASEModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_DELIVER_BASEModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_DELIVER_BASEModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
