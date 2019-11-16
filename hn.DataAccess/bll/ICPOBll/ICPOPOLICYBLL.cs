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
    public class ICPOPOLICYBLL
    {
        public static ICPOPOLICYBLL Instance
        {
            get { return SingletonProvider<ICPOPOLICYBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int rows = 15, string FOrgID = null, string FBrandID = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            return ICPOPOLICYDAL.Instance.GetJson(page, rows, FOrgID, FBrandID, keywords, sort, order);
        }

        /// <summary>
        /// 删除采购订单下价格政策
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public int DeleteByICPOBILLID(string FID)
        {
            return ICPOPOLICYDAL.Instance.DeleteWhere(new { FID = FID });
        }

        public int Add(ICPOPOLICYMODEL model, string ICPOBILLID)
        {
            return ICPOPOLICYDAL.Instance.InsertWithFID(model, ICPOBILLID);
        }
    }
}