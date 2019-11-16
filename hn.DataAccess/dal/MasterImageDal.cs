using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.SqlServer;
using hn.Common.Provider;
using hn.Core.Model;
using hn.DataAccess.Model;
namespace hn.DataAccess.Dal
{
    public class MasterImageDal : BaseRepository<MasterImageModel>
    {
        public static MasterImageDal Instance
        {
            get { return SingletonProvider<MasterImageDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(MasterImageModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public List<MasterImageModel> GetDataByMasterID(string id)
        {
            return MasterImageDal.Instance.GetWhere(new { MASTER_ID = id }).ToList();
        }

    }
}
