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
    public class OrganizeDal : BaseRepository<OrganizeModel>
    {
        private string _Organizeid = "";
        public static OrganizeDal Instance
        {
            get { return SingletonProvider<OrganizeDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc", string Organizeid = "")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(OrganizeModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public override DataTable GetPageWithSp(ProcCustomPage pcp, out int recordCount)
        {
            DataTable table = base.GetPageWithSp(pcp, out recordCount);
            if (pcp.IDX_PAGE_IN == 1)
            {
                DataRow newRow = table.NewRow();
                newRow["FID"] = "0";
                newRow["NAME"] = "总部";
                table.Rows.InsertAt(newRow, 0);
            }

            return table;
        }

        public OrganizeModel GetModelByName(String name)
        {
            if (name == "总部")
            {
                return new OrganizeModel() { NAME = name, FID = "0" };
            }

            List<OrganizeModel> list = this.GetWhere(new { NAME = name }).ToList();
            if (list.Count > 0)
            {
                return list[0];
            }

            return null;
        }

    }
}
