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
    public class SYS_SUBDICSDAL : BaseRepository<SYS_SUBDICSMODEL>
    {
        public static SYS_SUBDICSDAL Instance
        {
            get { return SingletonProvider<SYS_SUBDICSDAL>.Instance; }
        }

        public string GetJson(int page, int pageSize, string parentID= null, string parentCode = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if(!parentID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCLASSID = '{0}'",parentID);
            }

            if (!parentCode.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCLASSID in ( select FID from SYS_DICS where SYS_DICS.FCLASSCODE = '{0}' ) ", parentCode);
            }

            return JsonDataForEasyUIdataGrid(page, pageSize, query.ToString(), sort, order);
        }

    }
}
