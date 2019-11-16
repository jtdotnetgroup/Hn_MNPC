using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class SRCBll
    {
        public static SRCBll Instance
        {
            get { return SingletonProvider<SRCBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return SRCDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


    }
}
