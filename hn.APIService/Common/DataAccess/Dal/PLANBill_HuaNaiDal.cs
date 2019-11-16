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
    public class PLANBill_HuaNaiDal : BaseRepository<PLANBill_HuaNaiModel>
    {
        public static PLANBill_HuaNaiDal Instance
        {
            get { return SingletonProvider<PLANBill_HuaNaiDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(PLANBill_HuaNaiModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
