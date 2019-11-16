using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class BrandDal : BaseRepository<BrandModel>
    {
        public static BrandDal Instance
        {
            get { return SingletonProvider<BrandDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(BrandModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
