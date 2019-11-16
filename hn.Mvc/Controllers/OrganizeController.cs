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
using hn.DataAccess.Bll;
using System.Data;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using hn.Mvc.Models;

namespace hn.Mvc.Controllers
{
    public class OrganizeController : BaseController
    {

        //
        // GET: /Customer/
        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            try
            {

                return TB_OrganizationBll.Instance.GetOrganizationTreegridData();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                return ex.Message;
            }

        }


        [HttpPost]
        public string Tree(FormCollection context)
        {
            try
            {

                return JSONhelper.ToJson(TB_OrganizationBll.Instance.GetOrganizationTreeNodes("0").ToList());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                return ex.Message;
            }

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(TB_OrganizationModel model)
        {
            UserBll.Instance.CheckUserOnlingState();

            string result = TB_OrganizationBll.Instance.Add(model);

            if(result.IsGuid())
            {
                return JsonResultHelper.ToSuccess("添加完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Edit(TB_OrganizationModel model)
        {
            UserBll.Instance.CheckUserOnlingState();

            string result = TB_OrganizationBll.Instance.Edit(model);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("修改完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }

        [HttpPost]
        public JsonResult Delete(string FID)
        {
            UserBll.Instance.CheckUserOnlingState();

            string result = TB_OrganizationBll.Instance.Delete(FID);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("删除完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }

        private RequestParamModel<TB_OrganizationModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_OrganizationModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_OrganizationModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }


        /// <summary>
        /// 选择数据列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Ref(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_OrganizationModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_OrganizationModel>>(json);
                rpm.CurrentContext = context;
            }

            if (rpm != null)
            {
                return TB_OrganizationBll.Instance.GetOrganizationTreegridData();
            }

            return "";
        }


    }
}
