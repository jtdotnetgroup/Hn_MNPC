using hn.APIService.Data.Filter;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace hn.APIService
{
    public class BaseRepository<T> : IRepository<T> where T : new()
    {

        public T Get(int id)
        {
            return DbUtils.Get<T>(id);
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

        public virtual int Insert(T o)
        {
            return DbUtils.Insert(o);
        }

        public virtual int InsertEx(T o)
        {
            return DbUtils.InsertEx(o);
        }

        public virtual int Update(T o)
        {
            try
            {
                return DbUtils.Update(o);
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(typeof(BaseRepository<T>), TableConvention.Resolve(o) + " - " + ex.Message + ex.ToString());
                return 0;
            }
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

        public IPageable<T> GetPageable(int page, int pageSize)
        {
            return new Pageable<T>
            {
                Rows = GetPage(page, pageSize),
                PageCount = DbUtils.GetPageCount(pageSize, Count()),
                PageIndex = page,
            };
        }

        public IEnumerable<T> GetWhere(object where, string orderby = "")
        {
            return DbUtils.GetWhere<T>(where, orderby);
        }

        public int Delete(int id)
        {
            return DbUtils.Delete<T>(id);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">格式如： 1,2,4,6,8....</param>
        /// <returns></returns>
        public int Delete(string ids)
        {
            return DbUtils.Delete<T>(ids);
        }

        public int DeleteWhere(object where)
        {
            return DbUtils.DeleteWhere<T>(where);
        }

        public int CountWhere(object where)
        {
            return DbUtils.CountWhere<T>(where);
        }

        public int CountWhere(string where)
        {
            return DbUtils.CountWhere<T>(where);
        }

        public virtual DataTable GetPageWithSp(ProcCustomPage pcp, out int recordCount)
        {
            return DbUtils.GetPageWithSp(pcp, out recordCount);
        }


        public virtual string JsonDataForEasyUIdataGrid(int pageindex, int pagesize)
        {
            IEnumerable<T> list = this.GetPage(pageindex, pagesize);

            int recordcount = this.Count();

            return JsonHelper.FormatJSONForEasyuiDataGrid(recordcount, list);

        }

        public string JsonDataForEasyUIdataGrid(string tablename, int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(tablename)
            {
                PageIndex = pageindex,
                PageSize = pagesize,
                OrderFields = sortorder,
                WhereString = FilterTranslator.ToSql(filterJson)
            };
            int recordCount;
            //DataTable dt = GetPageWithSp(pcp, out recordCount);
            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));

            return JsonHelper.FormatJSONForEasyuiDataGrid(recordCount, list);
        }

        public IEnumerable<T> JsonDataForEasyUIdataGrid(ref int recordCount, string tablename, int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(tablename)
            {
                PageIndex = pageindex,
                PageSize = pagesize,
                OrderFields = sortorder,
                WhereString = FilterTranslator.ToSql(filterJson)
            };
            //DataTable dt = GetPageWithSp(pcp, out recordCount);
            IEnumerable<T> list = GetList<T>(GetPageWithSp(pcp, out recordCount));

            return list;
        }

        public string JsonDataForEasyUIdataGrid(string tablename, int pageindex, int pagesize, string filterJson, string hqlString, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;

            var pcp = new ProcCustomPage(tablename)
            {
                PageIndex = pageindex,
                PageSize = pagesize,
                OrderFields = sortorder,
                WhereString = FilterTranslator.ToSql(filterJson, hqlString)
            };
            int recordCount;
            DataTable dt = GetPageWithSp(pcp, out recordCount);
            return JsonHelper.FormatJSONForEasyuiDataGrid(recordCount, dt);
        }
        public DataTable JsonDataForDataTable(string tablename, int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            string sortorder = sort + " " + order;
            var pcp = new ProcCustomPage(tablename)
            {
                PageIndex = pageindex,
                PageSize = pagesize,
                OrderFields = sortorder,
                WhereString = FilterTranslator.ToSql(filterJson)
            };
            int recordCount;
            DataTable dt = GetPageWithSp(pcp, out recordCount);
            return dt;
        }

        public virtual string JsonDataForjQgrid(int pageindex, int pagesize)
        {
            IPageable<T> page = this.GetPageable(pageindex, pagesize);
            int recordcount = this.Count();
            return JsonHelper.FormatJSONForJQgrid(page.PageCount, pageindex, recordcount, page.Rows);
        }

        private IEnumerable<T> GetList<T>(DataTable data) where T : new()
        {
            foreach (DataRow item in data.Rows)
            {
                var o = new T();
                o.InjectFrom<DataRowInjection>(item);
                yield return o;
            }
        }

    }
}
