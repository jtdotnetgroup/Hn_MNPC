using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Common.Data;
using hn.Core.Model;
using hn.Common.Provider;
namespace hn.Core.Dal
{
    public class DepartmentDal:BaseRepository<Department>
    {
        public static DepartmentDal Instance
        {
            get { return SingletonProvider<DepartmentDal>.Instance; }
        }

        public IEnumerable<Department> GetChildren(string parentid = "0")
        {
            return GetAll().Where(d => d.ParentId == parentid);
        }
    }
}
