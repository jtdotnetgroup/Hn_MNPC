using hn.Common;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace hn.Mvc.Controllers
{
    public class SettlementController : BaseController
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            //工具栏
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public string Data(string settleorg, string brand, string startdate, string enddate, int page = 1,
            int rows = 15)
        {
            if (string.IsNullOrEmpty(settleorg) && string.IsNullOrEmpty(brand) && string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate))
            {
                return "";
            }
            else
            {
                return TB_SETTLEMENTDal.Instance.GetJson(page, rows, brand, settleorg, startdate, enddate);
            }
        }

        [HttpPost]
        public string ImportData(string startdate, string enddate)
        {
            if (string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate))
            {
                return "";
            }
            else
            {
                DataTable table = ReportDAL.Instance.GetSettlementData(startdate, enddate);
                table.Columns.Add(new DataColumn("FPOLICY_PRICE_NO", typeof(string)));
                table.Columns.Add(new DataColumn("FPOLICY_PRICE", typeof(decimal)));
                table.Columns.Add(new DataColumn("FCOST_RULES", typeof(string)));

                foreach (DataRow row in table.Rows)
                {
                    if (!string.IsNullOrEmpty(row["FPRODUCTID"].ToStr()))
                    {
                        var policys = TB_PRICEPOLICYDal.Instance.GetWhere(new { FCLIENTACCOUNT = row["CUSTOMER_NO"], FPRODUCTNUMBER = row["FPRODUCTID"], FBATCHES_STATUS = "Y" }).ToList();

                        if (policys.Count() > 0)
                        {
                            row["FPOLICY_PRICE_NO"] = policys[0].FPRICENUMBER;
                            row["FPOLICY_PRICE"] = policys[0].FAUDITPRICE;
                        }
                    }

                    if (!string.IsNullOrEmpty(row["FCODE"].ToStr()))
                    {
                        var cost = TB_UNLOADING_COSTDal.Instance.GetWhere(new { FARRIVAL_ADDR_BP = row["FCODE"], FPRINTMSG = row["WAREHOUSE"], FTRANSPORTTPE = 1 }).ToList();
                        if (cost.Count() > 0)
                        {
                            row["FCOST_RULES"] = cost[0].FCOST_RULES;
                        }
                    }


                }

                return JSONhelper.FormatJSONForEasyuiDataGrid(table.Rows.Count, table);
            }
        }

        [HttpPost]
        public string Save(FormCollection context)
        {
            try
            {
                string data = context["data"];
                if (!string.IsNullOrEmpty(data))
                {
                    List<TB_SETTLEMENTModel> list = JSONhelper.ConvertToObject<List<TB_SETTLEMENTModel>>(data);
                    foreach (TB_SETTLEMENTModel model in list)
                    {
                        TB_SETTLEMENTModel m = TB_SETTLEMENTDal.Instance.Get(model.FID);
                        if (m != null)
                        {
                            m.FSETTLE_ORG = model.FSETTLE_ORG;
                            m.FSPID = model.FSPID;
                            m.FBATCH_NO = model.FBATCH_NO;
                            m.FBATCH_EXPLAIN = model.FBATCH_EXPLAIN;
                            m.FSHIPPING_NO = model.FSHIPPING_NO;
                            m.FCONFIRM_PRICE = model.FCONFIRM_PRICE;
                            m.FCONFIRM_AMOUNT = model.FCONFIRM_AMOUNT;
                            m.FJDE_ORDER = model.FJDE_ORDER;
                            m.FPOLICY_PRICE_NO = model.FPOLICY_PRICE_NO;
                            m.FPOLICY_PRICE = model.FPOLICY_PRICE;
                            m.FCOST_RULES = model.FCOST_RULES;
                            m.FPLAN_FREIGHT_PRICE = model.FPLAN_FREIGHT_PRICE;
                            m.FPLAN_FREIGHT_AMOUNT = model.FPLAN_FREIGHT_AMOUNT;
                            m.FWAYBILL_NO = model.FWAYBILL_NO;
                            m.FFREIGHT = model.FFREIGHT;
                            m.FREMARK1 = model.FREMARK1;
                            m.FREMARK2 = model.FREMARK2;
                            m.FREMARK3 = model.FREMARK3;

                            TB_SETTLEMENTDal.Instance.Update(m);
                        }
                        else
                        {
                            TB_SETTLEMENTDal.Instance.Insert(model);
                        }
                    }

                    return JSONhelper.ToJson(new { errCode = 0 });
                }

                return JSONhelper.ToJson(new { errCode = -1, Message = "数据不存在" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                return JSONhelper.ToJson(new { errCode = -1, Message = ex.Message });
            }
        }
        public string Export()
        {
            try
            {
                string billno = Request["billno"];
                string preM_name = Request["classarea2Name"];
                string src_name = Request["brand"];
                string startdate = Request["startdate"];
                string enddate = Request["enddate"];

                DataTable table = ReportDAL.Instance.Report1(billno, preM_name, src_name, startdate, enddate);

                string title = "开发采购申请执行跟踪表";

                string filename = "\\Excel\\开发采购申请执行跟踪表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";

                if (ExcelHelper.TableToExcel(table, title, Server.MapPath("~" + filename), new string[] { }))
                {
                    Response.Redirect(filename);
                    return filename;
                }
                else
                {
                    return "-1";
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                return "-1";
            }

        }

    }
}
