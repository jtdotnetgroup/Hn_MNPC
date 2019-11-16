using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class TB_USERBRANDDal : BaseRepository<TB_USERBRANDModel>
    {
        public static TB_USERBRANDDal Instance
        {
            get { return SingletonProvider<TB_USERBRANDDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string userid, string sort = "FID",
                              string order = "asc")
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(userid))
            {
                where += string.Format(" and FUSERID='{0}'", userid);
            }
            return base.JsonDataForEasyUIdataGrid(where, TableConvention.Resolve(typeof(TB_USERBRANDModel)), pageindex, pagesize, "",
                                                  sort, order);
        }
    }
}
