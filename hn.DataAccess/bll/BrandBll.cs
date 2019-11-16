using hn.Common.Provider;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class BrandBll
    {
        public static BrandBll Instance
        {
            get { return SingletonProvider<BrandBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return BrandDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        /// <summary>
        /// 新增品牌厂家
        /// </summary>
        /// <param name="entity"></param>
        public void Add(BrandModel entity)
        {
            BrandDal.Instance.Insert(entity);
        }
    }
}
