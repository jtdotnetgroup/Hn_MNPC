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
using hn.DataAccess.Bll;

namespace hn.DataAccess.Dal
{
    public class V_ICSEOUTBILLENTRYDAL : BaseRepository<V_ICSEOUTBILLENTRYMODEL>
    {
        public static V_ICSEOUTBILLENTRYDAL Instance
        {
            get { return SingletonProvider<V_ICSEOUTBILLENTRYDAL>.Instance; }
        }

        public string GetJson(int page = 1, int rows = 15, string FICSEOUTID = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            query.AppendFormat(" and FICSEOUTID = '{0}'  ", FICSEOUTID);

           

            return JsonDataForEasyUIdataGridFoorter(page, rows, query.ToString(), sort, order);
        }

        protected override object GetEasyuiDatagridFooter(IEnumerable<V_ICSEOUTBILLENTRYMODEL> list)
        {
            var footer = new List<object>();

            decimal commitqty = 0;
            decimal hnamount = 0;
            decimal weight = 0;
            decimal volume = 0;
            foreach (V_ICSEOUTBILLENTRYMODEL model in list)
            {
                commitqty += model.FCOMMITQTY;
                hnamount += model.FHNAMOUNT;
                weight += model.FWEIGHT;
                volume += model.FVOLUME;
            }

            footer.Add(new { FPRODUCTTYPE = "合计：", FCOMMITQTY = commitqty, FHNAMOUNT = hnamount, FWEIGHT = weight, FVOLUME = volume });

            return footer;
        }
    }
}
