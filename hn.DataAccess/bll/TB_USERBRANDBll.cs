using hn.Common.Provider;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class TB_USERBRANDBll
    {
        public static TB_USERBRANDBll Instance
        {
            get { return SingletonProvider<TB_USERBRANDBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_USERBRANDDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        /// <summary>
        /// 新增品牌厂家
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TB_USERBRANDModel entity)
        {
            TB_USERBRANDDal.Instance.Insert(entity);
        }
    }
}
