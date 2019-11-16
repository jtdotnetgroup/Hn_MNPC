using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class ICPRBillViewDal : BaseRepository<ICPRBillView>
    {
        public static ICPRBillViewDal Instance
        {
            get { return SingletonProvider<ICPRBillViewDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(ICPRBillModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
