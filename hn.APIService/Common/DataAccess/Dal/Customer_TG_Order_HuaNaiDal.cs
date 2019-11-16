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
    public class Customer_TG_Order_HuaNaiDal : BaseRepository<Customer_TG_Order_HuaNaiModel>
    {
        public static Customer_TG_Order_HuaNaiDal Instance
        {
            get { return SingletonProvider<Customer_TG_Order_HuaNaiDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(Customer_TG_Order_HuaNaiModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
