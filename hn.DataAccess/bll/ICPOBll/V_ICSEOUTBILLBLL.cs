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
    public class V_ICSEOUTBILLBLL
    {
        public static V_ICSEOUTBILLBLL Instance
        {
            get { return SingletonProvider<V_ICSEOUTBILLBLL>.Instance; }
        }


        public string GetEasyUIJson(
            int page = 1,
            int pageSize = 15, 
            string startDate = null, 
            string endDate = null,
            string FOrgID = null, 
            int status = 0,
            string FPREMISEID = null,
            string FCLASSAREA2NAME = null,
            string FCARNUMBER = null,
            bool chkclose = false,
            string sort = "FBILLDATE", 
            string order = "desc")
        {
            return V_ICSEOUTBILLDAL.Instance.GetJson(page, pageSize, startDate, endDate, FOrgID, status, FPREMISEID, FCLASSAREA2NAME, FCARNUMBER, chkclose, sort, order);
        }
    }
}