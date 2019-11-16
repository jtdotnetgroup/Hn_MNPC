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
using hn.DataAccess.dal;

namespace hn.Mvc.Controllers
{
    public class BrandController : BaseController
    {
        //
        // GET: /TB_Brand/
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

            return TB_BrandBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize,context["keywords"], rpm.Sort, rpm.Order); 
        }

        public ActionResult Add()
        {
            return View(new TB_BrandModel());
        }

        [HttpPost]
        public string Add(FormCollection context)
        {

            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return TB_BrandBll.Instance.AddBrand(rpm.Entity);
        }

        public ActionResult Edit()
        {
            UserBll.Instance.CheckUserOnlingState();

            TB_BrandModel model = TB_BrandDal.Instance.Get(Request.QueryString["id"]);
            if (model == null){
                model = new TB_BrandModel();
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
                TB_BrandModel model = TB_BrandDal.Instance.Get(rpm.FID);
                TB_BrandModel d =new TB_BrandModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;               

                return TB_BrandBll.Instance.EditBrand(d);
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

            return TB_BrandDal.Instance.Delete(rpm.FID).ToString();
        }

        private RequestParamModel<TB_BrandModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_BrandModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_BrandModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        [HttpPost]
        public string Account(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            string brandid = context["FBRANDID"];

            var list = TB_CLIENTACCOUNTDal.Instance.GetWhere(new { FBRANDID = brandid }).ToList();

            return JSONhelper.FormatJSONForEasyuiDataGrid(list.Count, list);
        }

    }
}
