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
    public class ICPRBILLENTRYDAL : BaseRepository<ICPRBILLENTRYMODEL>
    {
        public static ICPRBILLENTRYDAL Instance
        {
            get { return SingletonProvider<ICPRBILLENTRYDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string ICPRBILLID = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!ICPRBILLID.IsNullOrEmpty())
            {
                query.AppendFormat("  ");
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, null, sort, order);
        }

        public IEnumerable<string> GetStatus()
        {
            return Instance.GetColumnList("FSTATUS");
        }

        public int GetConfirmStatus(string icprid)
        {
            string sql = string.Format("select count(1) from ICPRBILLENTRY where FSTATUS<>7 and FPLANID='{0}'", icprid);

            return DbUtils.Query(sql).Rows[0][0].ToInt();
        }

        public int GetCloseStatus(string icprid)
        {
            string sql = string.Format("select count(1) from ICPRBILLENTRY where FSTATUS<>5 and FPLANID='{0}'", icprid);

            return DbUtils.Query(sql).Rows[0][0].ToInt();
        }
    }
}
