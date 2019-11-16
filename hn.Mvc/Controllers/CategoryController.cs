using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Omu.ValueInjecter;
using System.Data;
using hn.Mvc;
using hn.DataAccess.Bll;
using hn.DataAccess.Model;
using hn.Common;
using hn.Core;
using hn.Core.Bll;

namespace WebMvc.Controllers
{
    public class CategoryController : BaseController
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
                return CategoryBll.Instance.GetCategoryTreegridData();
            }
            catch(Exception ex)
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
        public string Add(FormCollection context)
        {

            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return CategoryBll.Instance.AddNewCategory(rpm.Entity);
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
                CategoryModel d = new CategoryModel();
                d.InjectFrom(rpm.Entity);
                d.FID = rpm.FID;

                return CategoryBll.Instance.EditCategory(d);
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

            return CategoryBll.Instance.DeleteCategory(rpm.FID); 
        }

        private RequestParamModel<CategoryModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<CategoryModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<CategoryModel>>(json);
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
            var rpm = new RequestParamModel<CategoryModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<CategoryModel>>(json);
                rpm.CurrentContext = context;
            }

            if (rpm != null)
            {
                return CategoryBll.Instance.GetCategoryTreegridData();
            }

            return "";
        }


    }
}
