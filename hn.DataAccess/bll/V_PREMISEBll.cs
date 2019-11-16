using hn.Common.Provider;
using hn.DataAccess.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class V_PREMISEBll
    {
        public static V_PREMISEBll Instance
        {
            get { return SingletonProvider<V_PREMISEBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson,string keywords = "", string sort = "FID", string order = "asc")
        {
            return V_PREMISEDal.Instance.GetJson(pageindex, pagesize, filterJson, keywords, sort, order);
        }
    }
}
