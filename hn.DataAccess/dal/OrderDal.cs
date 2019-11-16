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
    [TableName("TB_ORDER")]
    public class OrderDal : BaseRepository<OrderModel>
    {
        public static OrderDal Instance
        {
            get { return SingletonProvider<OrderDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(OrderModel)), pageindex, pagesize, filterJson,
                                         sort, order);
        }



     

        //public override int DeleteByIds(string ids)
        //{
        //    int count = 0;
        //    string[] array = ids.Split(',');
        //    foreach (string id in array)
        //    {
        //        int result = this.UpdateWhatWhere(new { DELETE_FLAG = 1 }, new { FID = id });
        //        if (result > 0)
        //        {
        //            count++;
                    
        //           // DbUtils.DeleteWhere<OrderDetailModel>(new { ORDER_ID = id });
        //        }
        //    }

        //    return count;
        //}

    }
}
