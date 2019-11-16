using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class TB_UNLOADING_COSTDal : BaseRepository<TB_UNLOADING_COSTModel>
    {
        public static TB_UNLOADING_COSTDal Instance
        {
            get { return SingletonProvider<TB_UNLOADING_COSTDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TB_UNLOADING_COSTModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            return base.JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }
    }
}
