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
    public class V_ICPRICEPOLICYBLL
    {
        public static V_ICPRICEPOLICYBLL Instance
        {
            get { return SingletonProvider<V_ICPRICEPOLICYBLL>.Instance; }
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string flag, string FBrandID = null,  string keywords = null, string brandName = "", string clientName = "", string checkState = "", string startDate = "", string endDate = "",string policytype = "")
        {
            return V_ICPRICEPOLICYDAL.Instance.GetJson(pageindex, pagesize, flag, FBrandID: FBrandID,keywords: keywords, brandName:brandName, clientName: clientName, checkState:checkState, startDate: startDate, endDate: endDate, policytype:policytype);
        }
    }

}