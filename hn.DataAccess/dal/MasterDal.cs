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
using hn.Common.Data.Filter;

namespace hn.DataAccess.Dal
{
    public class MasterDal : BaseRepository<MasterModel>
    {
        public string Where = "";
        public static MasterDal Instance
        {
            get { return SingletonProvider<MasterDal>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID",
                              string order = "asc")
        {
            return base.JsonDataForEasyUIdataGrid(TableConvention.Resolve(typeof(MasterModel)), pageindex, pagesize, filterJson,
                                                  sort, order);
        }

        public override DataTable GetPageWithSp(ProcCustomPage pcp, out int recordCount)
        {
            //if (SysVisitor.Instance.CurrentUser != null && SysVisitor.Instance.CurrentUser.ORGANIZE_NAME != "总部")
            //{
            //    pcp.SQL_WHERE_IN += string.Format(" AND ORGANIZE_ID='{0}'", SysVisitor.Instance.CurrentUser.ORGANIZE_ID);
            //}

            pcp.SQL_WHERE_IN += Where;

            return DbUtils.GetPageWithSp(pcp, out recordCount);
        }

        public List<MasterModel> GetListByIds(string ids)
        {
            string sql = "SELECT * FROM TB_MASTER WHERE FID IN ('" + ids.Replace(",","','") + "')";

            return DbUtils.GetList<MasterModel>(sql, null).ToList();
        }

        public string GetNewNo()
        {
            string seq = MasterDal.Instance.GetAll().Count().ToString("0000");
            return "YAJ" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "A" + seq + "01";
        }

        //public override int DeleteByIds(string ids)
        //{
        //    int count = 0;
       

        //    return count;
        //}

        public DataTable GetData(string filterJson)
        {
            string where = FilterTranslator.ToSql(filterJson);
            string sql = "SELECT * FROM TB_MASTER WHERE 1=1 ";
            if (where != "" && where != "()")
            {
                sql +="   and " + where;
            }

            DataTable table = DbUtils.Query(sql);
            

            return table;
        }

        public MasterModel GetModelByNo(String no)
        {
            List<MasterModel> list = this.GetWhere(new { MASTER_NO = no }).ToList();
            if (list.Count > 0)
            {
                return list[0];
            }

            return null;
        }
    }
}
