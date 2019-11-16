using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.Filter;
using hn.Common.Provider;
using hn.Core.Dal;
using hn.Core.Model;
using System.Data.SqlClient;
using hn.Common.Data.SqlServer;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Bll
{
    public class V_ICPRBILLBLL
    {
        public static V_ICPRBILLBLL Instance
        {
            get { return SingletonProvider<V_ICPRBILLBLL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return V_ICPRBILLDAL.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetEasyUIJson(
            int page = 1,
            int pageSize = 15,
            string startDate = null,
            string endDate = null,
            string FOrgID = null,
            int FStatus = 0,
            string brandid = null,
            string FCLASSAREA2NAME = null,
            string FTYPEID = null,
            bool chkclose = false,
            bool approved = false,
            string sort = "FBILLDATE",
            string order = "DESC")
        {
            return V_ICPRBILLDAL.Instance.GetEasyUIJson(
                page, pageSize, startDate, endDate, FOrgID, FStatus,
                brandid, FCLASSAREA2NAME, FTYPEID, chkclose, approved, sort, order);
        }
    }
}