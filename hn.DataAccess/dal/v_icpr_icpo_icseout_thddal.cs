using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.SqlServer;
using hn.Common.Provider;
using hn.Core.Model;
using hn.DataAccess.Model;
using hn.Core;

namespace hn.DataAccess.Dal
{
    public class v_icpr_icpo_icseout_thdDal : BaseRepository<v_icpr_icpo_icseout_thdModel>
    {
        public static v_icpr_icpo_icseout_thdDal Instance
        {
            get { return SingletonProvider<v_icpr_icpo_icseout_thdDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(v_icpr_icpo_icseout_thdModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

    }
}
