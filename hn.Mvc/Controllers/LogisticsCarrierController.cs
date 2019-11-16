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
    public class LogisticsCarrierController : BaseController
    {
        //
        // GET: /TB_EXPRESSCOMPANY/
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

            return TB_EXPRESSCOMPANYBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }

        public ActionResult Add()
        {
            return View(new TB_EXPRESSCOMPANYModel());
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            try
            {

                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);

                int count = TB_EXPRESSCOMPANYDal.Instance.CountWhere(new { FCODE = rpm.Entity.FCODE });
                if (count > 0)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "物流编码已经存在" });
                }


                count = TB_EXPRESSCOMPANYDal.Instance.CountWhere(new { FNAME = rpm.Entity.FNAME });
                if (count > 0)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "物流名称已经存在" });
                }

                string result = TB_EXPRESSCOMPANYDal.Instance.Insert(rpm.Entity);

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

            TB_EXPRESSCOMPANYModel model = TB_EXPRESSCOMPANYDal.Instance.Get(Request.QueryString["id"]);
            if (model == null)
            {
                model = new TB_EXPRESSCOMPANYModel();
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
                TB_EXPRESSCOMPANYModel model = TB_EXPRESSCOMPANYDal.Instance.Get(rpm.FID);
                TB_EXPRESSCOMPANYModel d = new TB_EXPRESSCOMPANYModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;

                var ls = TB_EXPRESSCOMPANYDal.Instance.GetWhere(new { FCODE = rpm.Entity.FCODE });
                foreach(TB_EXPRESSCOMPANYModel m in ls) {
                    if (m.FID != model.FID)
                    {
                        return JSONhelper.ToJson(new { errCode = -1, errMsg = "物流编码已经存在" });
                    }
                }


                ls = TB_EXPRESSCOMPANYDal.Instance.GetWhere(new { FNAME = rpm.Entity.FNAME });
                foreach (TB_EXPRESSCOMPANYModel m in ls)
                {
                    if (m.FID != model.FID)
                    {
                        return JSONhelper.ToJson(new { errCode = -1, errMsg = "物流名称已经存在" });
                    }
                }

                TB_EXPRESSCOMPANYDal.Instance.Update(d);

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

            return TB_EXPRESSCOMPANYDal.Instance.Delete(rpm.FID).ToString();
        }

        private RequestParamModel<TB_EXPRESSCOMPANYModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_EXPRESSCOMPANYModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_EXPRESSCOMPANYModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }



    }
}
