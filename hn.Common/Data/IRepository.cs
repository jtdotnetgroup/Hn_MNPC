using System.Collections.Generic;

namespace hn.Common.Data
{
    public interface IRepository<T> where T : new()
    {
        T Get(string id);
        IEnumerable<T> GetAll();
        string Insert(T o);
        int Update(T o);
        int UpdateWhatWhere(object what, object where);
        int InsertNoIdentity(T o);
        IEnumerable<T> GetPage(int page, int pageSize);
        int Count();
        IPageable<T> GetPageable(int page, int pageSize);
        IEnumerable<T> GetWhere(object where);
        int Delete(string id);
        int CountWhere(object where);
        string JsonDataForEasyUIdataGrid(int pageindex,int pagesize);
        string JsonDataForjQgrid(int pageindex, int pagesize);
    }
}
