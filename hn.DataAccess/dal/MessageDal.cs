using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.SqlServer;
using hn.Common.Provider;
using hn.Core.Model;
using hn.DataAccess.Model;
using hn.Core;

namespace hn.DataAccess.Dal
{
    public class MessageDal : BaseRepository<MessageModel>
    {
        public static MessageDal Instance
        {
            get { return SingletonProvider<MessageDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(MessageModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public DataTable GetList(int pageindex, int pagesize, string driverids, string where, string sort, string order)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine("  FROM (SELECT ROWNUM AS rowno, T.*");
            sql.AppendLine("          FROM (SELECT T1.*, T2.NAME,T2.PLAT_FORM_NAME");
            sql.AppendLine("                  FROM TB_MESSAGE T1");
            sql.AppendLine("                 LEFT JOIN TB_APP_USER T2");
            sql.AppendLine("                    ON T1.USER_ID = T2.FID");
            // sql.AppendLine("                 LEFT JOIN " + SysVisitor.Instance.TmsUser + "." + "TMS_PLATFORM T3");
            // sql.AppendLine("                    ON T2.PLAT_FORM_ID = T3.ID");
            sql.AppendLine("                  WHERE 1=1 ");
            //sql.AppendLine("                  WHERE T2.ID IN ("+ driverids + ")) T) table_alias");
            

            if (!string.IsNullOrEmpty(SysVisitor.Instance.DataStartDate))
            {
                sql.AppendLine("                AND (T1.SEND_TIME >= to_date('" + SysVisitor.Instance.DataStartDate + "','yyyy-mm-dd'))");
            }
            sql.AppendLine("                    order by SEND_TIME desc) T) table_alias");
            sql.AppendLine(" WHERE table_alias.rowno > " + (pageindex - 1) * pagesize);
            sql.AppendLine("   and table_alias.rowno <= " + pageindex * pagesize);

            if (where != "" && where != "()")
            {
                sql.AppendLine("   and " + where.Replace("SEND_TIME", "TO_CHAR(SEND_TIME, 'YYYY-MM-DD')"));
            }

            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                sql.AppendLine("  ORDER BY " + sort + " " + order);
            }
            else
            {
                sql.AppendLine("  order by SEND_TIME desc");
            }


            return DbUtils.Query(sql.ToString());
        }

        public int GetCount(string driverids, string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT COUNT(1)");
            sql.AppendLine("  FROM (SELECT ROWNUM AS rowno, T.*");
            sql.AppendLine("          FROM (SELECT T1.*, T2.NAME,T2.PLAT_FORM_NAME");
            sql.AppendLine("                  FROM TB_MESSAGE T1");
            sql.AppendLine("                 LEFT JOIN TB_APP_USER T2");
            sql.AppendLine("                    ON T1.USER_ID = T2.FID");
            ////sql.AppendLine("                 LEFT JOIN " + SysVisitor.Instance.TmsUser + "." + "TMS_PLATFORM T3");
            ////sql.AppendLine("                    ON T2.PLAT_FORM_ID = T3.ID");
            sql.AppendLine("                  WHERE 1=1 ");
            //sql.AppendLine("                  WHERE T2.ID IN ("+ driverids + ")) T) table_alias");


            if (!string.IsNullOrEmpty(SysVisitor.Instance.DataStartDate))
            {
                sql.AppendLine("                AND (T1.SEND_TIME >= to_date('" + SysVisitor.Instance.DataStartDate + "','yyyy-mm-dd'))");
            }
            sql.AppendLine("                  ) T) table_alias");
            sql.AppendLine(" WHERE 1=1");

            if (where != "" && where != "()")
            {
                sql.AppendLine("   and " + where.Replace("SEND_TIME", "TO_CHAR(SEND_TIME, 'YYYY-MM-DD')"));
            }

            DataTable table = DbUtils.Query(sql.ToString());

            return PublicMethod.GetInt(table.Rows[0][0]);
        }

        public List<MessageModel> GetMessageByDriverID(string driverid, string status)
        {
            if (status == "1")
            {
                return GetWhere(new { RECEIVE_ID = driverid, IS_RREAD = 0 }).ToList();
            }
            else if (status == "2")
            {
                return GetWhere(new { RECEIVE_ID = driverid, IS_RREAD = 1 }).ToList();
            }

            return GetWhere(new { RECEIVE_ID = driverid }).ToList();

        }

        public List<MessageModel> GetMessageByUserID(string userid, string status)
        {
            if (status == "1")
            {
                //return DbUtils.ToModel<MessageModel>(DbUtils.Query(string.Format("SELECT * FROM TB_MESSAGE WHERE (USER_ID='{0}' OR USER_ID='0') AND IS_RREAD = 0 ", userid))).ToList();
                return GetWhere(new { USER_ID = userid, IS_RREAD = 0 }).ToList();
            }
            else if (status == "2")
            {
                 return GetWhere(new { USER_ID = userid, IS_RREAD = 1 }).ToList();
               // return DbUtils.ToModel<MessageModel>(DbUtils.Query(string.Format("SELECT * FROM TB_MESSAGE WHERE (USER_ID='{0}' OR USER_ID='0') AND IS_RREAD = 0 ", userid))).ToList();
            }

            return GetWhere(new { USER_ID = userid }).ToList();
           // return DbUtils.ToModel<MessageModel>(DbUtils.Query(string.Format("SELECT * FROM TB_MESSAGE WHERE (USER_ID='{0}' OR USER_ID='0')  ", userid))).ToList();

        }

        public void UpdateStatus(string fid)
        {
            // DbUtils.ExecuteNonQuery("UPDATE TB_MESSAGE SET IS_RREAD=1 WHERE FID='" + fid+"'",new {});
            MessageDal.Instance.UpdateWhatWhere(new { IS_RREAD = 1 }, new { FID = fid });
        }
    }
}
