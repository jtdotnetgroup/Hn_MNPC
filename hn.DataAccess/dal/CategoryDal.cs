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
    public class CategoryDal : BaseRepository<CategoryModel>
    {
        public static CategoryDal Instance
        {
            get { return SingletonProvider<CategoryDal>.Instance; }
        }

        public IEnumerable<CategoryModel> GetChildren(string parentid =null)
        {
            return GetAll().Where(d => d.PARENT_ID == parentid);
        }

        public bool IsExsit(int fid, string number)
        {

            DataTable table = DbUtils.Query(string.Format("SELECT * FROM TB_CATEGORY WHERE FID<>{0} AND CATEGORY_NUMBER='{1}'", fid, number));
            return table.Rows.Count > 0;
        }
    }
}
