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
    public class ICPRPOLICYDAL : BaseRepository<ICPRPOLICYMODEL>
    {
        public static ICPRPOLICYDAL Instance
        {
            get { return SingletonProvider<ICPRPOLICYDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public IEnumerable<string> GetFPOLICYIDListByUse(string FPOLICYIDList)
        {
            if (FPOLICYIDList.IsNullOrEmpty())
            {
                return new List<string>(0);
            }

            string sql = string.Format("select FPOLICYID from ICPRPOLICY GROUP BY FPOLICYID  HAVING  FPOLICYID in {0}", string.Join(",", FPOLICYIDList.SplitWithoutSpace().Select(f => '\'' + f + '\'')));

            return DbUtils.GetListByFristColumn<string>(sql);
        }
    }
}
