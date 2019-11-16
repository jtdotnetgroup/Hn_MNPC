using hn.APIService.Data.Provider;
using hn.APIService.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace hn.APIService.DataAccess.Dal
{
    public class TMP_STOCKBillDal : BaseRepository<TMP_STOCKBillModel>
    {
        public static TMP_STOCKBillDal Instance
        {
            get { return SingletonProvider<TMP_STOCKBillDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TMP_STOCKBillModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
