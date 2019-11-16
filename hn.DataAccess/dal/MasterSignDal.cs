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
    public class MasterSignDal : BaseRepository<MasterSignModel>
    {
        public static MasterSignDal Instance
        {
            get { return SingletonProvider<MasterSignDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(MasterSignModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public DataTable GetLastLocaltionByMasterID(string masterIds)
        {
            masterIds = "'" + masterIds.Replace(",", "','") + "'";
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT T1.LNG, T1.LAT,T3.NAME,T1.ADDRESS,T3.PHONE,T1.SIGN_TIME");
            sql.AppendLine("  FROM TB_MASTER_SIGN T1");
            sql.AppendLine(" INNER JOIN (SELECT max(SIGN_TIME) MAX_DATE, SIGNER_ID");
            sql.AppendLine("               FROM TB_MASTER_SIGN");
            sql.AppendLine("             HAVING SIGNER_ID IN(" + masterIds + ")");
            sql.AppendLine("              GROUP BY SIGNER_ID) T2");
            sql.AppendLine("    ON T1.SIGNER_ID = T2.SIGNER_ID");
            sql.AppendLine("   AND T1.SIGN_TIME = T2.MAX_DATE");
            sql.AppendLine("LEFT JOIN TB_MASTER T3 ON T1.SIGNER_ID = T3.FID");
            sql.AppendLine(" WHERE T1.LNG IS NOT NULL AND T1.LAT IS NOT NULL");

            return DbUtils.Query(sql.ToString());
        }

    }
}
