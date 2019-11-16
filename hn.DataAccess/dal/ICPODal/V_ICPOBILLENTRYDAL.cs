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
    public class V_ICPOBILLENTRYDAL : BaseRepository<V_ICPOBILLENTRYMODEL>
    {
        public static V_ICPOBILLENTRYDAL Instance
        {
            get { return SingletonProvider<V_ICPOBILLENTRYDAL>.Instance; }
        }

        public string GetJson(int page = 1, int rows = 15, string ICPOBILLID = null, int status = 0, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            query.AppendFormat(" and FICPOBILLID = '{0}' ", ICPOBILLID);

            if (status > 0)
            {
                query.AppendFormat(" and FSTATUS = {0} ", status);
            }

            return JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }

    }
}
