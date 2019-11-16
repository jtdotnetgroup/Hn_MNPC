using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.Common.Data
{
    public class ProcCustomPage
    {


        public ProcCustomPage() 
        {
            //ShowFields = "*";
            //KeyFields = "FID";
            SQL_ORDERBY_IN = " ORDER BY FID desc";
            IDX_PAGE_IN = 1;
            CURR_PAGE_COUNT_IN = 20;
            SQL_WHERE_IN = "";
            Sp_PagerName = "FenYe";
        }

        public string Sp_PagerName
        {
            get;
            set;
        }

        public ProcCustomPage(string tablename) :this()
        {
            TABLE_NAME_IN = tablename;
        }
        
        /// <summary>
        /// 表名或视图名称
        /// </summary>
        public string TABLE_NAME_IN { get; set; }

        ///// <summary>
        ///// 查询字段
        ///// </summary>
        //public string ShowFields { get; set; }

        ///// <summary>
        ///// 主键或标识字段
        ///// </summary>
        //public string KeyFields { get; set; }

        /// <summary>
        /// 排序字段 如：FID desc,name asc
        /// </summary>
        public string SQL_ORDERBY_IN { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public decimal IDX_PAGE_IN { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public decimal CURR_PAGE_COUNT_IN { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SQL_WHERE_IN { get; set; }
    }
}
