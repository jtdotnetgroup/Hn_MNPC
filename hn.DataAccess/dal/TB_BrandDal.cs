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
    public class TB_BrandDal : BaseRepository<TB_BrandModel>
    {
        public static TB_BrandDal Instance
        {
            get { return SingletonProvider<TB_BrandDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string keywords, string sort = "FID",
                              string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!keywords.IsNullOrEmpty())
            {
                query.Append(" and ( ");

                query.AppendFormat(" FNUMBER like '%{0}%' ", keywords);
                query.AppendFormat(" OR FNAME like '%{0}%' ", keywords);
                query.AppendFormat(" OR FFACTORY like '%{0}%' ", keywords);

                query.Append(" ) ");
            }


            if (!SysVisitor.Instance.IsAdmin)
            {             
                //品牌/厂家进行数据权限控制
                query.AppendFormat("  AND FID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", SysVisitor.Instance.UserId);
            }

            return base.JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            return base.JsonDataForEasyUIdataGrid( pageindex, pagesize, query.ToString(), sort, order);
        }
    }
}
