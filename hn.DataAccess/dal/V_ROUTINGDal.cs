using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class V_ROUTINGDal : BaseRepository<V_ROUTINGModel>
    {
        public static V_ROUTINGDal Instance
        {
            get { return SingletonProvider<V_ROUTINGDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                      string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(V_ROUTINGModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
