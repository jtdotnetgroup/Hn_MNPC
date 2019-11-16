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
    public class SYS_UPDATELOGBLL
    {
        public static SYS_UPDATELOGBLL Instance
        {
            get { return SingletonProvider<SYS_UPDATELOGBLL>.Instance; }
        }

        public string GetEasyUIJson()
        {
            return SYS_UPDATELOGDAL.Instance.GetJson(1,3);
        }

        public IEnumerable<SYS_UPDATELOGMODEL> GetBYHome()
        {
            return SYS_UPDATELOGDAL.Instance.GetPage(1, 3);
        }

        public int GetCount()
        {
            return SYS_UPDATELOGDAL.Instance.Count();
        }

        public IEnumerable<SYS_UPDATELOGMODEL> GetPageWithRecordCount(int page,int pageSize,out int recordCount)
        {
            return SYS_UPDATELOGDAL.Instance.GetPageWtihRecordCount(page, pageSize,out recordCount);
        }
    }

}