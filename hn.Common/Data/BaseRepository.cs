using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using hn.Common.Data.Filter;
using Omu.ValueInjecter;

namespace hn.Common.Data
{
    public class BaseRepository<T> : IRepository<T> where T : new()
    {

        public T Get(string id)
        {
            return DbUtils.Get<T>(id);
        }

        public T GetByID(string id)
        {
            return DbUtils.GetByID<T>(id);
        }

        public IEnumerable<T>GetByIDList(IEnumerable<string> IDList)
        {
            return DbUtils.GetByIDList<T>(IDList);
        }

        public IEnumerable<T> GetAll()
        {
            return DbUtils.GetAll<T>();
        }

        /// <summary>
        /// 执行SQL语句并返回指定类型的列表
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="whereParam">条件参数</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(string sql, object whereParam)
        {
            return DbUtils.GetList<T>(sql, whereParam);
        }

        public IEnumerable<T> GetList(string sql)
        {
            return DbUtils.GetList<T>(sql);
        }

        public IEnumerable<string> GetColumnList(string filed)
        {
            return DbUtils.GetColumnList<string>(filed);
        }

        public IEnumerable<T> GetWhere(string where, object whereParam)
        {
            return DbUtils.GetWhere<T>(where, whereParam);
        }

        public virtual string Insert(T o)
        {
            return DbUtils.Insert(o);
        }

        public int InsertWithFID(T o,string FID)
        {
            return DbUtils.InsertWithFID(o, FID);
        }

        public virtual int Update(T o)
        {
            return DbUtils.Update(o);
        }

        public virtual int UpdateWhatWhere(object what, object where)
        {
            return DbUtils.UpdateWhatWhere<T>(what, where);
        }

        public virtual int InsertNoIdentity(T o)
        {
            return DbUtils.InsertNoIdentity(o);
        }

        public IEnumerable<T> GetPage(int page, int pageSize)
        {
            return DbUtils.GetPage<T>(page, pageSize);
        }

        public int Count()
        {
            return DbUtils.Count<T>();
        }
        
        public object GetExecuteScalarWhere(string field,object where)
        {
            return DbUtils.GetExecuteScalarWhere<T>(field,where);
        }


        public int CountBySQL(string sql)
        {
            return DbUtils.CountBySQL(sql);
        }

        public IPageable<T> GetPageable(int page, int pageSize)
        {
            return new Pageable<T>
            {
                Rows = GetPage(page, pageSize),
                PageCount = DbUtils.GetPageCount(pageSize, Count()),
                PageIndex = page,
            };
        }

        public IEnumerable<T> GetWhere(object where)
        {
            return DbUtils.GetWhere<T>(where);
        }


        public IEnumerable<T> GetWhereStr(string where)
        {
            return DbUtils.GetWhereStr<T>(where);
        }

        public IEnumerable<T> GetWhereStr(string where,string order)
        {
            return DbUtils.GetWhereStr<T>(where, order);
        }

        public IEnumerable<T> Query(string sql)
        {
            return DbUtils.Query<T>(sql);
        }

        public int Delete(string id)
        {
            return DbUtils.Delete<T>(id);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">格式如： 1,2,4,6,8....</param>
        /// <returns></returns>
        public virtual int BatchDelete(string ids)
        {
            return DbUtils.BatchDelete<T>(ids);
        }

        public int DeleteWhere(object where)
        {
            return DbUtils.DeleteWhere<T>(where);
        }

        public int CountWhere(object where)
        {
            return DbUtils.CountWhere<T>(where);
        }

        public virtual DataTable GetPageWithSp(ProcCustomPage pcp, out int recordCount)
        {
            if (pcp.SQL_ORDERBY_IN != null)
            {
                pcp.SQL_ORDERBY_IN = pcp.SQL_ORDERBY_IN.Trim();
            }

            return DbUtils.GetPageWithSp(pcp, out recordCount);
        }


        public virtual string JsonDataForEasyUIdataGrid(int pageindex, int pagesize)
        {
            IEnumerable<T> list = this.GetPage(pageindex, pagesize);

            int recordcount = this.Count();

            return JSONhelper.FormatJSONForEasyuiDataGrid(recordcount, list);

        }

        public string JsonDataForEasyUIdataGrid(string tablename, int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(tablename)
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = sortorder.Trim(),
                SQL_WHERE_IN = FilterTranslator.ToSql(filterJson)
            };
            //  LogHelper.WriteLog("where: "+pcp.SQL_WHERE_IN);
            int recordCount;
            //DataTable dt = GetPageWithSp(pcp, out recordCount);
            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, list);

        }

        public string JsonDataForEasyUIdataGrid(string where,string tablename, int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(tablename)
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = sortorder.Trim(),
                SQL_WHERE_IN = where
            };
            //  LogHelper.WriteLog("where: "+pcp.SQL_WHERE_IN);
            int recordCount;
            //DataTable dt = GetPageWithSp(pcp, out recordCount);
            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, list);

        }


        public string JsonDataForEasyUIdataGrid(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(GetTableName())
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = sortorder.Trim(),
                SQL_WHERE_IN = string.Format("({0})", filterJson.IsNullOrEmpty() ? " 1=1 " : filterJson),
            };
            //  LogHelper.WriteLog("where: "+pcp.SQL_WHERE_IN);
            int recordCount;
            //DataTable dt = GetPageWithSp(pcp, out recordCount);
            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, list);

        }

        public IEnumerable<T> GetPageList(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(GetTableName())
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = sortorder.Trim(),
                SQL_WHERE_IN = string.Format("({0})", filterJson.IsNullOrEmpty() ? " 1=1 " : filterJson),
            };
            //  LogHelper.WriteLog("where: "+pcp.SQL_WHERE_IN);
            int recordCount;
            //DataTable dt = GetPageWithSp(pcp, out recordCount);
            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));
            return list;

        }


        public string JsonDataForEasyUIdataGridFoorter(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(GetTableName())
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = sortorder.Trim(),
                SQL_WHERE_IN = string.Format("({0})", filterJson.IsNullOrEmpty() ? " 1=1 " : filterJson),
            };
            //  LogHelper.WriteLog("where: "+pcp.SQL_WHERE_IN);
            int recordCount;
            //DataTable dt = GetPageWithSp(pcp, out recordCount);
            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));
            return JSONhelper.FormatJSONForEasyuiDataGridFooter(recordCount, list, GetEasyuiDatagridFooter(list));
        }

        protected virtual object GetEasyuiDatagridFooter(IEnumerable<T> list)
        {
            return null;
        }

        public IEnumerable<T> GetPageWtihRecordCount(int page,int pageSize,out int recordCount,string filterJson = null, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(GetTableName())
            {
                IDX_PAGE_IN = page,
                CURR_PAGE_COUNT_IN = pageSize,
                SQL_ORDERBY_IN = sortorder.Trim(),
                SQL_WHERE_IN = string.Format("({0})", filterJson.IsNullOrEmpty() ? " 1=1 " : filterJson),
            };

            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));

            return list;
        }

        private static IEnumerable<T> GetList<T>(DataTable data) where T : new()
        {
            foreach (DataRow item in data.Rows)
            {
                var o = new T();
                o.InjectFrom<DataRowInjection>(item);
                yield return o;
            }
        }


        public virtual string JsonDataForjQgrid(int pageindex, int pagesize)
        {
            IPageable<T> page = this.GetPageable(pageindex, pagesize);
            int recordcount = this.Count();
            return JSONhelper.FormatJSONForJQgrid(page.PageCount, pageindex, recordcount, page.Rows);
        }

        public virtual DataTable GetDatatForPage(string tablename, int pageindex, int pagesize, string where, string orderby = "")
        {
            var pcp = new ProcCustomPage(tablename)
            {
                IDX_PAGE_IN = pageindex,
                CURR_PAGE_COUNT_IN = pagesize,
                SQL_ORDERBY_IN = orderby,
                SQL_WHERE_IN = where
            };
            int recordCount;
            DataTable dt = GetPageWithSp(pcp, out recordCount);
            return dt;

        }

        public virtual string GetTableName()
        {
            return TableConvention.Resolve(typeof(T));
        }


        #region 事务方法

        public string GetInsertCommandText(T o)
        {
            return DbUtils.GetInsertCommandText(o);
        }

        public string GetInsertCommandText(IEnumerable<T> o)
        {
            StringBuilder commandTextList = new StringBuilder();

            foreach (var item in o)
            {
                commandTextList.Append(GetInsertCommandText(o));
            }

            return commandTextList.ToString();
        }

        /// <summary>
        /// 获取默认删除ID命令字符串
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetDeleteCommandText(string ID)
        {
            return DbUtils.GetDeleteCommandText<T>(ID);
        }

        /// <summary>
        /// 获取默认删除ID命令字符串
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public string GetDeleteWhereCommandText(object where)
        {
            return DbUtils.GetDeleteWhereCommandText<T>(where);
        }

        #endregion
    }
}
