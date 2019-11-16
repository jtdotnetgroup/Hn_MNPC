using hn.Common.Data;
using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class ProductViewDal : BaseRepository<ProductViewModel>
    {
        public static ProductViewDal Instance
        {
            get { return SingletonProvider<ProductViewDal>.Instance; }
        }

        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
        //                      string order = "asc")
        //{
        //    return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(ProductViewModel)), pageindex, pagesize, filterJson,
        //                                          sort, order);
        //}

      

        public string GetJson(int pageindex, int pagesize, string FORGID = null, string FTYPEID = null, string categoryID = null, string FBRANDID = null, string keywords = null, string status = null, string sort = "FID", string order = "asc")
        {
            StringBuilder query = new StringBuilder();

            query.Append(" 1=1 ");

            //if(!FORGID.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FORGID = '{0}' ", FORGID);
            //}

            //if (!FTYPEID.IsNullOrEmpty())
            //{
            //    query.AppendFormat(" and FTYPEID = '{0}' ", FTYPEID);
            //}


            if (!categoryID.IsNullOrEmpty() && categoryID != "0")
            {
                query.AppendFormat(" and FTYPEID in ('{0}') ", categoryID);
            }

            if (!FBRANDID.IsNullOrEmpty())
            {
                query.AppendFormat(" and FBRANDID = '{0}' ", FBRANDID);
            }

            if (!status.IsNullOrEmpty())
            {
                query.AppendFormat(" and FCHECKSTATUS = {0} ", status);
            }

            if (!keywords.IsNullOrEmpty())
            {
                query.Append(" and ( ");

                query.AppendFormat("  FPRODUCTNAME like '%{0}%' ", keywords);
                query.AppendFormat(" OR FPRODUCTTYPE like '%{0}%' ", keywords);
                query.AppendFormat(" OR FPRODUCTCODE like '%{0}%' ", keywords);
                query.AppendFormat(" OR FMODEL like '%{0}%' ", keywords);

                query.Append(" ) ");
            }

            if (!SysVisitor.Instance.IsAdmin)
            {
                //品牌/厂家进行数据权限控制
                query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", SysVisitor.Instance.UserId);
            }

            return JsonDataForEasyUIdataGrid(pageindex, pagesize, query.ToString(), sort, order);
        }
    }
}
