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
    public class OrderDetailDal : BaseRepository<OrderDetailModel>
    {
        public static OrderDetailDal Instance
        {
            get { return SingletonProvider<OrderDetailDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(OrderDetailModel)), pageindex, pagesize, filterJson,
                                         sort, order);
        }



        //public override DataTable GetPageWithSp(ProcCustomPage pcp, out int recordCount)
        //{
        //    string platformid = DataAuthorizedDal.Instance.GetPlatFormIds(SysVisitor.enmDataAuthorized.CONTACT);
        //    if (platformid != "")
        //    {
        //        if (pcp.SQL_WHERE_IN == "")
        //        {
        //            pcp.SQL_WHERE_IN = "(PLAT_FORM_ID IN (" + platformid + "))";
        //        }
        //        else
        //        {
        //            pcp.SQL_WHERE_IN = pcp.SQL_WHERE_IN + " AND (PLAT_FORM_ID IN (" + platformid + "))";
        //        }

        //    }

        //    DataTable table = base.GetPageWithSp(pcp, out recordCount);
        //    return table;
        //}
    }
}
