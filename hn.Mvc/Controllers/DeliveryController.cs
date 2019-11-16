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
using hn.DataAccess.dal;

namespace hn.Mvc.Controllers
{
    public class DeliveryController : BaseController
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
        public string Data(
            int page = 1,
            int rows = 15,
            string brandid = null,
            string groupno = null,
            string startDate = null,
            string endDate = null,
            string classarea2name = null,
            string status = null,
            string transid = null)
        {
            return V_DELIVERY_DETAILDal.Instance.GetJson(page, rows, brandid, groupno, startDate, endDate, classarea2name,status, transid);
        }

        /// <summary>
        /// 明细数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="groupno"></param>
        /// <returns></returns>
        [HttpPost]
        public string GroupData(string groupno = null)
        {
            return V_DELIVERY_DETAILDal.Instance.GetDetail(groupno);
        }

        [HttpPost]
        public string Save(FormCollection context)
        {
            try
            {
                string data = context["data"];
                if (!string.IsNullOrEmpty(data))
                {
                    List<TB_DELIVERY_DETAILModel> list = JSONhelper.ConvertToObject<List<TB_DELIVERY_DETAILModel>>(data);
                    foreach (TB_DELIVERY_DETAILModel model in list)
                    {
                        TB_DELIVERY_DETAILModel dbModel = TB_DELIVERY_DETAILDal.Instance.Get(model.FID);
                        if (dbModel != null)
                        {
                            dbModel.FEXPRESSCOMPANYID = model.FEXPRESSCOMPANYID;
                            dbModel.FCARNUMBER = model.FCARNUMBER;
                            dbModel.FAMOUNT = model.FAMOUNT;
                            dbModel.FWAYBILLNO = model.FWAYBILLNO;
                            dbModel.FSTATUS = 1;
                            TB_DELIVERY_DETAILDal.Instance.Update(dbModel);
                        }
                    }

                    return JSONhelper.ToJson(new { errCode = 0 });
                }

                return JSONhelper.ToJson(new { errCode = -1, Message = "数据不存在" });
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);

                return JSONhelper.ToJson(new { errCode = -1, Message = ex.Message });
            }
        }
    }
}
