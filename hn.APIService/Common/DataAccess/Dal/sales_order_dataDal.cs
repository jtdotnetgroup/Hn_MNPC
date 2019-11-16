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
    public class sales_order_dataDal : BaseRepository<sales_order_dataModel>
    {
        public static sales_order_dataDal Instance
        {
            get { return SingletonProvider<sales_order_dataDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(sales_order_dataModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
