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
using hn.Core;

namespace hn.DataAccess.Dal
{
    public class SYS_PARAMSDAL : BaseRepository<SYS_PARAMSMODEL>
    {
        public static SYS_PARAMSDAL Instance
        {
            get { return SingletonProvider<SYS_PARAMSDAL>.Instance; }
        }

        public int DeleteAll()
        {
            return DbUtils.ExecuteNonQuery("delete from SYS_PARAMS", null);
        }
        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
        //                      string order = "asc")
        //{
        //    return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(Sys_ParamsModel)), pageindex, pagesize, filterJson,
        //                                          sort, order);
        //}

        public IEnumerable<SYS_PARAMSMODEL> GetListBy(string key)
        {
            return GetWhere(new { FKEY = key });
        }

        public string GetJson(int page, int pageSize, string parentID, string parentCode = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            //if(!parentID.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FClassID = '{0}'",parentID);
            //}

            //if (!parentCode.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FClassID in ( select FID from Sys_Dics where Sys_Dics.FClassCode = '{0}' ) ", parentCode);
            //}

            return JsonDataForEasyUIdataGrid(page, pageSize, query.ToString(), sort, order);
        }

    }
}
