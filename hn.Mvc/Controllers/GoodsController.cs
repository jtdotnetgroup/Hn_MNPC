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
    public class GoodsController : BaseController
    {
        //
        // GET: /Goods/

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

            return GoodsBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }

        public string Category()
        {
            string categoryTreeData = CategoryBll.Instance.GetCategoryTreegridData();
            return categoryTreeData.Replace("FID", "id").Replace("CATEGORY_NAME", "text");
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
            GoodsModel model = new GoodsModel();
            model.InjectFrom(rpm.Entity);
            model.GOODS_NUMBER = GoodsBll.Instance.GetNumber(rpm.Entity.CATEGORY_NUMBER);

            return GoodsBll.Instance.AddGoods(model);
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
                GoodsModel model = GoodsDal.Instance.Get(rpm.FID);
                GoodsModel d = new GoodsModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;
                d.GOODS_NUMBER = model.GOODS_NUMBER;
                d.CREATE_USER_ID = model.CREATE_USER_ID;
                d.STATUS = model.STATUS;
                d.AUDIT_TIME = model.AUDIT_TIME;
                d.AUDIT_USER = model.AUDIT_USER;
                d.AUDIT_USER_ID = model.AUDIT_USER_ID;
                d.CREATE_TIME = model.CREATE_TIME;

                return GoodsBll.Instance.EditGoods(d);
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
            return GoodsBll.Instance.DeleteAllBat(Request["id"]).ToString();
        }

        private RequestParamModel<GoodsModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<GoodsModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<GoodsModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        public string Audit()
        {
            try
            {



                return JSONhelper.ToJson("审核处理完成！并成功生成APP用户，初始密码：123456");
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(ex.Message);
            }
   
        }

    }
}
