using hn.Common;
using hn.Common.Data;
using hn.Core;
using hn.DataAccess;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using hn.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class ICSEOUTBILLController : Controller
    {
        //
        // GET: /Apply/

        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public JsonResult Save(string ICSEOUTBILLJson, string ICSEOUTBILLENTRYListJson)
        {
            string result = ICSEOUTBILLBLL.Instance.Save(JSONhelper.ConvertToObject<ICSEOUTBILLMODEL>(ICSEOUTBILLJson),
                 JSONhelper.ConvertToObject<IEnumerable<ICSEOUTBILLENTRYMODEL>>(ICSEOUTBILLENTRYListJson));

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("保存完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
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
            string startDate = null,
            string endDate = null,
            string FOrgID = null,
            int status = 0,
            string FPREMISEID = null,
            string FCLASSAREA2NAME = null,
            string FCARNUMBER = null,
            bool chkclose = false)
        {
            return V_ICSEOUTBILLBLL.Instance.GetEasyUIJson(page, rows, startDate, endDate, FOrgID, status,
                FPREMISEID, FCLASSAREA2NAME, FCARNUMBER, chkclose);
        }

        [HttpPost]
        public string ICPRData(
            int page = 1, 
            int rows = 15,
             string brandid = null, 
             string startDate = null, 
             string endDate = null, 
             string classarea2name = null,
             string productname = null,
             string premisebrandname = null,
             string icprbillno = null,
             string sort = "FSRCCODE",
             string order = "")
        {
            return V_ICPRBILLENTRYDAL.Instance.GetJsonToICSEOUT(page, rows, brandid, startDate, endDate, classarea2name, productname, premisebrandname, icprbillno, sort, order);
        }


        /// <summary>
        /// 明细数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="ICSEOUTBILLID"></param>
        /// <returns></returns>
        [HttpPost]
        public string ENTRYData(int page = 1, int rows = 15, string FICSEOUTID = null)
        {
            return V_ICSEOUTBILLENTRYBLL.Instance.GetEasyUIJson(page, rows, FICSEOUTID);
        }

        /// <summary>
        /// 明细数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="ICSEOUTBILLID"></param>
        /// <returns></returns>
        [HttpPost]
        public string SRCData(string itemid = null, string ICPRBILLNO = null, string FICPRENTRYID = null, string FSTOCK = null)
        {
            List<object> list = new List<object>();

            List<string> srcnumbers = new List<string>();
            var srclist = SRCDal.Instance.GetWhere(new { FPRODUCTID = itemid }).ToList();
            foreach (var model in srclist)
            {
                srcnumbers.Add(model.FSRCCODE);
            }
            int pagecount = 0;
            APIService.APIServiceClient api = new APIService.APIServiceClient();
            DataTable table = api.WmStock(out pagecount, string.Join(",", srcnumbers.ToArray()), "", FSTOCK, "","","","","", 1000, 1,true,false);
            foreach (var model in srclist)
            {
                var query = table.AsEnumerable().Where<DataRow>(m => m["FNumber"].ToString() == model.FSRCCODE);
                foreach (var row in query)
                {
                    list.Add(new
                    {
                        FID = model.FID,
                        FPRODUCTID = model.FPRODUCTID,
                        FSRCNAME = model.FSRCNAME,
                        FSRCCODE = model.FSRCCODE,
                        FSRCMODEL = model.FSRCMODEL,
                        FSRCUNIT = model.FUNIT,
                        FORDERUNIT = model.FORDERUNIT,
                        FRATE = model.FRATE,
                        FBATCHNO = row["FBatchNo"],
                        FCOLORNO = row["FColorNo"],
                        FSTOCK = row["FStockName"],
                        FSTOCKPLACE = row["FSPName"],
                        FQTY = row["FBasicQty"],
                        FLEVEL = row["FGrade"],
                        FWDR = row["FWDRID"],
                        FCOMMITQTY = System.DBNull.Value,
                        FHNAMOUNT = 0,
                        ICPRBILLNO = ICPRBILLNO,
                        FICPRENTRYID = FICPRENTRYID,
                        FREMARK = ""
                    });
                }
            }

            return JSONhelper.FormatJSONForEasyuiDataGrid(list.Count, list);
        }

        /// <summary>
        /// 采购订单下单记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public string ICPOHISTORYData(int page = 1, int rows = 15)
        {
            return V_ICPOHISTORYBLL.Instance.GetEasyUIJson(page, rows);
        }

        /// <summary>
        /// 采购订单下单记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<V_ICPOHISTORYMODEL> ICPOHISTORYList()
        {
            return V_ICPOHISTORYBLL.Instance.GetAll();
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
                        var model = ICSEOUTBILLDAL.Instance.Get(id);
                        if (model.FSTATUS != 1)
                        {
                            return JSONhelper.ToJson(new { errCode = -1, errMsg = "单据状态已发生改变，请刷新当前页面，重新处理" });
                        }

                        ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new
                        {
                            FSTATUS = 2
                        }, new { FID = id });
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
        public string Audit(FormCollection context)
        {
            try
            {
                string ids = context["ids"];
                string status = context["status"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    var model = ICSEOUTBILLDAL.Instance.Get(id);
                    if (model.FSTATUS != 2)
                    {
                        return JSONhelper.ToJson(new { errCode = -1, errMsg = "单据状态已发生改变，请刷新当前页面，重新处理" });
                    }

                    ICSEOUTBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = status.ToInt(),
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
                    var model = ICSEOUTBILLDAL.Instance.Get(id);
                    if (model.FSTATUS != 3)
                    {
                        return JSONhelper.ToJson(new { errCode = -1, errMsg = "单据状态已发生改变，请刷新当前页面，重新处理" });
                    }

                    ICSEOUTBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = 1,
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

        [HttpPost]
        public string Sync(FormCollection context)
        {
            try
            {
                string resultjson = "";
                string ids = context["ids"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {

                    APIService.APIServiceClient api = new APIService.APIServiceClient();

                    var icseoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FID = id });
                    var icseoutentrylist = V_ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICSEOUTID = id });

                    resultjson = api.ICSEOUTBILLSync(icseoutlist.ToArray(), icseoutentrylist.ToArray());
                    if (!string.IsNullOrEmpty(resultjson))
                    {
                        DataResult result = JSONhelper.ConvertToObject<DataResult>(resultjson);
                        if (result != null)
                        {
                            ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FFACTORYSTATUS = 1, FSYNCSTATUS = 1 }, new { FID = id });
                            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(new { FERR_MESSAGE = "" }, new { FICSEOUTID = id });
                        }

                        LogHelper.WriteLog(resultjson);
                    }
                }

                return resultjson;
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
                ICSEOUTBILLDAL.Instance.Delete(id);

                var list = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICSEOUTID = id }).ToList();

                ICSEOUTBILLENTRYDAL.Instance.DeleteWhere(new { FICSEOUTID = id });
                foreach (var model in list)
                {
                    string sql = string.Format("SELECT SUM(FCOMMITQTY*FRATE) FROM V_ICSEOUTBILLENTRY WHERE FICPRID='{0}'", model.FICPRID);
                    DataTable table = DbUtils.Query(sql);
                    decimal total = PublicMethod.GetDecimal(table.Rows[0][0]);
                    ICPRBILLENTRYMODEL icprModel = ICPRBILLENTRYDAL.Instance.Get(model.FICPRID);
                    if (icprModel != null)
                    {
                        icprModel.FLEFTAMOUNT = icprModel.FASKQTY - total;

                        if (icprModel.FLEFTAMOUNT > 0)
                        {
                            icprModel.FSTATUS = 7;

                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.采购确认 }, new { FID = icprModel.FPLANID });
                        }

                        ICPRBILLENTRYDAL.Instance.Update(icprModel);


                    }
                }
                return JsonResultHelper.ToSuccess("删除完成！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JsonResultHelper.ToFailed(ex.Message);
            }

        }

        /// <summary>
        /// 匹配价格政策
        /// </summary>
        /// <returns></returns>
        public JsonResult Price(string brandid, string clientid, string conEntry)
        {
            List<JsonEntry> entrys = JSONhelper.ConvertToObject<List<JsonEntry>>(conEntry);

            List<string> itemids = new List<string>();
            foreach (JsonEntry entry in entrys)
            {
                itemids.Add(entry.FITEMID);
            }

            int index = 1;
            List<JsonEntry> list = new List<JsonEntry>();
            DataTable table = ICSEOUTBILLENTRYDAL.Instance.GetICPRICEPOLICYENTRY(string.Join("','", itemids.ToArray()), brandid, clientid);
            foreach (JsonEntry entry in entrys)
            {
                var query = table.AsEnumerable().Where(m => m["FITEMID"].ToString() == entry.FITEMID);
                foreach (var row in query)
                {
                    if (row["FQTYLIMIT"].ToDecimal() < entry.FCOMMITQTY.ToDecimal())
                    {
                        entry.FCOMMITQTY = (entry.FCOMMITQTY.ToDecimal() - row["FQTYLIMIT"].ToDecimal()).ToString();

                        JsonEntry m = entry.clone();
                        m.FENTRYID = index;
                        m.FCOMMITQTY = row["FQTYLIMIT"].ToString();
                        m.FACCOUNTPRICE = row["FACCOUNTPRICE"].ToDecimal();
                        m.FPOLICYBILLNO = row["FBILLNO"].ToString();
                        m.FPOLICYNAME = row["FNAME"].ToString();
                        m.FPRICEPOLICYENTRYID = row["FID"].ToString();
                        list.Add(m);
                    }
                    else
                    {
                        JsonEntry m = entry.clone();
                        m.FENTRYID = index;
                        m.FACCOUNTPRICE = row["FACCOUNTPRICE"].ToDecimal();
                        m.FPOLICYBILLNO = row["FBILLNO"].ToString();
                        m.FPOLICYNAME = row["FNAME"].ToString();
                        m.FPRICEPOLICYENTRYID = row["FID"].ToString();
                        list.Add(m);

                        break;
                    }

                    index++;
                }
            }

            return JsonResultHelper.ToSuccess("处理完成！", list);


            //string result = ICSEOUTBILLBLL.Instance.Save(JSONhelper.ConvertToObject<ICSEOUTBILLMODEL>(ICSEOUTBILLJson),
            //     JSONhelper.ConvertToObject<IEnumerable<ICSEOUTBILLENTRYMODEL>>(ICSEOUTBILLENTRYListJson));

            //if (result.IsNullOrEmpty())
            //{
            //    return JsonResultHelper.ToSuccess("保存完成！");
            //}
            //else
            //{
            //    return JsonResultHelper.ToFailed(result);
            //}

            //return JsonResultHelper.ToSuccess("保存完成！");
        }
    }

    public class JsonEntry
    {
        public decimal FENTRYID { get; set; }
        public string FMODEL { get; set; }
        public string FPRODUCTNAME { get; set; }
        public string FPRODUCTTYPE { get; set; }
        public string FPRODUCTCODE { get; set; }
        public string FUNITNAME { get; set; }
        public string FBATCHNO { get; set; }
        public string FCOLORNO { get; set; }
        public string FREMARK { get; set; }
        public string FSRCID { get; set; }
        public string FSRCNAME { get; set; }
        public string FSRCCODE { get; set; }
        public string FSRCMODEL { get; set; }
        public string FSRCUNIT { get; set; }
        public string FORDERUNIT { get; set; }
        public string FSTOCK { get; set; }
        public string FSTOCKPLACE { get; set; }
        public string FCOMMITQTY { get; set; }
        public string FPRICEPOLICYENTRYID { get; set; }
        public string FICPRID { get; set; }
        public decimal FWEIGHT { get; set; }
        public decimal FVOLUME { get; set; }
        public string FITEMID { get; set; }
        public decimal FACCOUNTPRICE { get; set; }
        public string FPOLICYBILLNO { get; set; }
        public string FPOLICYNAME { get; set; }
        public string ICPRBILLNO { get; set; }
        public string FICPRENTRYID { get; set; }

        public JsonEntry clone()
        {
            return new JsonEntry()
            {
                FENTRYID = FENTRYID,
                FMODEL = FMODEL,
                FPRODUCTNAME = FPRODUCTNAME,
                FPRODUCTTYPE = FPRODUCTTYPE,
                FPRODUCTCODE = FPRODUCTCODE,
                FUNITNAME = FUNITNAME,
                FBATCHNO = FBATCHNO,
                FCOLORNO = FCOLORNO,
                FREMARK = FREMARK,
                FSRCID = FSRCID,
                FSRCNAME = FSRCNAME,
                FSRCCODE = FSRCCODE,
                FSRCMODEL = FSRCMODEL,
                FSRCUNIT = FSRCUNIT,
                FORDERUNIT = FORDERUNIT,
                FSTOCK = FSTOCK,
                FSTOCKPLACE = FSTOCKPLACE,
                FCOMMITQTY = FCOMMITQTY,
                FPRICEPOLICYENTRYID = FPRICEPOLICYENTRYID,
                FICPRID = FICPRID,
                FWEIGHT = FWEIGHT,
                FVOLUME = FVOLUME,
                FITEMID = FITEMID,
                FACCOUNTPRICE = FACCOUNTPRICE,
                ICPRBILLNO = ICPRBILLNO,
                FICPRENTRYID = FICPRENTRYID
            };

        }
    }
}
