using hn.Common.Provider;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class TB_ATTACHMENTBll
    {
        public static TB_ATTACHMENTBll Instance
        {
            get { return SingletonProvider<TB_ATTACHMENTBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_ATTACHMENTDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

    }
}
