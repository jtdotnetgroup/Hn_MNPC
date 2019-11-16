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
    public class V_ICPRICEPOLICYENTRYDAL : BaseRepository<V_ICPRICEPOLICYENTRYMODEL>
    {
        public static V_ICPRICEPOLICYENTRYDAL Instance
        {
            get { return SingletonProvider<V_ICPRICEPOLICYENTRYDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string policyID = null, string startDate = null, string endDate = null, string productInfo = null,string itemid=null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            {
                query.AppendFormat(" and FPOLICYID = '{0}' ", policyID);
            }

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBEGDATE >= {0} ", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FENDDATE <= {0} ", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!itemid.IsNullOrEmpty())
            {
                query.AppendFormat(" and FITEMID = '{0}' ", itemid);
            }
            

            if (!productInfo.IsNullOrEmpty())
            {
                query.Append(" and (");

                query.AppendFormat(" FPRODUCTTYPE like '%{0}%' ", productInfo);
                query.AppendFormat(" OR FPRODUCTCODE like '%{0}%' ", productInfo);
                query.AppendFormat(" OR FPRODUCTNAME like '%{0}%' ", productInfo);
                query.AppendFormat(" OR FMODEL like '%{0}%' ", productInfo);
                query.AppendFormat(" OR FUNIT like '%{0}%' ", productInfo);

                query.Append(" )");
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }

        /// <summary>
        /// 获取指定销区下商品价格政策明细
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IEnumerable<V_ICPRICEPOLICYENTRYMODEL> GetByProductWithFORGID(string FORGID, string PRODUCTIDLIST)
        {

            string where = string.Format(" FPOLICYID IN ( SELECT FID FROM ICPRICEPOLICY WHERE ICPRICEPOLICY.FORGID = :FORGID and FSTATUS = :FSTATUS ) AND FITEMID in ({0}) ", string.Join(",", PRODUCTIDLIST.SplitWithoutSpace().Select(p => '\'' + p + '\'')));

            object value = new
            {
                FORGID = FORGID,
                FSTATUS = Constant.ICPricePolicyStatus.审核通过.ToInt(),
            };

            return Instance.GetWhere(where, value);
        }

    }
}
