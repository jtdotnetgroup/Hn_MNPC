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
    public class V_ICPRBILLENTRYDAL : BaseRepository<V_ICPRBILLENTRYMODEL>
    {
        public static V_ICPRBILLENTRYDAL Instance
        {
            get { return SingletonProvider<V_ICPRBILLENTRYDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string ICPRBILLID = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(ICPRBILLID))
            {
                query.AppendFormat(" and FPLANID = '{0}' ", PublicMethod.GetString(ICPRBILLID));
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), "FENTRYID", order);
        }

        public string GetJsonAll(
            int pageindex,
            int pagesize,
            string brandid = null,
            string startDate = null,
            string endDate = null,
            string classarea2name = null,
            string productname = null,
            string billno = null,
            string status = null,
            string sort = "FENTRYID",
            string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            query.AppendLine("and FSTATUS in (" + (string.IsNullOrEmpty(status) ? "3" : status) + ")");


            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and to_char(FNEEDDATE,'yyyy-MM-dd')  >= '{0}' ", startDate);
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and to_char(FNEEDDATE,'yyyy-MM-dd') <= '{0}' ", endDate);
            }

            if (!string.IsNullOrEmpty(brandid))
            {
                query.AppendFormat(" and FBRANDID = '{0}' ", PublicMethod.GetString(brandid));
            }


            if (!string.IsNullOrEmpty(classarea2name))
            {
                query.AppendFormat(" and FCLASSAREA2NAME LIKE  '%{0}%' ", PublicMethod.GetString(classarea2name));
            }

            if (!string.IsNullOrEmpty(classarea2name))
            {
                query.AppendFormat(" and FPRODUCTNAME LIKE  '%{0}%' ", PublicMethod.GetString(productname));
            }

            if (!string.IsNullOrEmpty(billno))
            {
                query.AppendFormat(" and ICPRBILLNO = '{0}' ", PublicMethod.GetString(billno));
            }


            return JsonDataForEasyUIdataGrid(pageindex, 9999, query.ToString(), sort, order);
        }

        /// <summary>
        /// 根据条件查询采购订单弹出选择请购计划明细数据
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="ICPRBILLID"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetJsonToICPO(int pageindex, int pagesize, string ICPRBILLID = null, string sort = "FENTRYID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            query.AppendFormat(" and FPLANID = '{0}' ", PublicMethod.GetString(ICPRBILLID));

            query.AppendLine("and FICPOBILLNO IS NULL");

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
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
        public string GetJsonToICSEOUT(int pageindex, int pagesize,
             string brandid = null,
             string startDate = null,
             string endDate = null,
             string classarea2name = null,
             string productname = null,
             string premisebrandname = null,
             string icprbillno = null,
             string sort = "FENTRYID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            query.AppendFormat(" and FBRANDID = '{0}' ", PublicMethod.GetString(brandid));

            query.AppendLine("and FSTATUS=7 AND FLEFTAMOUNT>0");


            if (!string.IsNullOrEmpty(classarea2name))
            {
                query.AppendFormat(" and FCLASSAREA2NAME LIKE  '%{0}%' ", PublicMethod.GetString(classarea2name));
            }

            if (!string.IsNullOrEmpty(premisebrandname))
            {
                query.AppendFormat(" and FPREMISEBRANDNAME LIKE  '%{0}%' ", PublicMethod.GetString(premisebrandname));
            }

            if (!string.IsNullOrEmpty(icprbillno))
            {
                query.AppendFormat(" and ICPRBILLNO LIKE  '%{0}%' ", PublicMethod.GetString(icprbillno));
            }

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and to_char(FNEEDDATE,'yyyy-MM-dd')  >= '{0}' ", startDate);
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and to_char(FNEEDDATE,'yyyy-MM-dd') <= '{0}' ", endDate);
            }

            if (!string.IsNullOrEmpty(productname))
            {
                query.AppendFormat(" and FPRODUCTNAME LIKE  '%{0}%' ", PublicMethod.GetString(productname));
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort.Replace("FNEEDDATESTR", "FNEEDDATE"), order);
        }
    }
}
