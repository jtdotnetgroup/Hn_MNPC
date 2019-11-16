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
using hn.DataAccess.model;

namespace hn.DataAccess.Dal
{
    public class BIINTERFACEDal : BaseRepository<BIINTERFACEMODEL>
    {
        public static BIINTERFACEDal Instance
        {
            get { return SingletonProvider<BIINTERFACEDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(BIINTERFACEMODEL)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            return base.JsonDataForEasyUIdataGrid( pageindex, pagesize, query.ToString(), sort, order);
        }
    }
}
