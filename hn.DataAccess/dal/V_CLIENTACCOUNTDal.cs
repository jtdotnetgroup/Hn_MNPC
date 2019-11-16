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
   public class V_CLIENTACCOUNTDal : BaseRepository<V_CLIENTACCOUNTModel>
    {
        public static V_CLIENTACCOUNTDal Instance
        {
            get { return SingletonProvider<V_CLIENTACCOUNTDal>.Instance; }
        }


        public IEnumerable<V_CLIENTACCOUNTModel> GetChildren(string parentid = "0")
        {
            return GetAll().Where(d => d.FID == parentid);
        }


        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(V_CLIENTACCOUNTModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }


        public string GetJson(string where,int pageindex, int pagesize, string filterJson, string sort = "FID",
                      string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(where, TableConvention.Resolve(typeof(V_CLIENTACCOUNTModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }


        public string GetEasyUIJson(int page = 1, int pageSize = 15,  string sort = "FID", string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(page, pageSize, null, sort, order);
        }
    }
}
