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
    public class ICPRBIDATADAL : BaseRepository<ICPRBIDATAMODEL>
    {
        public static ICPRBIDATADAL Instance
        {
            get { return SingletonProvider<ICPRBIDATADAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return JsonDataForEasyUIdataGrid( pageindex, pagesize, filterJson,
                                                  sort, order);
        }

    }
}
