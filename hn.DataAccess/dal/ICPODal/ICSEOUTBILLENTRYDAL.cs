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
using hn.DataAccess.Bll;

namespace hn.DataAccess.Dal
{
    public class ICSEOUTBILLENTRYDAL : BaseRepository<ICSEOUTBILLENTRYMODEL>
    {
        public static ICSEOUTBILLENTRYDAL Instance
        {
            get { return SingletonProvider<ICSEOUTBILLENTRYDAL>.Instance; }
        }

        public string GetJson(int page = 1, int rows = 15, string startDate = null, string endDate = null, string FOrgID = null, int status = 0, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBILLDATE >= ({0}) ", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBILLDATE >= ({0}) ", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!FOrgID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FORGID = '{0}' ", FOrgID);
            }

            if (status > 0)
            {
                query.AppendFormat(" and FSTATE = '{0}' ", status);
            }

            return JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }

        public DataTable GetICPRICEPOLICYENTRY(string itemids, string brandid,string clientid)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT T1.*,");
            sql.AppendLine("       T2.FBEGDATE,");
            sql.AppendLine("       T2.FENDDATE,");
            sql.AppendLine("       T2.FBRANDID,");
            sql.AppendLine("       T2.FCLIENTID,");
            sql.AppendLine("       T2.FNAME,");
            sql.AppendLine("       T2.FBILLNO,");
            sql.AppendLine("       T2.FPOLICYTYPE,");
            sql.AppendLine("       T2.FPRIORITY");
            sql.AppendLine("  FROM ICPRICEPOLICYENTRY T1");
            sql.AppendLine(" INNER JOIN ICPRICEPOLICY T2");
            sql.AppendLine("    ON T1.FPOLICYID = T2.FID");
            sql.AppendLine(" WHERE T1.FITEMID IN ('" + itemids + "')");
            sql.AppendLine("   AND (SYSDATE BETWEEN T2.FBEGDATE AND T2.FENDDATE)");
            sql.AppendFormat("   AND T2.FBRANDID = '{0}'", brandid);
            sql.AppendFormat("   AND T2.FCLIENTID = '{0}'", clientid);
            sql.AppendLine(" ORDER BY T2.FPRIORITY DESC");

            return DbUtils.Query(sql.ToString());
        }

    }
}
