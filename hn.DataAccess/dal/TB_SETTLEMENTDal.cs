using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class TB_SETTLEMENTDal : BaseRepository<TB_SETTLEMENTModel>
    {
        public static TB_SETTLEMENTDal Instance
        {
            get { return SingletonProvider<TB_SETTLEMENTDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(TB_SETTLEMENTModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            return base.JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }

        public string GetJson(
            int page = 1,
            int rows = 15,
            string brand = null,
            string settleorg = null,
            string startDate = null,
            string endDate = null,
            string sort = "FSTATUS",
            string order = "")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDATE_APPLIED >= ({0}) ", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDATE_APPLIED >= ({0}) ", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!brand.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBRANDNAME = '{0}' ", brand);
            }


            if (!settleorg.IsNullOrEmpty())
            {
                query.AppendFormat(" and FSETTLE_ORG  LIKE '%{0}%' ", settleorg);
            }

            return JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }
    }
}
