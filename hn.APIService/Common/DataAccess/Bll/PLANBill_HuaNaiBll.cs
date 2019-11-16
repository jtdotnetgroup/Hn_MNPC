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
    public class PLANBill_HuaNaiBll
    {

        public static PLANBill_HuaNaiBll Instance
        {
            get { return SingletonProvider<PLANBill_HuaNaiBll>.Instance; }
        }

        public int Update(PLANBill_HuaNaiModel model)
        {
            return PLANBill_HuaNaiDal.Instance.Update(model);
        }

        public int Delete(int FID)
        {
            return PLANBill_HuaNaiDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return PLANBill_HuaNaiDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }
}
