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
    public class Customer_TG_Order_HuaNaiBll
    {

        public static Customer_TG_Order_HuaNaiBll Instance
        {
            get { return SingletonProvider<Customer_TG_Order_HuaNaiBll>.Instance; }
        }

        public int Update(Customer_TG_Order_HuaNaiModel model)
        {
            return Customer_TG_Order_HuaNaiDal.Instance.Update(model);
        }

        public int Delete(int FID)
        {
            return Customer_TG_Order_HuaNaiDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return Customer_TG_Order_HuaNaiDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }
}
