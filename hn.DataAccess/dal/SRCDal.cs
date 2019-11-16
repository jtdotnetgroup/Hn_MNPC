using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
   public class SRCDal : BaseRepository<SRCModel>
    {
        public static SRCDal Instance
        {
            get { return SingletonProvider<SRCDal>.Instance; }
        }


        public IEnumerable<SRCModel> GetChildren(string parentid = "0")
        {
            return GetAll().Where(d => d.FID == parentid);
        }


        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                      string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(SRCModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }


        public string GetEasyUIJson(int page = 1, int pageSize = 15,  string sort = "FID", string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(page, pageSize, null, sort, order);
        }
    }
}
