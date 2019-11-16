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
    public class SYS_PARAMSBLL
    {
        public static SYS_PARAMSBLL Instance
        {
            get { return SingletonProvider<SYS_PARAMSBLL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return SYS_PARAMSDAL.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public IEnumerable<SYS_PARAMSMODEL> GetAll()
        {
            return SYS_PARAMSDAL.Instance.GetAll();
        }

        public int DeleteAll()
        {
            return SYS_PARAMSDAL.Instance.DeleteAll();
        }

        public string Add(SYS_PARAMSMODEL model)
        {
            return SYS_PARAMSDAL.Instance.Insert(model);
        }

        public IEnumerable<SYS_PARAMSMODEL> GetWhereKey(string key)
        {
            return SYS_PARAMSDAL.Instance.GetListBy(key);
        }


        public int Update(SYS_PARAMSMODEL model)
        {
            return SYS_PARAMSDAL.Instance.Update(model);
        }
    }

}