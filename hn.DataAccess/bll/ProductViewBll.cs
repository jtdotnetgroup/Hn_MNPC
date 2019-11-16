using hn.Common.Provider;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class ProductViewBll
    {
        public static ProductViewBll Instance
        {
            get { return SingletonProvider<ProductViewBll>.Instance; }
        }

      

        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        //{
        //    return ProductViewDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        //}

        public string GetEasyUIJson(int pageindex, int pagesize, string FORGID=null,string FTYPEID=null,string categoryID = null, string FBRANDID = null, string keywords = null,string status = null, string sort = "FID", string order = "asc")
        {
            return ProductViewDal.Instance.GetJson(pageindex, pagesize, FORGID, FTYPEID,categoryID, FBRANDID, keywords, status, sort, order);
        }
    }
}
