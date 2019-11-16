using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace hn.Client.Service
{
    public partial class APIService
    {
    

        public string getStock(string token,int t_StartTime, int t_EndTime, int PageSize, int PageIndex, string CPXH)
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


                LogHelper.WriteLog(sql.ToStr());



                return JsonHelper.ToJson(list);
            }
            catch(Exception ee)
            {
                return ee.ToStr();
            }

          
        }

        public string Hello(string name)
        {
            return "111111"+ name;
        }

    }
}