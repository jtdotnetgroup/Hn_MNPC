using hn.Common;
using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class ICPRBILLAUDITDal : BaseRepository<ICPRBILLAUDITModel>
    {
        public static ICPRBILLAUDITDal Instance
        {
            get { return SingletonProvider<ICPRBILLAUDITDal>.Instance; }
        }


        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            return base.JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }

        public override string GetTableName()
        {
            return "V_ICPRBILLAUDIT";
        }

        public string GetJson(int pageindex, int pagesize, string billid = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(billid))
            {
                query.AppendFormat(" and FBILLID ='{0}' ", PublicMethod.GetString(billid));

            }

            //query.AppendFormat(" and FSTATUS !=-1");

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), "FSORT", order);
        }

        public IEnumerable<ICPRBILLAUDITModel> GetJson2(string billid = null)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");
            query.AppendFormat(" and FBILLID = '{0}' ", PublicMethod.GetString(billid));
            query.AppendFormat(" and FSTATUS !=-1");

            return GetPageList(1, 99999, query.ToString(), "FSORT", "");
        }
    }
}
