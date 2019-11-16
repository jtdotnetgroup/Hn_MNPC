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
    public class V_ICPOPOLICYBLL
    {
        public static V_ICPOPOLICYBLL Instance
        {
            get { return SingletonProvider<V_ICPOPOLICYBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int rows = 15, string FOrgID = null, string FBrandID = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            return V_ICPOPOLICYDAL.Instance.GetJson(page, rows, FOrgID, FBrandID, keywords, sort, order);
        }

        public IEnumerable<V_ICPOPOLICYMODEL> GetByICPOBILLID(string ICPOBILLID)
        {
            return V_ICPOPOLICYDAL.Instance.GetWhere(new { FID = ICPOBILLID });
        }
    }
}