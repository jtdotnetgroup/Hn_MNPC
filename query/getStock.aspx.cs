using hn.Common;
using hn.DataAccess.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hn.Client.Service;

public partial class getStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["token"] != null && Request.QueryString["t_StartTime"] != null && Request.QueryString["t_EndTime"] != null && Request.QueryString["PageSize"] != null && Request.QueryString["PageIndex"] != null && Request.QueryString["CPXH"] != null)
        {
            try
            {
                int iStartDate = int.Parse(Request.QueryString["t_StartTime"]);
                int iEndDate = int.Parse(Request.QueryString["t_EndTime"]);
                int iPageSize = int.Parse(Request.QueryString["PageSize"]);
                int iPageIndex = int.Parse(Request.QueryString["PageIndex"]);
                string strCPXH = Request.QueryString["CPXH"];
                string str = getStockData("", iStartDate, iEndDate, iPageSize, iPageIndex, strCPXH);
                Response.Write(str);
                Response.End();
            }
            catch(Exception ee)
            {
               // Response.Write("参数出错");
                Response.End();
            }
        }
        else
        {
            Response.Write("参数出错");
            Response.End();
        }
    }


    public string getStockData(string token, int t_StartTime, int t_EndTime, int PageSize, int PageIndex, string CPXH)
    {
        try
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT V_QUERY_STOCK.*,rownum n from V_QUERY_STOCK where 1=1 ");

            if (t_StartTime != 0)
            {
                sql.AppendLine(" and to_number(to_char(ftime,'yyyymmdd'))>='" + t_StartTime + "'");
            }

            if (t_EndTime != 0)
            {
                sql.AppendLine(" and to_number(to_char(ftime,'yyyymmdd'))<='" + t_EndTime + "'");
            }

            if (CPXH != "")
            {
                sql.AppendLine(" and cpxh like '%" + CPXH + "%'");
            }
            sql.AppendLine(" order by ftime desc");

            if (PageIndex < 1) PageIndex = 1;
            if (PageSize < 1) PageSize = 1;

            string strSql = "select t.*　from (" + sql.ToString() + ") t where n between " + PageSize * (PageIndex - 1) + 1 + " and " + PageSize * PageIndex + "";


            var list = V_QUERY_STOCKDal.Instance.Query(strSql).ToList();


            hn.Common.LogHelper.WriteLog(sql.ToStr());



            return JsonHelper.ToJson(list);
        }
        catch (Exception ee)
        {
            return ee.ToStr();
        }


    }

}