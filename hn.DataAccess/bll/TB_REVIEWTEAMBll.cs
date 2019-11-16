using hn.Common.Provider;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class TB_REVIEWTEAMBll
    {
        public static TB_REVIEWTEAMBll Instance
        {
            get { return SingletonProvider<TB_REVIEWTEAMBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_REVIEWTEAMDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public int Update(TB_REVIEWTEAMModel model)
        {
            return TB_REVIEWTEAMDal.Instance.Update(model);
        }

        public TB_REVIEWTEAMModel GetByID(string id)
        {
            return TB_REVIEWTEAMDal.Instance.Get(id);
        }

        public int DeleteByID(string id)
        {
            return TB_REVIEWTEAMDal.Instance.Delete(id);
        }
    }
}
