using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.Filter;
using hn.Common.Provider;
using hn.Core.Dal;
using hn.Core.Model;
using System.Data.SqlClient;
using hn.Common.Data.SqlServer;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Bll
{
    public class V_ICPOBILLENTRYBLL
    {
        public static V_ICPOBILLENTRYBLL Instance
        {
            get { return SingletonProvider<V_ICPOBILLENTRYBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int rows = 15, string ICPOBILLID = null, int status = 0, string sort = "FID", string order = "asc")
        {
            return V_ICPOBILLENTRYDAL.Instance.GetJson(page, rows, ICPOBILLID, status, sort, order);
        }

        /// <summary>
        /// 删除采购订单下明细
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public int DeleteByICPOBILLID(string FID)
        {
            return V_ICPOBILLENTRYDAL.Instance.DeleteWhere(new { FID = FID });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Add(V_ICPOBILLENTRYMODEL model, string ICPOBILLID)
        {
            return V_ICPOBILLENTRYDAL.Instance.InsertWithFID(model, ICPOBILLID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ICPOBILLENTRYMODEL> GetByICPOBILLENTRY(string ICPOBILLID)
        {
            return V_ICPOBILLENTRYDAL.Instance.GetWhere(new { FICPOBILLID = ICPOBILLID });
        }
    }
}