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
    public class SYS_DICSDAL : BaseRepository<SYS_DICSMODEL>
    {
        public static SYS_DICSDAL Instance
        {
            get { return SingletonProvider<SYS_DICSDAL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(SYS_DICSMODEL)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        ///// <summary>
        ///// 删除主表和子表的数据
        ///// </summary>
        ///// <returns></returns>
        //public int DeleteWithSub(string ID)
        //{
        //    List<string> list = new List<string>();

        //    object value1 = new
        //    {
        //        FID = ID,
        //    };

        //    object value2 = new
        //    {
        //        FCLASSID = ID,
        //    };

        //    list.Add(Instance.GetDeleteCommandText(ID));//删除主表
        //    list.Add(SYS_SUBDICSDAL.Instance.GetDeleteWhereCommandText(value2));//删除子表

        //    return DbUtils.DeleteWithTransaction(list, value1,value2);
        //}

    }
}
