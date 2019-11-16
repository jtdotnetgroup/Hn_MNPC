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
    public class ICPRBILLDAL : BaseRepository<ICPRBILLMODEL>
    {
        public static ICPRBILLDAL Instance
        {
            get { return SingletonProvider<ICPRBILLDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(ICPRBILLMODEL)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15, string startDate = null, string endDate = null, string FOrgID = null, int FStatus = 0, bool approved = false, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBILLDATE >= {0} ", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBILLDATE <= {0} ", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!FOrgID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FOrgID = '{0}' ", FOrgID);
            }

            if (FStatus > 0)
            {
                query.AppendFormat(" and FStatus = {0} ", FOrgID);
            }

            if (approved)
            {
                query.AppendFormat(" and FSTATUS = {0} ", Constant.BILL_FSTATUS.审核通过.ToInt());
            }

            return JsonDataForEasyUIdataGrid(page, pageSize, query.ToString(), sort, order);
        }

        /// <summary>
        /// 获取请购计划状态
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public int GetStatus(string FID)
        {
            return DbUtils.GetExecuteScalarWhere<ICPRBILLMODEL, int>("FSTATUS", new { FID = FID });
        }

        //public bool checkBill(string id)
        //{
        //    int count = ICPOBILLDAL.Instance.CountWhere(new { });
        //}
    }
}
