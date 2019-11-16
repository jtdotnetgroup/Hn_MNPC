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
    public class AppUserDal : BaseRepository<AppUserModel>
    {
        public static AppUserDal Instance
        {
            get { return SingletonProvider<AppUserDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(AppUserModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }


        public DataTable CheckLogin(string phone, string password)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT T1.* ");
            sql.AppendLine("  FROM TB_APP_USER T1");
             sql.AppendLine(" WHERE T1.PHONE = :PHONE");
            sql.AppendLine("   AND T1.PASSWORD = :PASSWORD");
            sql.AppendLine("   AND (T1.IS_DISABLE =0 OR T1.IS_DISABLE IS NULL)");

            return DbUtils.Query(sql.ToString(), new { PHONE = phone, PASSWORD = password });

        }

        public void UpdateLastLoginTime(string fid)
        {
            DbUtils.ExecuteNonQuery("UPDATE TB_APP_USER SET LAST_LOGIN_TIME =:LAST_LOGIN_TIME WHERE FID =:FID", new { LAST_LOGIN_TIME = DateTime.Now, FID = fid });
        }
    }
}
