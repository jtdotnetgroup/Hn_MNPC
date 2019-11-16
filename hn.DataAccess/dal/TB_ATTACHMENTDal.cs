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
    public class TB_ATTACHMENTDal : BaseRepository<TB_ATTACHMENTModel>
    {
        public static TB_ATTACHMENTDal Instance
        {
            get { return SingletonProvider<TB_ATTACHMENTDal>.Instance; }
        }


        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
        //         string order = "asc")
        //{
        //    return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TB_ATTACHMENTModel)), pageindex, pagesize, filterJson,
        //                                          sort, order);
        //}

        public string GetJson(int pageindex, int pagesize, string billid = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(billid))
            {
                query.AppendFormat(" and FBILLID = '{0}' ", PublicMethod.GetString(billid));
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), "FADD_TIME", order);
        }
    }
}
