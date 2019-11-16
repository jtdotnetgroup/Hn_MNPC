using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class V_CLIENTACCOUNTBll
    {
        public static V_CLIENTACCOUNTBll Instance
        {
            get { return SingletonProvider<V_CLIENTACCOUNTBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return V_CLIENTACCOUNTDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


        public string GetJson(string where, int pageindex, int pagesize, string filterJson, string sort = "FSORT", string order = "asc")
        {
            return V_CLIENTACCOUNTDal.Instance.GetJson(where, pageindex, pagesize, filterJson, string.IsNullOrEmpty(sort) ? "FSORT" : sort, order);
        }


    }
}
