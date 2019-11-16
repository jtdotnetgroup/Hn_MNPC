using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class V_DELIVER_BASEDal : BaseRepository<V_DELIVER_BASEModel>
    {
        public static V_DELIVER_BASEDal Instance
        {
            get { return SingletonProvider<V_DELIVER_BASEDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid("V_DELIVER_BASE", pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            return base.JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }
    }
}
