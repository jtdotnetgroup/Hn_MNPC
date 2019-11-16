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
    public class V_ICPRPOLICYDAL : BaseRepository<V_ICPRPOLICYMODEL>
    {
        public static V_ICPRPOLICYDAL Instance
        {
            get { return SingletonProvider<V_ICPRPOLICYDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string ICPRBILLID = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if(!ICPRBILLID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FID = '{0}' ", ICPRBILLID);
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }

    }
}
