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
    public class V_ICSEOUTBILLDAL : BaseRepository<V_ICSEOUTBILLMODEL>
    {
        public static V_ICSEOUTBILLDAL Instance
        {
            get { return SingletonProvider<V_ICSEOUTBILLDAL>.Instance; }
        }

        public string GetJson(
            int page = 1, 
            int rows = 15, 
            string startDate = null,
            string endDate = null,
            string FOrgID = null,
            int status = 0,
            string FPREMISEID = null,
            string FCLASSAREA2NAME = null,
            string FCARNUMBER = null,
            bool chkclose = false,
            string sort = "FBILLDATE",
            string order = "desc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDELIVERDATE >= ({0}) ", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDELIVERDATE >= ({0}) ", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!FOrgID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FORGID = '{0}' ", FOrgID);
            }


            if (!FPREMISEID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FPREMISEID = '{0}' ", FPREMISEID);
            }


            if (!FCLASSAREA2NAME.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCLASSAREA2NAME LIKE '%{0}%' ", FCLASSAREA2NAME);
            }


            if (!FCARNUMBER.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCARNUMBER LIKE '%{0}%' ", FCARNUMBER);
            }

            if (status > 0)
            {
                query.AppendFormat(" and FSTATUS = {0} ", status);
            }

            if (!chkclose)
            {
                query.AppendLine(" and FStatus <> 5 ");
            }


            if (!SysVisitor.Instance.IsAdmin)
            {
                //品牌/厂家进行数据权限控制
                query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", SysVisitor.Instance.UserId);
            }

            return JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }

    }
}
