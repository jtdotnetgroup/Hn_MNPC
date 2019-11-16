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
    public class PlatFormDal : BaseRepository<PlatFormModel>
    {
        private string _platformid = "";
        public static PlatFormDal Instance
        {
            get { return SingletonProvider<PlatFormDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc", string platformid = "")
        {
            _platformid = platformid;
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(PlatFormModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }


        public override DataTable GetPageWithSp(ProcCustomPage pcp, out int recordCount)
        {            
            if (_platformid != "")
            {
                if (pcp.SQL_WHERE_IN == "")
                {
                    pcp.SQL_WHERE_IN = "(ID IN (" + _platformid + "))";
                }
                else
                {
                    pcp.SQL_WHERE_IN = pcp.SQL_WHERE_IN + " AND (ID IN (" + _platformid + "))";
                }

            }

            DataTable table = base.GetPageWithSp(pcp, out recordCount);
            return table;
        }
    }
}
