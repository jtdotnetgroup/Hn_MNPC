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
    public class SYS_UPDATELOGDAL : BaseRepository<SYS_UPDATELOGMODEL>
    {
        public static SYS_UPDATELOGDAL Instance
        {
            get { return SingletonProvider<SYS_UPDATELOGDAL>.Instance; }
        }

        public string GetJson(int page = 1, int pageSize=3, string sort = "FUpdateTime", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            return JsonDataForEasyUIdataGrid(page, pageSize, query.ToString(), sort, order);
        }

        public IEnumerable<SYS_UPDATELOGMODEL> GetPageWtihRecordCount(int page, int pageSize, out int recordCount, string sort = "FUpdateTime", string order = "asc")
        {
            return base.GetPageWtihRecordCount(page,pageSize,out recordCount);
        }

    }
}
