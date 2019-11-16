using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Core.Model;
using hn.Common.Data;
using hn.Common.Provider;

namespace hn.Core.Dal
{
    public class SysDicsDal : BaseRepository<SysDics>
    {
        public static SysDicsDal Instance
        {
            get { return SingletonProvider<SysDicsDal>.Instance; }
        }
        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(SysDics)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public IEnumerable<SysDics> GetListBy(string FClassIdent)
        {
            return GetWhere(new { FClassIdent = FClassIdent });
        }


        public IEnumerable<SysDics> GetListByCategoryCode(string FClassIdent)
        {
            return GetWhere(new { FClassIdent = FClassIdent });
        }


    }

}
