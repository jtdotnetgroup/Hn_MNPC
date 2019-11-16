using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class V_UNITGROUPDal : BaseRepository<V_UNITGROUPModel>
    {
        public static V_UNITGROUPDal Instance
        {
            get { return SingletonProvider<V_UNITGROUPDal>.Instance; }
        }

        public IEnumerable<V_UNITGROUPModel> GetChildren(string parentid = "0")
        {
            return GetAll();
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                      string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(V_UNITGROUPModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

    }
}
