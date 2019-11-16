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
    public class V_ICPRICEPOLICYENTRYBLL
    {
        public static V_ICPRICEPOLICYENTRYBLL Instance
        {
            get { return SingletonProvider<V_ICPRICEPOLICYENTRYBLL>.Instance; }
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string policyID = null, string startDate = null, string endDate = null, string productInfo = null,string itemid = null)
        {
            return V_ICPRICEPOLICYENTRYDAL.Instance.GetJson(pageindex, pagesize, policyID, startDate, endDate, productInfo, itemid);
        }

        public IEnumerable<V_ICPRICEPOLICYENTRYMODEL> GetByProductWithFORGID(string FORGID, string PRODUCTIDLIST)
        {
            return V_ICPRICEPOLICYENTRYDAL.Instance.GetByProductWithFORGID(FORGID, PRODUCTIDLIST);
        }
    }

}