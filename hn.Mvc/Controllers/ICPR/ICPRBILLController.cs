using hn.Common;
using hn.Core;
using hn.DataAccess;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using hn.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class ICPRBILLController : Controller
    {
        //
        // GET: /Apply/

        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// 请购计划
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="FOrgID">销区ID</param>
        /// <param name="FBrandID">品牌ID</param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        [HttpPost]
        public string Data(
            int page = 1,
            int rows = 15,
            string startDate = null,
            string endDate = null,
            string FPREMISEID = null,
            string brandid = null,
            int FStatus = 0,
            string FCLASSAREA2NAME = null,
            string FTYPEID = null,
            bool chkclose = false,
            string sort = "FBILLDATE",
            string order = "DESC")
        {
            return V_ICPRBILLBLL.Instance.GetEasyUIJson(
                page, rows, startDate, endDate, FPREMISEID,
                FStatus, brandid, FCLASSAREA2NAME, FTYPEID, chkclose, false, sort, order);
        }

        /// <summary>
        /// 请购计划
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="FOrgID">销区ID</param>
        /// <param name="FBrandID">品牌ID</param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        [HttpPost]
        public string DataSelector(
            int page = 1,
            int rows = 15,
            string startDate = null,
            string endDate = null,
            string FPREMISEID = null,
            string brandid = null,
            int FStatus = 0,
            string FCLASSAREA2NAME = null,
            string FTYPEID = null,
            bool chkclose = false,
            string sort = "FBILLDATE",
            string order = "DESC")
        {
            return V_ICPRBILLBLL.Instance.GetEasyUIJson(
                page, rows, startDate, endDate, FPREMISEID,
                FStatus, brandid, FCLASSAREA2NAME, FTYPEID, chkclose, true, sort, order);
        }



        /// <summary>
        /// 请购计划明细列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="ICPRBILLID">请购计划ID</param>0
        /// <returns></returns>
        [HttpPost]
        public string EntryData(int page = 1, int rows = 9999, string ICPRBILLID = null)
        {
            return V_ICPRBILLENTRYBLL.Instance.GetEasyUIJson(page, rows, ICPRBILLID);
        }

        /// <summary>
        /// 请购计划附件列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="billid">请购计划ID</param>0
        /// <returns></returns>
        [HttpPost]
        public string AttachmentData(int page = 1, int rows = 15, string billid = null)
        {
            return TB_ATTACHMENTDal.Instance.GetJson(page, rows, billid);
        }

        /// <summary>
        /// 审核列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="billid">请购计划ID</param>0
        /// <returns></returns>
        [HttpPost]
        public string AuditData(int page = 1, int rows = 15, string billid = null)
        {
            return ICPRBILLAUDITDal.Instance.GetJson(page, rows, billid);
        }



        /// <summary>
        /// 请购计划明细列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="ICPRBILLID">请购计划ID</param>0
        /// <returns></returns>
        [HttpPost]
        public string EntryDataAll(
            int page = 1,
            int rows = 9999,
            string brandid = null,
            string startDate = null,
            string endDate = null,
            string classarea2name = null,
            string productname = null,
            string billno = null,
            string sort = "FSRCCODE",
            string order = "")
        {
            return V_ICPRBILLENTRYDAL.Instance.GetJsonAll(page, rows, brandid, startDate, endDate, classarea2name, productname, billno, "", sort, order);
        }

        [HttpPost]
        public string EntryDataAllConfim(
      int page = 1,
      int rows = 9999,
      string brandid = null,
      string startDate = null,
      string endDate = null,
      string classarea2name = null,
      string productname = null,
      string billno = null,
      string sort = "FSRCCODE",
      string order = "")
        {
            return V_ICPRBILLENTRYDAL.Instance.GetJsonAll(page, rows, brandid, startDate, endDate, classarea2name, productname, billno, "7", sort, order);
        }


        [HttpPost]
        public string EntryDataAllClose(
        int page = 1,
        int rows = 15,
        string brandid = null,
        string startDate = null,
        string endDate = null,
        string classarea2name = null,
        string productname = null,
        string billno = null,
        string sort = "FSRCCODE",
        string order = "")
        {
            return V_ICPRBILLENTRYDAL.Instance.GetJsonAll(page, rows, brandid, startDate, endDate, classarea2name, productname, billno, "3,7", sort, order);
        }

        /// <summary>
        /// 采购订单选择请购计划明细列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="ICPRBILLID">请购计划ID</param>
        /// <returns></returns>
        [HttpPost]
        public string EntryDataByICPO(int page = 1, int rows = 15, string ICPRBILLID = null)
        {
            return V_ICPRBILLENTRYDAL.Instance.GetJsonToICPO(page, rows, ICPRBILLID);
        }

        /// <summary>
        /// 获取特定价格政策
        /// </summary>
        /// <param name="FORGID">销区ID</param>
        /// <param name="FITEMID">商品ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetICPrice(string FORGID, string PRODUCTIDLIST)
        {
            if (FORGID.IsNullOrEmpty() || PRODUCTIDLIST.IsNullOrEmpty())
            {
                return JsonResultHelper.ToFailed("未能获取特定价格政策，参数错误！");
            }

            IEnumerable<V_ICPRICEPOLICYENTRYMODEL> list = V_ICPRICEPOLICYENTRYBLL.Instance.GetByProductWithFORGID(FORGID, PRODUCTIDLIST);

            return JsonResultHelper.ToSuccess("", list);
        }

        public JsonResult GetICPriceByICPR(string ICPRBILLID)
        {
            IEnumerable<V_ICPRPOLICYMODEL> list = V_ICPRPOLICYBLL.Instance.GetByICPRBILL(ICPRBILLID);

            return JsonResultHelper.ToSuccess("", list);
        }


        /// <summary>
        /// 请购计划保存
        /// </summary>
        /// <param name="ICPRBillJson"></param>
        /// <param name="ICPRBillEntryListJson"></param>
        /// <param name="ICRPBIDataListJson"></param>
        /// <param name="ICPRPolicyListJson"></param>
        /// <param name=""></param>
        /// <param name="deleteICPRBillEntryIDList"></param>
        /// <param name="deleteICRPBIDataIDList"></param>
        /// <param name="deleteICPRPolicyIDList"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(string ICPRBillJson, string ICPRBillDataJson, string ICPRATTACHMENTDataJson)
        {
            List<ICPRBillDataModel> list = JSONhelper.ConvertToObject<List<ICPRBillDataModel>>(ICPRBillDataJson);

            List<TB_ATTACHMENTModel> attaList = JSONhelper.ConvertToObject<List<TB_ATTACHMENTModel>>(ICPRATTACHMENTDataJson);


            string result = ICPRBILLBLL.Instance.Save(JSONhelper.ConvertToObject<ICPRBILLMODEL>(ICPRBillJson), list, attaList);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("保存请购计划成功");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }

        [HttpPost]
        public JsonResult ConfirmSave(string ICPRBillDataJson)
        {
            try
            {
                string action = Request["action"];
                List<ICPRBILLENTRYMODEL> list = JSONhelper.ConvertToObject<List<ICPRBILLENTRYMODEL>>(ICPRBillDataJson);

                foreach (ICPRBILLENTRYMODEL model in list)
                {
                    ICPRBILLENTRYMODEL uptModel = ICPRBILLENTRYDAL.Instance.Get(model.FID);
                    if (uptModel != null)
                    {

                        uptModel.FACCOUNT = model.FACCOUNT;
                        uptModel.FSTOREHOUSE = model.FSTOREHOUSE;
                        uptModel.FPOLICY = model.FPOLICY;
                        uptModel.FCOMMITQTY = model.FCOMMITQTY;
                        uptModel.FTRANSNAME = model.FTRANSNAME;
                        uptModel.FORDERREMARK1 = model.FORDERREMARK1;
                        uptModel.FORDERREMARK2 = model.FORDERREMARK2;
                        uptModel.FCONFIRM_USER = SysVisitor.Instance.UserId;
                        uptModel.FCONFIRM_TIME = DateTime.Now;
                        if (action == "confirm")
                        {
                            uptModel.FSTATUS = (int)Constant.ICPRBILL_FSTATUS.采购确认;
                        }
                        else if (action == "unconfirm")
                        {

                            var count = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICPRID = uptModel.FID }).ToList();
                            if (count.Count > 0)
                            {
                                continue;
                            }
                            uptModel.FSTATUS = (int)Constant.ICPRBILL_FSTATUS.审核通过;
                        }

                        ICPRBILLENTRYDAL.Instance.Update(uptModel);

                        if (action == "unconfirm")
                        {
                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.审核通过 }, new { FID = uptModel.FPLANID });
                        }
                        else
                        {
                            if (ICPRBILLENTRYDAL.Instance.GetConfirmStatus(uptModel.FPLANID) == 0)
                            {
                                ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.采购确认 }, new { FID = uptModel.FPLANID });
                            }
                        }
                    }

                }

                return JsonResultHelper.ToSuccess("处理完成，已发货的明细已自动跳过！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JsonResultHelper.ToFailed(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult CloseSave(string ICPRBillDataJson)
        {
            try
            {
                string action = Request["action"];
                List<ICPRBILLENTRYMODEL> list = JSONhelper.ConvertToObject<List<ICPRBILLENTRYMODEL>>(ICPRBillDataJson);

                foreach (ICPRBILLENTRYMODEL model in list)
                {
                    ICPRBILLENTRYMODEL uptModel = ICPRBILLENTRYDAL.Instance.Get(model.FID);
                    if (uptModel != null)
                    {
                        uptModel.FSTATUS = (int)Constant.ICPRBILL_FSTATUS.关闭;
                        uptModel.FCLOSE = 1;

                        ICPRBILLENTRYDAL.Instance.Update(uptModel);

                        if (ICPRBILLENTRYDAL.Instance.GetCloseStatus(uptModel.FPLANID) == 0)
                        {
                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.关闭 }, new { FID = uptModel.FPLANID });
                        }
                    }

                }

                return JsonResultHelper.ToSuccess("处理成功");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JsonResultHelper.ToFailed(ex.Message);
            }
        }


        /// <summary>
        /// 提交审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubmitAudit(string FID)
        {
            string result = ICPRBILLBLL.Instance.SubmitAudit(FID);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("提交审核完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }

        ///// <summary>
        ///// 审核请购计划
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult Audit(string FID, bool isPass)
        //{
        //   string result = ICPRBILLBLL.Instance.Audit(FID, isPass);

        //    if (result.IsNullOrEmpty())
        //    {
        //        return JsonResultHelper.ToSuccess("审核完成！");
        //    }
        //    else
        //    {
        //        return JsonResultHelper.ToFailed(result);
        //    }
        //}

        /// <summary>
        /// 审核请购计划明细
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ENTRYAudit(
            string FID,
            string FITEMID,
            string FENTRYID,
            bool isPass,
            decimal FAUDQTY,
            decimal FAUDAMOUNT)
        {
            string result = ICPRBILLENTRYBLL.Instance.Audit(FID, FITEMID, FENTRYID, isPass, FAUDQTY, FAUDAMOUNT);

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
                string ids = context["ids"];

                if (!string.IsNullOrEmpty(ids))
                {
                    string[] array = ids.Split(',');
                    foreach (string id in array)
                    {
                        //获取请购计划主记录
                        var model = ICPRBILLDAL.Instance.Get(id);
                        if(model.FSTATUS != 1)
                        {
                            return JSONhelper.ToJson(new { errCode = -1, errMsg = "单据状态已发生改变，请刷新当前页面，重新提交审核" });
                        }

                        ICPRBILLDAL.Instance.UpdateWhatWhere(new
                        {
                            FSTATUS = 2
                        },
                         new { FID = id });


                        ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new
                        {
                            FSTATUS = 2
                        },
                        new { FPLANID = id });

                        //开始审批流程
                        //获取审批流程，根据条件：品牌/厂家、经营场所、计划类型
                        var flows = V_ROUTINGDal.Instance.GetWhere(new { FBRANDID = model.FBRANDID, FPREMISEID = model.FPREMISEID, FTYPE = model.FTYPEID });
                        foreach (V_ROUTINGModel flow in flows)
                        {
                            //发送微信模板消息，通知审核人
                            //SendWxtTmpMessage(flow.FAPPROVER_USERNAME1);

                            if (flow.FNODECOUNT >= 1)
                            {
                                //生成预审批记录
                                ICPRBILLAUDITModel audit = new ICPRBILLAUDITModel();
                                audit.FBILLID = model.FID;
                                audit.FBILLTYPE = 1;
                                audit.FFLOWID = flow.FID;
                                audit.FAUDITOR = flow.FAPPROVER_USERNAME1;
                                audit.FSTATUS = 0;
                                audit.FSORT = 1;
                                ICPRBILLAUDITDal.Instance.Insert(audit);

                                //发送微信模板消息，通知审核人
                                SendWxtTmpMessage(flow.FAPPROVER_USERNAME1,model.FBILLNO, model.FID);
                            }
                            if (flow.FNODECOUNT >= 2)
                            {
                                //生成预审批记录
                                ICPRBILLAUDITModel audit = new ICPRBILLAUDITModel();
                                audit.FBILLID = model.FID;
                                audit.FBILLTYPE = 1;
                                audit.FFLOWID = flow.FID;
                                audit.FAUDITOR = flow.FAPPROVER_USERNAME2;
                                audit.FSTATUS = -1; //未发起
                                audit.FSORT = 2;
                                ICPRBILLAUDITDal.Instance.Insert(audit);
                            }
                            if (flow.FNODECOUNT >= 3)
                            {
                                //生成预审批记录
                                ICPRBILLAUDITModel audit = new ICPRBILLAUDITModel();
                                audit.FBILLID = model.FID;
                                audit.FBILLTYPE = 1;
                                audit.FFLOWID = flow.FID;
                                audit.FAUDITOR = flow.FAPPROVER_USERNAME3;
                                audit.FSTATUS = -1;//未发起
                                audit.FSORT = 3;
                                ICPRBILLAUDITDal.Instance.Insert(audit);
                            }

                            ICPRBILLDAL.Instance.UpdateWhatWhere(new
                            {
                                FIsAuditFlow = 1
                            },
                            new { FID = id });
                        }
                    }
                }


                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(ex.Message);
            }

        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool SendWxtTmpMessage(string user,string billno,string billid)
        {
            try
            {
                HttpItem item = new HttpItem();
                item.URL = ConfigurationManager.AppSettings["WxTmpMessageUrl"];
                item.Encoding = Encoding.UTF8;
                item.Method = "POST";
                item.Accept = "application/json";
                item.ContentType = "application/json;charset=utf-8";
                item.PostEncoding = Encoding.UTF8;

                var postdata = new
                {
                    agentid = 68,
                    touser = user,
                    title = "采购申请审批",
                    url = "http://wx.4006002222.com/qyweb/purchaseExamine/purchaseDetail/purchaseDetail.html?fid=" + billid,
                    description = "待审批单号:" + billno,
                    picurl = ""
                };

                item.Postdata = JSONhelper.ToJson(postdata);
                HttpResult result = HttpHelper.Instance.GetHtml(item);
                if (!string.IsNullOrEmpty(result.Html))
                {
                    ResultClass rs = JSONhelper.ConvertToObject<ResultClass>(result.Html);
                    if (rs.status == "00000")
                    {
                        return true;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
            }
        }

        public class ResultClass
        {
            public string status;
            public string response;
            public string message;
            public object date;
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
                    //获取请购计划主记录
                    var model = ICPRBILLDAL.Instance.Get(id);
                    if (model.FSTATUS != 2)
                    {
                        return JSONhelper.ToJson(new { errCode = -1, errMsg = "单据状态已发生改变，请刷新当前页面，重新处理" });
                    }


                    //开始审批流程
                    //获取审批流程，根据条件：品牌/厂家、经营场所、计划类型
                    var flows = V_ROUTINGDal.Instance.GetWhere(new { FBRANDID = model.FBRANDID, FPREMISEID = model.FPREMISEID, FTYPE = model.FTYPEID });
                    if (flows.Count() > 0){
                        return JSONhelper.ToJson(new { errCode = -1, errMsg = "已配有审批流程，无法单独进行审核操作" });
                    }
                    int entrycount = ICPRBILLENTRYDAL.Instance.CountWhere(new { FPLANID = id });
                    if (entrycount > 0)
                    {
                        ICPRBILLDAL.Instance.UpdateWhatWhere(
                            new
                            {
                                FSTATUS = status.ToInt(),
                                FCHECKERID = SysVisitor.Instance.UserId,
                                FCHECKDATE = DateTime.Now
                            },
                            new { FID = id });


                        ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(
                            new
                            {
                                FSTATUS = status.ToInt()
                            },
                            new { FPLANID = id });
                    }

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
                    var model = ICPRBILLDAL.Instance.Get(id);
                    if (model.FSTATUS != 3)
                    {
                        return JSONhelper.ToJson(new { errCode = -1, errMsg = "单据状态已发生改变，请刷新当前页面，重新处理" });
                    }

                    ICPRBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = 1,
                            FCHECKERID = ""
                        },
                        new { FID = id });


                    ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(
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

        [HttpPost]
        public string Comfirm(FormCollection context)
        {
            try
            {
                string ids = context["ids"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {

                    ICPRBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = 7,
                            FCHECKERID = ""
                        },
                        new { FID = id });


                    ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = 7
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

        public JsonResult Delete(string id)
        {
            try
            {
                ICPRBILLDAL.Instance.Delete(id);
                ICPRBILLENTRYDAL.Instance.DeleteWhere(new { FPLANID = id });

                return JsonResultHelper.ToSuccess("删除完成！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JsonResultHelper.ToFailed(ex.Message);
            }

        }

        [HttpPost]
        public string Import(FormCollection context)
        {
            var filename = Request["file"];
            var importCore = new ExcelImportCore();
            importCore.LoadFile(Server.MapPath(filename));
            var ds = importCore.GetAllTables(true);

            string message = "";
            List<object> list = new List<object>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string srcname = PublicMethod.GetString(row["厂家名称型号"]).Trim();
                if (!string.IsNullOrEmpty(srcname))
                {
                    var srcModels = SRCDal.Instance.GetWhere(new { FSRCNAME = srcname }).ToList();
                    if (srcModels.Count > 0)
                    {
                        ProductViewModel model = ProductViewDal.Instance.Get(srcModels[0].FPRODUCTID);
                        if (model != null)
                        {
                            list.Add(new
                            {
                                FID = model.FID,
                                FMODEL = model.FMODEL,
                                FITEMID = model.FID,
                                FPRODUCTNAME = model.FPRODUCTNAME,
                                FPRODUCTTYPE = model.FPRODUCTTYPE,
                                FPRODUCTCODE = model.FPRODUCTCODE,
                                FASKQTY = row["主单位数量"].ToDecimal(),
                                FUNITNAME = row["主计量单位"].ToStr(),
                                FUNITID = model.FUNITID,
                                FORDERUNIT = row["采购单位"].ToStr(),
                                FORDERUNITQTY = row["采购单位数量"].ToDecimal(),
                                FBRANDID = model.FBRANDID,
                                FBRANDNAME = model.FBRANDNAME,
                                FPKGFORMAT = model.FPKGFORMAT,
                                FCATEGORYID = model.FTYPEID,
                                FWEIGHT = model.FWEIGHT,
                                FVOLUME = model.FVOLUME,
                                FSTATUS = model.FSTATUS,
                                FNEEDDATE = row["要求发货时间"].ToStr(),
                                FUPDATETIME = model.FUPDATETIME,
                                FSTATUSNAME = "草稿",
                                FWEIGHTUNIT = "KG",
                                FRATE = model.FRATE,
                                FCOLORNO = row["色号"].ToStr(),
                                FREMARK = row["销区备注"].ToStr(),
                                FWHOLESALEPRICE = model.FPRIORITYP_L_RICE,
                                FBATCHNO = model.FBATCHNO,
                                FADVQTY = 0
                            });
                        }
                        else
                        {
                            message += string.Format("商品编码：{0}不存在;", srcModels[0].FPRODUCTID);
                        }
                    }
                    else
                    {
                        message += string.Format("厂家代码：{0}不存在;", srcname);
                    }
                }
            }

            return JSONhelper.ToJson(new { list = list, message = message });
        }


    }
}
