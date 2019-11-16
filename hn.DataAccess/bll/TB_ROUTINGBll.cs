using hn.Common.Provider;
using hn.DataAccess.dal;
using hn.DataAccess.model;

namespace hn.DataAccess.bll
{
    public class TB_ROUTINGBll
    {
        public static TB_ROUTINGBll Instance
        {
            get { return SingletonProvider<TB_ROUTINGBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_ROUTINGDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public int Update(TB_ROUTINGModel model)
        {
            return TB_ROUTINGDal.Instance.Update(model);
        }

        public TB_ROUTINGModel GetByID(string id)
        {
            return TB_ROUTINGDal.Instance.Get(id);
        }

        public int DeleteByID(string id)
        {
            return TB_ROUTINGDal.Instance.Delete(id);
        }
    }
}
