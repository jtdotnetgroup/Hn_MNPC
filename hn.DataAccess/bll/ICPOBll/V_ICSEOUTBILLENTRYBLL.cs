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
    public class V_ICSEOUTBILLENTRYBLL
    {
        public static V_ICSEOUTBILLENTRYBLL Instance
        {
            get { return SingletonProvider<V_ICSEOUTBILLENTRYBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15,string FICSEOUTID = null, string sort = "FID", string order = "asc")
        {
            return V_ICSEOUTBILLENTRYDAL.Instance.GetJson(page, pageSize, FICSEOUTID, sort, order);
        }
    }
}