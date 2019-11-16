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
    public class DeliveryReleaseController : BaseController
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
            //TB_DeliveryReleaseModel model = TB_DeliveryReleaseDal.Instance.Get(id);

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

                //var r = new TB_DeliveryReleaseModel();
                //r.InjectFrom(rpm.Entity);
                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_DeliveryReleaseDal.Instance.Insert(rpm.Entity);

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

                //var r = new TB_DeliveryReleaseModel();
                //r.InjectFrom(rpm.Entity);
                //r.FID = rpm.FID;

                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_DeliveryReleaseDal.Instance.Update(r);

                return JSONhelper.ToJson(new { errCode = 0 });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        public ActionResult Detail()
        {
            return View();
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
                //int count = bll.GetCountByDeliveryRelease(rpm.FID);
                //if (count > 0)
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = "该品牌已经对应有电梯档案存在，无法删除！" });
                //}

                //TB_DeliveryReleaseBll.Instance.Delete(rpm.FID);

                return JSONhelper.ToJson(new { errCode = 0 });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        /// <summary>
        /// 数据模型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //private RequestParamModel<TB_DeliveryReleaseModel> GetRpm(FormCollection context)
        //{
        //    var json = context["json"];
        //    var rpm = new RequestParamModel<TB_DeliveryReleaseModel>(context) { CurrentContext = context, Action = Request["action"] };
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_DeliveryReleaseModel>>(json);
        //        rpm.CurrentContext = context;
        //    }

        //    return rpm;
        //}

        public ActionResult ICPRSelector()
        {
            return View();
        }

        public ActionResult SRCSelector()
        {
            return View();
        }

        public ActionResult PricePolicySelector()
        {
            return View();
        }
    }
}
