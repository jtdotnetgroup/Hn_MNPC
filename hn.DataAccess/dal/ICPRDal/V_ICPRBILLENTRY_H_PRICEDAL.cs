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
    public class V_ICPRBILLENTRY_H_PRICEDAL : BaseRepository<V_ICPRBILLENTRY_H_PRICEMODEL>
    {
        public static V_ICPRBILLENTRY_H_PRICEDAL Instance
        {
            get { return SingletonProvider<V_ICPRBILLENTRY_H_PRICEDAL>.Instance; }
        }

       
        /// <summary>
        /// 根据条件查询采购订单弹出选择请购计划明细数据[发货通知，配柜用]
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="ICPRBILLID"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetJsonToICSEOUT(int pageindex, int pagesize, string brandid = null,string clientid = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            query.AppendFormat(" and FBRANDID = '{0}' ", PublicMethod.GetString(brandid));
            query.AppendFormat(" and FCLIENTID = '{0}' ", PublicMethod.GetString(clientid));

            query.AppendLine("and FSTATUS=3 AND FLEFTAMOUNT>0");

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), "FENTRYID", order);
        }
    }
}
