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
    public class TP_inventoryDal : BaseRepository<TP_inventoryModel>
    {
        public static TP_inventoryDal Instance
        {
            get { return SingletonProvider<TP_inventoryDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TP_inventoryModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
