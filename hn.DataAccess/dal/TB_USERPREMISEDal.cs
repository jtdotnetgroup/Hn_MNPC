using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class TB_USERPREMISEDal : BaseRepository<TB_USERPREMISEModel>
    {
        public static TB_USERPREMISEDal Instance
        {
            get { return SingletonProvider<TB_USERPREMISEDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string userid, string sort = "FID",
                              string order = "asc")
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(userid))
            {
                where +=string.Format(" and FUSERID='{0}'",userid);
            }
            return base.JsonDataForEasyUIdataGrid(where, TableConvention.Resolve(typeof(TB_USERPREMISEModel)), pageindex, pagesize, "",
                                                  sort, order);
        }
    }
}
