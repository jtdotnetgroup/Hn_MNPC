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
    public class trans_finishDal : BaseRepository<trans_finishModel>
    {
        public static trans_finishDal Instance
        {
            get { return SingletonProvider<trans_finishDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(trans_finishModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
