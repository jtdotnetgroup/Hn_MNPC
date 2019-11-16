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
using hn.DataAccess.model;

namespace hn.DataAccess.Bll
{
    public class V_BIINTERFACEBll
    {

        public static V_BIINTERFACEBll Instance
        {
            get { return SingletonProvider<V_BIINTERFACEBll>.Instance; }
        }

        public int Update(V_BIINTERFACEMODEL model)
        {
            return V_BIINTERFACEDal.Instance.Update(model);
        }

        public int Delete(string FID)
        {
            return V_BIINTERFACEDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return V_BIINTERFACEDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            return V_BIINTERFACEDal.Instance.GetEasyUIJson(pageindex, pagesize, sort, order);
        }
        
    }
}
