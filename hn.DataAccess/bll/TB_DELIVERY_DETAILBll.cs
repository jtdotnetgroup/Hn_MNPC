using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class TB_DELIVERY_DETAILBll
    {
        public static TB_DELIVERY_DETAILBll Instance
        {
            get { return SingletonProvider<TB_DELIVERY_DETAILBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_DELIVERY_DETAILDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


    }
}
