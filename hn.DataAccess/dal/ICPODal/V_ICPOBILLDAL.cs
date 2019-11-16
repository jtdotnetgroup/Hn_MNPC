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
    public class V_ICPOBILLDAL : BaseRepository<V_ICPOBILLMODEL>
    {
        public static V_ICPOBILLDAL Instance
        {
            get { return SingletonProvider<V_ICPOBILLDAL>.Instance; }
        }

        public string GetJson(int page = 1, int rows = 15, string startDate = null, string endDate = null, string brandid = null, int status = 0, string sort = "FID", string order = "asc")
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

            if (!brandid.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBRANDID = '{0}' ", brandid);
            }

            if (status > 0)
            {
                query.AppendFormat(" and FSTATUS = '{0}' ", status);
            }

            return JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }

    }
}
