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
    public class PurchaseOrderController : BaseController
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


        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string List(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                return "";
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "";
            }
        }

        /// <summary>
        /// 添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddOrderDetails()
        {
            return View();
        }
        public ActionResult ReviewOrderDetails()
        {
            return View();
        }
        public ActionResult AddOrderRecord()
        {
            return View();
        }
        public ActionResult AddReferenceData()
        {
            return View();
        }
        public ActionResult AddPricePolicy()
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
            //TB_PurchaseOrderModel model = TB_PurchaseOrderDal.Instance.Get(id);

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

                //var r = new TB_PurchaseOrderModel();
                //r.InjectFrom(rpm.Entity);
                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_PurchaseOrderDal.Instance.Insert(rpm.Entity);

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

                //var r = new TB_PurchaseOrderModel();
                //r.InjectFrom(rpm.Entity);
                //r.FID = rpm.FID;

                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_PurchaseOrderDal.Instance.Update(r);

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
                //int count = bll.GetCountByPurchaseOrder(rpm.FID);
                //if (count > 0)
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = "该品牌已经对应有电梯档案存在，无法删除！" });
                //}

                //TB_PurchaseOrderBll.Instance.Delete(rpm.FID);

                return JSONhelper.ToJson(new { errCode = 0 });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        public ActionResult Confirm()
        {
            return View();
        }

        /// <summary>
        /// 数据模型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //private RequestParamModel<TB_PurchaseOrderModel> GetRpm(FormCollection context)
        //{
        //    var json = context["json"];
        //    var rpm = new RequestParamModel<TB_PurchaseOrderModel>(context) { CurrentContext = context, Action = Request["action"] };
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_PurchaseOrderModel>>(json);
        //        rpm.CurrentContext = context;
        //    }

        //    return rpm;
        //}
    }
}
