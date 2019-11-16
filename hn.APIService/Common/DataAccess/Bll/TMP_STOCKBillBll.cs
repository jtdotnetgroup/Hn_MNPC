using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using hn.APIService.Data.Provider;
using hn.APIService.DataAccess.Model;
using hn.APIService.DataAccess.Dal;

namespace zt.DataAccess.Bll
{
    public class TMP_STOCKBillBll
    {

        public static TMP_STOCKBillBll Instance
        {
            get { return SingletonProvider<TMP_STOCKBillBll>.Instance; }
        }

        public int Update(TMP_STOCKBillModel model)
        {
            return TMP_STOCKBillDal.Instance.Update(model);
        }

        public int Delete(int FID)
        {
            return TMP_STOCKBillDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TMP_STOCKBillDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }
}
