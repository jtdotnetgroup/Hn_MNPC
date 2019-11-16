using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class TB_CLIENTACCOUNTBll
    {
        public static TB_CLIENTACCOUNTBll Instance
        {
            get { return SingletonProvider<TB_CLIENTACCOUNTBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_CLIENTACCOUNTDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


    }
}
