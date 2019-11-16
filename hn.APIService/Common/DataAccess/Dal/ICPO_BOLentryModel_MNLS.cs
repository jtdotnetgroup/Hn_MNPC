using hn.APIService.Data.Provider;
using hn.APIService.DataAccess.Model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace hn.APIService.DataAccess.Dal
{
    public class ICPO_BOLentryModel_MNLSDal : BaseRepository<ICPO_BOLentryModel_MNLS>
    {
        public static ICPO_BOLentryModel_MNLSDal Instance
        {
            get { return SingletonProvider<ICPO_BOLentryModel_MNLSDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(ICPO_BOLentryModel_MNLS)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }
    }
}
