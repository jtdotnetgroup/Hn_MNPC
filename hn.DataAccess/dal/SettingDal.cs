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
    public class SettingDal : BaseRepository<SettingModel>
    {
        public static SettingDal Instance
        {
            get { return SingletonProvider<SettingDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(SettingModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public int DeleteAll()
        {
            return DbUtils.ExecuteNonQuery("delete from TB_SETTING", null);
        }

        public void UpdateMaxID(decimal maxId)
        {
            DbUtils.ExecuteNonQuery("UPDATE TB_SETTING SET MAX_SCHEDULE_ID=" + maxId);
        }
    }
}
