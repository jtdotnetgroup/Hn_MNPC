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
    public class v_thdBll
    {
        public static v_thdBll Instance
        {
            get { return SingletonProvider<v_thdBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "AUTOID", string order = "asc")
        {
            return v_thdDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }

}