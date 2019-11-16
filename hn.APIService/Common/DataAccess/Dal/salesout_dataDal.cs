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
    public class salesout_dataDal : BaseRepository<salesout_dataModel>
    {
        public static salesout_dataDal Instance
        {
            get { return SingletonProvider<salesout_dataDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(salesout_dataModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
