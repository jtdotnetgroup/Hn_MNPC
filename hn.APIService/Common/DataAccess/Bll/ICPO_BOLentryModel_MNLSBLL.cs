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
using hn.DataAccess.Model;

namespace zt.DataAccess.Bll
{
    public class ICPO_BOLentryModel_MNLSBLL
    {

        public static ICPO_BOLentryModel_MNLSBLL Instance
        {
            get { return SingletonProvider<ICPO_BOLentryModel_MNLSBLL>.Instance; }
        }

        public int Update(ICPO_BOLentryModel_MNLS model)
        {
            return ICPO_BOLentryModel_MNLSDal.Instance.Update(model);
        }

        public int Delete(int FID)
        {
            return ICPO_BOLentryModel_MNLSDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return ICPO_BOLentryModel_MNLSDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }
}
