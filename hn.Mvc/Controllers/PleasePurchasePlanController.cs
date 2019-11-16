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
using hn.DataAccess.bll;
using System.Text;

namespace hn.Mvc.Controllers
{
    public class PleasePurchasePlanController : BaseController
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
        public string List(string filterJson, int page = 1, int rows = 15, string sort = "ID", string order = "DESC")
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                return ICPRBILLBLL.Instance.GetJson(page, rows, filterJson, sort, order);
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

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <returns></returns>
        public ActionResult AddItem()
        {
            return View();
        }
        public ActionResult AuditDetailed()
        {
            return View();
        }
        public ActionResult AddData()
        {
            return View();
        }
        public ActionResult PurchaseOrder()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult UnConfirm()
        {
            return View();
        }

        public ActionResult Close()
        {
            return View();
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Upload()
        {
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

                //var r = new TB_PleasePurchasePlanModel();
                //r.InjectFrom(rpm.Entity);
                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_PleasePurchasePlanDal.Instance.Insert(rpm.Entity);

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

                //var r = new TB_PleasePurchasePlanModel();
                //r.InjectFrom(rpm.Entity);
                //r.FID = rpm.FID;

                //string result;
                //if (!CheckData(r, out result))
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
                //}

                //TB_PleasePurchasePlanDal.Instance.Update(r);

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
                //int count = bll.GetCountByPleasePurchasePlan(rpm.FID);
                //if (count > 0)
                //{
                //    return JSONhelper.ToJson(new { errCode = -1, errMsg = "该品牌已经对应有电梯档案存在，无法删除！" });
                //}

                //TB_PleasePurchasePlanBll.Instance.Delete(rpm.FID);

                return JSONhelper.ToJson(new { errCode = 0 });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Selector()
        {
            return View();
        }

        public ActionResult BiImport()
        {
            return View();
        }


        public ActionResult Audit()
        {
            return View();
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string BIData(string filterJson, int page = 1, int rows = 15,string FBRANDNO = null,string FDEPTNO = null, string sort = "FID", string order = "DESC")
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" 1= 1");
                if (!string.IsNullOrEmpty(FBRANDNO))
                {
                    sql.AppendFormat(" and FBRANDNO = '{0}' ", PublicMethod.GetString(FBRANDNO));
                }

                if (!string.IsNullOrEmpty(FDEPTNO))
                {
                    sql.AppendFormat(" and FDEPTNO = '{0}' ", PublicMethod.GetString(FDEPTNO));
                }

                return V_BIINTERFACEDal.Instance.GetJson(page, rows, sql.ToString(), sort,order);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "";
            }
        }

        public ActionResult Import()
        {
            return View();
        }

        public ActionResult AuditLog()
        {
            return View();
        }

        /// <summary>
        /// 数据模型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //private RequestParamModel<TB_PleasePurchasePlanModel> GetRpm(FormCollection context)
        //{
        //    var json = context["json"];
        //    var rpm = new RequestParamModel<TB_PleasePurchasePlanModel>(context) { CurrentContext = context, Action = Request["action"] };
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_PleasePurchasePlanModel>>(json);
        //        rpm.CurrentContext = context;
        //    }

        //    return rpm;
        //}
    }
}
