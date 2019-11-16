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
    public class FactoryAccountManagementController : BaseController
    {
        //
        // GET: /TB_CLIENTACCOUNT/
        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();
            string keywords = context["keywords"];

            var rpm = GetRpm(context);

            string where = "1=1";
            
            if (!string.IsNullOrEmpty(keywords))
            {
                where += string.Format(" AND (FACCOUNT like '%{0}%' OR FNAME LIKE '%{0}%')", keywords);
            }

            return V_CLIENTACCOUNTBll.Instance.GetJson(where, rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }
        [HttpPost]
        public string GetListByBrandID(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            string brandid = context["brandid"];
            if (string.IsNullOrEmpty(brandid))
            {
                return "";
            }
            string where = "FBRANDID='" + brandid + "'";
            string keywords = context["keywords"];
            if (!string.IsNullOrEmpty(keywords))
            {
                where +=string.Format(" AND (FACCOUNT like '%{0}%' OR FNAME LIKE '%{0}%')",keywords);
            }

            var rpm = GetRpm(context);

            return V_CLIENTACCOUNTBll.Instance.GetJson(where, rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }

        public ActionResult Add()
        {
            return View(new TB_CLIENTACCOUNTModel());
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            try
            {

                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);

                int count = TB_CLIENTACCOUNTDal.Instance.CountWhere(new { FACCOUNT = rpm.Entity.FACCOUNT });
                if (count > 0)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "账号已经存在" });
                }

                count = TB_CLIENTACCOUNTDal.Instance.CountWhere(new { FBRANDID = rpm.Entity.FBRANDID, FCOMMONFLAG=1 });
                if (count > 0)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "该品牌下基准价账户已经存在！" });
                }

                string result = TB_CLIENTACCOUNTDal.Instance.Insert(rpm.Entity);

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

            V_CLIENTACCOUNTModel model = V_CLIENTACCOUNTDal.Instance.Get(Request.QueryString["id"]);
            if (model == null)
            {
                model = new V_CLIENTACCOUNTModel();
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
                TB_CLIENTACCOUNTModel model = TB_CLIENTACCOUNTDal.Instance.Get(rpm.FID);
                TB_CLIENTACCOUNTModel d = new TB_CLIENTACCOUNTModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;

                TB_CLIENTACCOUNTDal.Instance.Update(d);

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

            return TB_CLIENTACCOUNTDal.Instance.Delete(rpm.FID).ToString();
        }

        private RequestParamModel<TB_CLIENTACCOUNTModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_CLIENTACCOUNTModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_CLIENTACCOUNTModel>>(json);
                rpm.CurrentContext = context;
                
            }

            return rpm;
        }



    }
}
