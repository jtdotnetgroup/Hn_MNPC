using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class TB_TreeUserDal: BaseRepository<TB_TreeUserModel>
    {
       
            public static TB_TreeUserDal Instance
            {
                get { return SingletonProvider<TB_TreeUserDal>.Instance; }
            }


            public IEnumerable<TB_TreeUserModel> GetChildren(string parentid = "0")
            {
                return GetAll().Where(d => d.FID == parentid);
            }


            public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                                string order = "asc")
            {
                return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TB_TreeUserModel)), pageindex, pagesize, filterJson,
                                                      sort, order);
            }
        
    }
}
