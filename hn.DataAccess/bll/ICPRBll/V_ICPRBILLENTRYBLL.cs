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
    public class V_ICPRBILLENTRYBLL
    {
        public static V_ICPRBILLENTRYBLL Instance
        {
            get { return SingletonProvider<V_ICPRBILLENTRYBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15, string ICPRBILLID = null, string sort = "FID", string order = "asc")
        {
            return V_ICPRBILLENTRYDAL.Instance.GetJson(page, pageSize, ICPRBILLID, sort, order);
        }

        public IEnumerable<V_ICPRBILLENTRYMODEL> GetByICPRBILL(string ICPRBILLID)
        {
            return V_ICPRBILLENTRYDAL.Instance.GetWhere(new
            {
                FID = ICPRBILLID,
            });
        }
    }
}