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
    public class V_ICPRICEPOLICYDAL : BaseRepository<V_ICPRICEPOLICYMODEL>
    {
        public static V_ICPRICEPOLICYDAL Instance
        {
            get { return SingletonProvider<V_ICPRICEPOLICYDAL>.Instance; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="flag">標志：点击树节点的类型 0品牌 1厂家账号</param>
        /// <param name="FBrandID"></param>
        /// <param name="keywords"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="brandName"></param>
        /// <param name="clientName"></param>
        /// <param name="checkState">审核状态 0未审核 1已审核 无全部</param>
        /// <returns></returns>
        public string GetJson(int pageindex, int pagesize, string flag,string FBrandID = null, 
            string keywords = null, string sort = "FBILLDATE", string order = "asc", string brandName = "",
            string clientName = "", string checkState = "", string startDate= "", string endDate= "",string policytype = "")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBEGDATE - '{0}' <= 0", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FENDDATE - '{0}' >= 0", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!FBrandID.IsNullOrEmpty())
            {
                if (flag=="0")
                {
                    query.AppendFormat(" and FBrandID = '{0}' ", FBrandID);
                }
                else
                {
                    query.AppendFormat(" and FCLIENTID = '{0}' ", FBrandID);
                }
            }
            if (!brandName.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBRANDNAME like '%{0}%' ", brandName);
            }
            if (!clientName.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCLIENTACCOUNT like '%{0}%' ", clientName);
            }
            if (!policytype.IsNullOrEmpty())
            {
                query.AppendFormat(" and FPOLICYTYPE = '{0}'  ", policytype);
            }

            short state = -1;
            if (!checkState.IsNullOrEmpty()&&short.TryParse(checkState,out state))
            {
                if (state>=0 && state <=1)
                {
                    query.AppendFormat(" and FCHECKSTATUS = {0} ", state);
                }
            }
            if (!keywords.IsNullOrEmpty())
            {
                query.Append(" and ( ");

                query.AppendFormat(" FBILLNO like '%{0}%' ", keywords);
                query.AppendFormat(" OR FNAME like '%{0}%' ", keywords);

                query.Append(" )");
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }

    }
}
