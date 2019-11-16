using hn.Common.Provider;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class ICPRBillViewBll
    {
        public static ICPRBillViewBll Instance
        {
            get { return SingletonProvider<ICPRBillViewBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return ICPRBillViewDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


        public void Add(ICPRBillView entity)
        {
            ICPRBillViewDal.Instance.Insert(entity);
        }
    }
}
