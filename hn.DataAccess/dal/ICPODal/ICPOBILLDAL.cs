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
using hn.DataAccess.Bll;

namespace hn.DataAccess.Dal
{
    public class ICPOBILLDAL : BaseRepository<ICPOBILLMODEL>
    {
        public static ICPOBILLDAL Instance
        {
            get { return SingletonProvider<ICPOBILLDAL>.Instance; }
        }

        public string GetJson(int page = 1, int rows = 15, string FOrgID = null, string FBrandID = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            //if(!stratDate.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FBillDate - {0} >= 0 ", stratDate.ToDateTime().GetOracleToDateShortString());
            //}

            //if (!endDate.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FBillDate - {0} <= 0 ", endDate.ToDateTime().GetOracleToDateShortString());
            //}

            //if (!FOrgID.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FORGID = '{0}' ", FOrgID);
            //}

            //if (!FBrandID.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FBrandID = '{0}' ", FOrgID);
            //}

            //if (!keywords.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FNAME like '%{0}%' ", keywords);
            //}

            return JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns></returns>
        public int GetStatus(string FID)
        {
            return Instance.GetExecuteScalarWhere("FSTATUS", new { FID = FID }).ToInt();
        }
    }
}
