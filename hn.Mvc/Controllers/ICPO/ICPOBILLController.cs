using hn.Common;
using hn.Core;
using hn.DataAccess.Bll;
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
    public class ICPOBILLController : Controller
    {
        //
        // GET: /Apply/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 主表数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="FOrgID">销区</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [HttpPost]
        public string Data(int page = 1, int rows = 15, string startDate = null, string endDate = null, string brandid = null, int status = 0)
        {
            return V_ICPOBILLBLL.Instance.GetEasyUIJson(page, rows, startDate, endDate, brandid, status);
        }

        /// <summary>
        /// 明细表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="ICPOBILLID">主表ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [HttpPost]
        public string ENTRYData(int page = 1, int rows = 15, string ICPOBILLID = null, int status = 0)
        {
            return V_ICPOBILLENTRYBLL.Instance.GetEasyUIJson(page, rows, ICPOBILLID, status);
        }

        /// <summary>
        /// 价格政策表
        /// </summary>
        /// <param name="ICPOBILLID"></param>
        /// <returns></returns>
        [HttpPost]
        public string ICPOPOLICYData(string ICPOBILLID = null)
        {
            return JSONhelper.ToJson(V_ICPOPOLICYBLL.Instance.GetByICPOBILLID(ICPOBILLID));
        }

        /// <summary>
        /// 保存采购计划
        /// </summary>
        /// <param name="ICPOBILLJson">主表JSON</param>
        /// <param name="ICPOBILLENTRYListJson">明细列表JSON</param>
        /// <param name="ICPOPOLICYListJson">价格政策JSON</param>
        /// <returns></returns>
        /// [HttpPost]
        public JsonResult Save(string ICPOBILLJson, string ICPOBILLENTRYListJson)
        {
            string result = ICPOBILLBLL.Instance.Save(JSONhelper.ConvertToObject<ICPOBILLMODEL>(ICPOBILLJson),
                JSONhelper.ConvertToObject<IEnumerable<ICPOBILLENTRYMODEL>>(ICPOBILLENTRYListJson));

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("保存完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }

        public JsonResult Delete(string id)
        {
            try
            {
                ICPOBILLDAL.Instance.Delete(id);
                var details = ICPOBILLENTRYDAL.Instance.GetWhere(new { FICPOBILLID = id }).ToList();
                foreach (var model in details)
                {
                    ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new { FICPOBILLNO = System.DBNull.Value }, new { FID = model.FPLANID });
                }
                ICPOBILLENTRYDAL.Instance.DeleteWhere(new { FICPOBILLID = id });

                return JsonResultHelper.ToSuccess("删除完成！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JsonResultHelper.ToFailed(ex.Message);
            }

        }


        /// <summary>
        /// 获取采购计划明细视图数据
        /// </summary>
        /// <param name="ICPOBILLID"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<V_ICPOBILLENTRYMODEL> GetICPOBILLENTRYList(string ICPOBILLID)
        {
            return V_ICPOBILLENTRYBLL.Instance.GetByICPOBILLENTRY(ICPOBILLID);
        }

        /// <summary>
        /// 明细审核
        /// </summary>
        /// <param name="FID">主表ID</param>
        /// <param name="FENTRYID">序号？</param>
        /// <param name="FITEMID">商品ID</param>
        /// <param name="FAUDQTY">审核数量</param>
        /// <param name="FAUDAMOUNT">审核金额</param>
        /// <param name="isPass">是否通过</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ENTRYAduit(string FID = null, string FENTRYID = null, string FITEMID = null, decimal FAUDQTY = 0M, decimal FAUDAMOUNT = 0M, bool isPass = false)
        {
            string result = ICPOBILLENTRYBLL.Instance.Audit(FID, FENTRYID, FITEMID, FAUDQTY, FAUDAMOUNT, isPass);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("审核完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }

        [HttpPost]
        public string Accept(FormCollection context)
        {
            try
            {
                string id = context["id"];

                if (!string.IsNullOrEmpty(id))
                {
                    ICPOBILLDAL.Instance.UpdateWhatWhere(new
                    {
                        FSTATUS = 2
                    },
                 new { FID = id });


                    ICPOBILLENTRYDAL.Instance.UpdateWhatWhere(new
                    {
                        FSTATUS = 2
                    },
                        new { FICPOBILLID = id });
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
        public string Audit(FormCollection context)
        {
            try
            {
                string ids = context["ids"];
                string status = context["status"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    ICPOBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = status.ToInt(),
                            FCHECKDATE = DateTime.Now
                        },
                        new { FID = id });


                    ICPOBILLENTRYDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = status.ToInt()
                        },
                        new { FPLANID = id });

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

                    ICPOBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = 1,
                        },
                        new { FID = id });


                    ICPOBILLENTRYDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = 1
                        },
                        new { FPLANID = id });

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
