using hn.Common;
using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
   public class V_DELIVERY_DETAILDal : BaseRepository<V_DELIVERY_DETAILModel>
    {
        public static V_DELIVERY_DETAILDal Instance
        {
            get { return SingletonProvider<V_DELIVERY_DETAILDal>.Instance; }
        }


        public IEnumerable<V_DELIVERY_DETAILModel> GetChildren(string parentid = "0")
        {
            return GetAll().Where(d => d.FID == parentid);
        }


        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                      string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(V_DELIVERY_DETAILModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }


        public string GetEasyUIJson(int page = 1, int pageSize = 15,  string sort = "FID", string order = "asc")
        {
            return JsonDataForEasyUIdataGrid(page, pageSize, null, sort, order);
        }


        public string GetJson(
            int page = 1,
            int rows = 15,
            string brandid=null,
            string groupno = null,
            string startDate = null,
            string endDate = null,
            string classarea2name = null,
            string status = null,
            string transid = null,          
            string sort = "FSTATUS",
            string order = "")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            if (!startDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDATE >= ({0}) ", startDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!endDate.IsNullOrEmpty())
            {
                query.AppendFormat(" and FDATE >= ({0}) ", endDate.ToDateTime().GetOracleToDateShortString());
            }

            if (!brandid.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBRANDID = '{0}' ", brandid);
            }


            if (!groupno.IsNullOrEmpty())
            {
                query.AppendFormat(" and FGROUP_NO  LIKE '%{0}%' ", groupno);
            }


            if (!classarea2name.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCLASSAREA2NAME LIKE '%{0}%' ", classarea2name);
            }

            if (!transid.IsNullOrEmpty())
            {
                query.AppendFormat(" and FTRANSID = '{0}' ", transid);
            }

            if (!status.IsNullOrEmpty())
            {
                query.AppendFormat(" and FSTATUS = '{0}' ", status);
            }

            return JsonDataForEasyUIdataGrid(page, rows, query.ToString(), sort, order);
        }

        public string GetDetail(string groupno)
        {
            if (!string.IsNullOrEmpty(groupno))
            {
                string sql = string.Format("select t1.*  from V_ICSEOUTBILLENTRY t1 inner join ICSEOUTBILL t2 on t1.FICSEOUTID = t2.fid  and t2.fgroup_no='{0}'", groupno);

                var table = DbUtils.Query(sql);
                return JSONhelper.FormatJSONForEasyuiDataGrid(table.Rows.Count, table);
            }
            else
            {
                return JSONhelper.FormatJSONForEasyuiDataGrid(0, null);
            }
        }
    }
}
