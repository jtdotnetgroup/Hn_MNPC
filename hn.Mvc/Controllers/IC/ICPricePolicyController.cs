using hn.Common;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using hn.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class ICPricePolicyController : BaseController
    {
        //
        // GET: /Apply/

        public ActionResult Index()
        {
            //工具栏
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="salesTerritoryID">销区ID</param>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public string Data(int page = 1, int rows = 15, string Flag="0", string FBrandID = null, string keywords = null, string brandName="", string clientName = "", string checkState = "", string startDate = "", string endDate = "",string policytype = "")
        {
            return V_ICPRICEPOLICYBLL.Instance.GetEasyUIJson(page, rows, Flag, FBrandID, keywords, brandName, clientName, checkState, startDate: startDate, endDate: endDate, policytype:policytype);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="policyID">价格政策ID</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        [HttpPost]
        public string EntryData(int page = 1, int rows = 15, string policyID = null, string startDate = null, string endDate = null, string productInfo = null,string itemid = null)
        {
            return V_ICPRICEPOLICYENTRYBLL.Instance.GetEasyUIJson(page, rows, policyID, startDate, endDate, productInfo, itemid);
        }

        /// <summary>
        /// 保存价格政策，自动检测新增和修改
        /// </summary>
        /// <param name="ICPricePolicy">价格政策Model</param>
        /// <param name="ICPricePolicyEntryList">价格政策明细列表</param>
        /// <param name="DeleteEntryList">要删除的价格政策ID列表</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(string ICPricePolicyJson, string ICPricePolicyEntryListJson, string DeleteEntryList)
        {
            try
            {
                ICPRICEPOLICYMODEL model = JSONhelper.ConvertToObject<ICPRICEPOLICYMODEL>(ICPricePolicyJson);

                List<ICPRICEPOLICYENTRYMODEL> list = JSONhelper.ConvertToObject<List<ICPRICEPOLICYENTRYMODEL>>(ICPricePolicyEntryListJson);
                object result = ICPRICEPOLICYBLL.Instance.Save(model, list, DeleteEntryList);

                return JsonResultHelper.ToSuccess("", result);
            }
            catch (Exception ex)
            {
                return JsonResultHelper.ToFailed("保存失败！"+ex.Message);
            }
        }

        /// <summary>
        /// 编辑明细
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditEntry(string ICPricePolicyEntryJson)
        {
            //ICPricePolicyEntryBll.Instance.yup

            return null;
        }


        public JsonResult Delete(string ID)
        {
            string result = ICPRICEPOLICYBLL.Instance.Delete(ID);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("删除完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }


        [HttpPost]
        public string Audit(FormCollection context)
        {
            try
            {
                string ids = context["ids"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    ICPRICEPOLICYDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FCHECKSTATUS = 1,
                            FCHECKERID = SysVisitor.Instance.UserId,
                            FCHECKDATE = DateTime.Now
                        },
                        new { FID = id });

                }

                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(ex.Message);
            }

        }

        [HttpPost]
        public string AuditAnti(FormCollection context)
        {
            try
            {
                string ids = context["ids"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {

                    ICPRICEPOLICYDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FCHECKSTATUS = 0,
                            FCHECKERID = ""
                        },
                        new { FID = id });
                }

                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(ex.Message);
            }

        }
    }
}
