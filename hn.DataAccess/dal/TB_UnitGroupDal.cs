using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class TB_UnitGroupDal : BaseRepository<TB_UnitGroupModel>
    {
        public static TB_UnitGroupDal Instance
        {
            get { return SingletonProvider<TB_UnitGroupDal>.Instance; }
        }

        public IEnumerable<TB_UnitGroupModel> GetChildren(string parentid = "0")
        {
            return GetAll();
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                      string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TB_UnitGroupModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

    }
}
