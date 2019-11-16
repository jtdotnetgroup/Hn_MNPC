using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Provider;
using hn.Common.Data;
using hn.Common.Data.Filter;
using hn.Core.Model;
namespace hn.Core.Dal
{
    public class ButtonDal : BaseRepository<Button>
    {
        
        public static ButtonDal Instance
        {
            get { return SingletonProvider<ButtonDal>.Instance; }
        }

        public string JsonDataForEasyUIdataGrid(int pageindex, int pagesize,string filterJson,string sort="FID",string order="asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage("sys_buttons")
                          {
                              IDX_PAGE_IN = pageindex,
                              CURR_PAGE_COUNT_IN = pagesize,
                              SQL_ORDERBY_IN = sortorder,
                              SQL_WHERE_IN = FilterTranslator.ToSql(filterJson)
                          };
            int recordCount ;
            DataTable dt = base.GetPageWithSp(pcp,out recordCount);
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, dt);

        }

        /// <summary>
        /// 获取菜单中的按钮列表
        /// </summary>
        /// <param name="navid">菜单ID</param>
        /// <returns></returns>
        public IEnumerable<Button> GetButtonsBy(string navid)
        {
            const string sql = "select b.*,n.sortnum sn from Sys_NavButtons n join sys_buttons b on n.buttonid=b.FID where n.navid=:navid order by n.Sortnum";
            return GetList(sql, new {NavId = navid});
        }

        
    }
}
