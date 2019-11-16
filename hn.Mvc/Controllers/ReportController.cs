using hn.Common;
using hn.DataAccess.dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace hn.Mvc.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Report1()
        {
            return View();
        }

        public ActionResult Report2()
        {
            return View();
        }

        public ActionResult Report3()
        {
            return View();
        }

        [HttpPost]
        public string Report1Data(string billno, string preM_name, string src_name, string startdate, string enddate)
        {
            if (string.IsNullOrEmpty(billno) && string.IsNullOrEmpty(src_name) && string.IsNullOrEmpty(preM_name) && string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate))
            {
                return "";
            }
            else
            {
                DataTable table = ReportDAL.Instance.Report1(billno, preM_name, src_name, startdate, enddate);

                return JSONhelper.FormatJSONForEasyuiDataGrid(table.Rows.Count, table);
            }
        }

        [HttpPost]
        public string Report2Data(string startdate, string enddate)
        {
            if (string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate))
            {
                return "";
            }
            else
            {
                DataTable table = ReportDAL.Instance.Report2(startdate, enddate);

                return JSONhelper.FormatJSONForEasyuiDataGrid(table.Rows.Count, table);
            }
        }

        [HttpPost]
        public string Report3Data(string startdate, string enddate,string typename, string fbrandname)
        {
            if (string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate))
            {
                return "";
            }
            else
            {
                DataTable table = ReportDAL.Instance.Report3(startdate, enddate, typename, fbrandname);

                return JSONhelper.FormatJSONForEasyuiDataGrid(table.Rows.Count, table);
            }
        }

        public string Export1()
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

        public string Export2()
        {
            try
            {
                string startdate = Request["startdate"];
                string enddate = Request["enddate"];

                DataTable table = ReportDAL.Instance.Report2(startdate, enddate);

                string title = "开发厂家发货记录表";

                string filename = "\\Excel\\开发厂家发货记录表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";

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

        public string Export3()
        {
            try
            {
                string startdate = Request["startdate"];
                string enddate = Request["enddate"];
                string typename = Request["typename"];
                string fbrandname = Request["fbrandname"];

                DataTable table = ReportDAL.Instance.Report3(startdate, enddate, typename, fbrandname);

                string title = "销区提报采购计划汇总表";

                string filename = "\\Excel\\销区提报采购计划汇总表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";

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
