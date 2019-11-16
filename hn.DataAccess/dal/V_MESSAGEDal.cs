using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{

    [Description("消息发送")]
    [TableName("V_MESSAGE")]
    public class V_MESSAGEDal : BaseRepository<V_MESSAGEMODEL>
    {
        public static V_MESSAGEDal Instance
        {
            get { return SingletonProvider<V_MESSAGEDal>.Instance; }
        }

        public IEnumerable<V_MESSAGEMODEL> GetChildren(string parentid = "0")
        {
            return GetAll().Where(d => d.FID == parentid);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                 string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(V_MESSAGEMODEL)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15, string startDate = null, string endDate = null, string FRECEIVERID = null)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDATE >= ({0}) ", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDATE <= ({0}) ", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!FRECEIVERID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FRECEIVERID = '{0}'", FRECEIVERID);
            }

            return JsonDataForEasyUIdataGrid(page, pageSize, null, "FDATE", "DESC");
        }

    }
}
