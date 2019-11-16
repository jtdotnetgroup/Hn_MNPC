using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class TB_EXPRESSCOMPANYBll
    {
        public static TB_EXPRESSCOMPANYBll Instance
        {
            get { return SingletonProvider<TB_EXPRESSCOMPANYBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_EXPRESSCOMPANYDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


    }
}
