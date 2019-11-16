using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using hn.APIService.Data.Provider;
using hn.APIService.DataAccess.Model;
using hn.APIService.DataAccess.Dal;

namespace zt.DataAccess.Bll
{
    public class ICSEOUTBILLEntryBll
    {

        public static ICSEOUTBILLEntryBll Instance
        {
            get { return SingletonProvider<ICSEOUTBILLEntryBll>.Instance; }
        }

        public int Update(ICSEOUTBILLEntryModel model)
        {
            return ICSEOUTBILLEntryDal.Instance.Update(model);
        }

        public int Delete(int FID)
        {
            return ICSEOUTBILLEntryDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return ICSEOUTBILLEntryDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }
}
