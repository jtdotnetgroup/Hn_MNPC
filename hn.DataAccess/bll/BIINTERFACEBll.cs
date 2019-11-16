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
    public class BIINTERFACEBll
    {

        public static BIINTERFACEBll Instance
        {
            get { return SingletonProvider<BIINTERFACEBll>.Instance; }
        }

        public int Update(BIINTERFACEMODEL model)
        {
            return BIINTERFACEDal.Instance.Update(model);
        }

        public int Delete(string FID)
        {
            return BIINTERFACEDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return BIINTERFACEDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            return BIINTERFACEDal.Instance.GetEasyUIJson(pageindex, pagesize, sort, order);
        }
        
    }
}
