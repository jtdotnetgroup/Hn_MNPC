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
    public class ICPRICEPOLICYENTRYDAL : BaseRepository<ICPRICEPOLICYENTRYMODEL>
    {
        public static ICPRICEPOLICYENTRYDAL Instance
        {
            get { return SingletonProvider<ICPRICEPOLICYENTRYDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize,string policyID = null, string startDate = null, string endDate = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            {
                query.AppendFormat(" and FPOLICYID = '{0}' ", policyID);
            }

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBEGDATE - '{0}' <= 0", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FENDDATE - '{0}' >= 0", endDate.ToDateTime().GetOracleToDateShortString());
            }

            //if (!keywords.IsNullOrEmpty())
            //{
            //    query.Append(" and (");

            //    query.AppendFormat(" and FBillDate - '{0}' >= 0", keywords);

            //    query.Append(" )");
            //}

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }


        /// <summary>
        /// 获取指定销区下商品价格政策明细
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IEnumerable<ICPRICEPOLICYENTRYMODEL> GetByProductWithFORGID(string FORGID,string PRODUCTIDLIST)
        {

            string where = string.Format(" FPOLICYID IN ( SELECT FID FROM ICPRICEPOLICY WHERE ICPRICEPOLICY.FORGID = :FORGID ) AND FITEMID in ({0}) ", string.Join(",", PRODUCTIDLIST.SplitWithoutSpace().Select(p => '\'' + p + '\'')));

            object value = new {
                FORGID = FORGID,
            };

            return Instance.GetWhere(where, value);
        }

        public int CountIsUseByPOLICYID(string POLICYID)
        {
            string sql = string.Format(@"SELECT (
		                        SELECT COUNT(*)
		                        FROM ICPRPOLICY
		                        WHERE ICPRPOLICY.FPOLICYID IN (SELECT FID
			                        FROM {1}
			                        WHERE {1}.FPOLICYID = '{0}' )
		                        ) + (
		                        SELECT COUNT(*)
		                        FROM ICPOPOLICY
		                        WHERE ICPOPOLICY.FPOLICYID IN (SELECT FID
			                        FROM ICPricePolicyEntry
			                        WHERE ICPricePolicyEntry.FPOLICYID = '{0}' )
		                        )
                        FROM dual", POLICYID,TableConvention.Resolve(typeof(ICPRICEPOLICYENTRYMODEL)));

            return DbUtils.CountBySQL(sql);
        }
    }
}
