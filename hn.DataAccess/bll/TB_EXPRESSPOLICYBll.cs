using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class TB_EXPRESSPOLICYBll
    {
        public static TB_EXPRESSPOLICYBll Instance
        {
            get { return SingletonProvider<TB_EXPRESSPOLICYBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_EXPRESSPOLICYDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


    }
}
