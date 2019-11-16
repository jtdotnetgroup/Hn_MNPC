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

namespace hn.Mvc.Controllers
{
    public class FranchiseeController : BaseController
    {
        //
        // GET: /Franchisee/

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

            return FranchiseeBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order); 
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {

            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);
           // rpm.Entity.ADD_DATE = DateTime.Now;
           // rpm.Entity.PASSWORD = StringHelper.MD5string(rpm.Entity.PASSWORD);

            return FranchiseeBll.Instance.AddFranchisee(rpm.Entity);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);
                FranchiseeModel model = FranchiseeDal.Instance.Get(rpm.FID);
                FranchiseeModel d =new FranchiseeModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;               
                d.AUDIT_TIME = model.AUDIT_TIME;
                d.AUDIT_USER = model.AUDIT_USER;
                d.AUDIT_USER_ID = model.AUDIT_USER_ID;
                d.CREATE_TIME = model.CREATE_TIME;

                return FranchiseeBll.Instance.EditFranchisee(d);
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

            return FranchiseeDal.Instance.Delete(rpm.FID).ToString();
        }

        public ActionResult Password()
        {
            return View();
        }

        public string EditPassword()
        {
            string fid = Request["fid"];
            string password = Request["password"];

            int result = FranchiseeDal.Instance.UpdateWhatWhere(new { PASSWORD = StringHelper.MD5string(password) }, new { FID = fid });
            if (result > 0)
            {
                return JSONhelper.ToJson("密码修改成功！");
            }
            else
            {
                return JSONhelper.ToJson("密码修改失败！");
            }
        }

        private RequestParamModel<FranchiseeModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<FranchiseeModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<FranchiseeModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

    }
}
