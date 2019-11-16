using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{

    //[Description("组织架构")]
    [TableName("TB_MESSAGE")]
    public class TB_MessageDal : BaseRepository<TB_MESSAGEMODEL>
    {
        public static TB_MessageDal Instance
        {
            get { return SingletonProvider<TB_MessageDal>.Instance; }
        }

        public IEnumerable<TB_MESSAGEMODEL> GetChildren(string parentid = "0")
        {
            return GetAll().Where(d => d.FID == parentid);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                 string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TB_MESSAGEMODEL)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

    }
}
