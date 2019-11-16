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

namespace hn.Mvc.Controllers
{
    public class SalesDeliveryController : BaseController
    {
        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //工具栏
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }


        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return ICSTOCKBILLDAL.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }

        /// <summary>
        /// 添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Viewhtm()
        {
            return View();
        }
        public ActionResult PurchaseOrder()
        {
            return View();
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            int id = Request["id"].ToInt();
            //TB_SalesDeliveryModel model = TB_SalesDeliveryDal.Instance.Get(id);

            return View();
        }

        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Add(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                //var rpm = GetRpm(context);

                //var r = new TB_SalesDeliveryModel();
                //r.InjectFrom(rpm.Entity);
                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_SalesDeliveryDal.Instance.Insert(rpm.Entity);

                return JSONhelper.ToJson(new { errCode = 0 });

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Edit(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                //var rpm = GetRpm(context);

                //var r = new TB_SalesDeliveryModel();
                //r.InjectFrom(rpm.Entity);
                //r.FID = rpm.FID;

                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_SalesDeliveryDal.Instance.Update(r);

                return JSONhelper.ToJson(new { errCode = 0 });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        /// <summary>
        /// 删除处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                //var rpm = GetRpm(context);

                //TB_MachineBll bll = new TB_MachineBll();
                //int count = bll.GetCountBySalesDelivery(rpm.FID);
                //if (count > 0)
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = "该品牌已经对应有电梯档案存在，无法删除！" });
                //}

                //TB_SalesDeliveryBll.Instance.Delete(rpm.FID);

                return JSONhelper.ToJson(new { errCode = 0 });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        private RequestParamModel<ICSTOCKBILLMODEL> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<ICSTOCKBILLMODEL>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<ICSTOCKBILLMODEL>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
