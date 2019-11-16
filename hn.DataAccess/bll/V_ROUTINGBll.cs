using hn.Common.Provider;
using hn.DataAccess.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class V_ROUTINGBll
    {
        public static V_ROUTINGBll Instance
        {
            get { return SingletonProvider<V_ROUTINGBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return V_ROUTINGDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }
}
