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
    public class ICSTOCKBILLDAL : BaseRepository<ICSTOCKBILLMODEL>
    {
        public static ICSTOCKBILLDAL Instance
        {
            get { return SingletonProvider<ICSTOCKBILLDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(ICSTOCKBILLMODEL)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }        

    }
}
