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
    public class V_ICPRBILLDAL : BaseRepository<V_ICPRBILLMODEL>
    {
        public static V_ICPRBILLDAL Instance
        {
            get { return SingletonProvider<V_ICPRBILLDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(V_ICPRBILLMODEL)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public string GetEasyUIJson(
            int page = 1, 
            int pageSize = 15,
            string startDate = null, 
            string endDate = null,
            string FPREMISEID = null,
            int FStatus = 0,
            string brandid = null,
            string FCLASSAREA2NAME = null,
            string FTYPEID = null,
            bool chkclose = false,
            bool approved = false,
            string sort = "FID", 
            string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and to_char(FBILLDATE,'yyyy-MM-dd')  >= '{0}' ", startDate);
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and to_char(FBILLDATE,'yyyy-MM-dd') <= '{0}' ", endDate);
            }

            if (!FPREMISEID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FPREMISEID = '{0}' ", FPREMISEID);
            }

            if (!brandid.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBRANDID = '{0}' ", brandid);
            }

            if (FStatus > 0)
            {
                query.AppendFormat(" and FStatus = {0} ", FStatus);
            }

            if (!chkclose)
            {
                query.AppendLine(" and FStatus <> 5 ");
            }

            if (approved)
            {
                //query.AppendFormat(" and FSTATUS = {0} ", Constant.ICPRBILL_FSTATUS.审核通过.ToInt());
                query.AppendLine(" AND (SELECT COUNT(1) FROM ICPRBILLENTRY WHERE ICPRBILLENTRY.FPLANID = V_ICPRBILL.FID AND ICPRBILLENTRY.FICPOBILLNO IS NULL)>0");
            }

            if (!FCLASSAREA2NAME.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCLASSAREA2NAME like '%{0}%' ", FCLASSAREA2NAME);
            }

            if (!FTYPEID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FTYPEID = '{0}' ", FTYPEID);
            }

            if (!SysVisitor.Instance.IsAdmin)
            {
                query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", SysVisitor.Instance.UserId);
                //品牌/厂家进行数据权限控制
                query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", SysVisitor.Instance.UserId);
            }

            return JsonDataForEasyUIdataGrid(page, pageSize, query.ToString(), sort, order);
        }

    }
}
