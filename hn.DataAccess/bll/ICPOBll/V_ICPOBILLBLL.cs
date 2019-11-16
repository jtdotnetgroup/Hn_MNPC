using hn.Common.Provider;
using hn.DataAccess.Dal;
using System.Collections.Generic;

namespace hn.DataAccess.Bll
{
    public class V_ICPOBILLBLL
    {
        public static V_ICPOBILLBLL Instance
        {
            get { return SingletonProvider<V_ICPOBILLBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int rows = 15, string startDate = null, string endDate = null, string brandid = null, int status = 0, string sort = "FDATE", string order = "desc")
        {
            return V_ICPOBILLDAL.Instance.GetJson(page, rows, startDate, endDate, brandid, status, sort, order);
        }
    }
}