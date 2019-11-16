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
    public class ICPOBILLENTRYDAL : BaseRepository<ICPOBILLENTRYMODEL>
    {
        public static ICPOBILLENTRYDAL Instance
        {
            get { return SingletonProvider<ICPOBILLENTRYDAL>.Instance; }
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


        public int GetConfirmStatus(string icprid)
        {
            string sql = string.Format("select count(1) from ICPOBILLENTRY where FSTATUS<>7 and FID='{0}'", icprid);

            return DbUtils.Query(sql).Rows[0][0].ToInt();
        }

        public int GetCloseStatus(string icprid)
        {
            string sql = string.Format("select count(1) from ICPOBILLENTRY where FSTATUS<>5 and FID='{0}'", icprid);

            return DbUtils.Query(sql).Rows[0][0].ToInt();
        }

    }
}
