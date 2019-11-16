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
   public class TB_CLIENTACCOUNTDal : BaseRepository<TB_CLIENTACCOUNTModel>
    {
        public static TB_CLIENTACCOUNTDal Instance
        {
            get { return SingletonProvider<TB_CLIENTACCOUNTDal>.Instance; }
        }


        public IEnumerable<TB_CLIENTACCOUNTModel> GetChildren(string parentid = "0")
        {
            return GetAll().Where(d => d.FID == parentid);
        }


        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                      string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TB_CLIENTACCOUNTModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }


        public string GetEasyUIJson(int page = 1, int pageSize = 15,  string sort = "FID", string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(page, pageSize, null, sort, order);
        }
    }
}
