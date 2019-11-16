using hn.Common.Data;
using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class V_PREMISEDal : BaseRepository<V_PREMISEModel>
    {
        public static V_PREMISEDal Instance
        {
            get { return SingletonProvider<V_PREMISEDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson,string keywords = "", string sort = "FID",
                      string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!keywords.IsNullOrEmpty())
            {
                query.Append(" and ( ");

                query.AppendFormat("  FCODE like '%{0}%' ", keywords);
                query.AppendFormat(" OR FNAME like '%{0}%' ", keywords);
                query.AppendFormat(" OR FCLASSAREA2NAME like '%{0}%' ", keywords);

                query.Append(" ) ");
            }

            if (!SysVisitor.Instance.IsAdmin)
            {
                query.AppendFormat("  AND FID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", SysVisitor.Instance.UserId);
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }
     
    }
}
