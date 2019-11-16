using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.SqlServer;
using hn.Common.Provider;
using hn.Core.Model;
using hn.DataAccess.Model;
using hn.Core;

namespace hn.DataAccess.Dal
{
    public class TB_OrganizationDal : BaseRepository<TB_OrganizationModel>
    {
        public static TB_OrganizationDal Instance
        {
            get { return SingletonProvider<TB_OrganizationDal>.Instance; }
        }

        public IEnumerable<TB_OrganizationModel> GetChildren(string parentid ="0")
        {
           //if (parentid == "0")
           // {
           //     return GetAll().Where(d => d.FPARENTALID.IsNullOrEmpty());
           // }

            return GetAll().Where(d => d.FPARENTALID == parentid);
        }

        public bool IsExsit(int fid, string number)
        {

            DataTable table = DbUtils.Query(string.Format("SELECT * FROM TB_Organization WHERE FID<>{0} AND FORGNAME='{1}'", fid, number));
            return table.Rows.Count > 0;
        }

    }
}
