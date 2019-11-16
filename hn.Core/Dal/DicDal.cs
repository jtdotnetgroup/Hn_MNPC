using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Core.Model;
using hn.Common.Data;
using hn.Common.Provider;

namespace hn.Core.Dal
{
    public class DicDal : BaseRepository<Dic>
    {
        public static DicDal Instance
        {
            get { return SingletonProvider<DicDal>.Instance; }
        }

        //public IEnumerable<Dic> GetListBy(string categoryCode)
        //{
        //    return GetList("select * from sys_dics where code=:code", new { code = categoryCode });
        //}

        public IEnumerable<Dic> GetListByCategoryCode(string categoryCode)
        {
            return GetWhere(new { CategoryCode = categoryCode });
        }

        public IEnumerable<Dic> GetListBy(string categoryId)
        {
            return GetWhere(new {CategoryId = categoryId});
        }

        public IEnumerable<Dic> GetListBy(string categoryid, string parentId)
        {
            var list = GetListBy(categoryid);
            return from n in list
                   where n.ParentId == parentId
                   select n;
        }

        /// <summary>
        /// 校验编码
        /// </summary>
        /// <returns></returns>
        public List<Dic> CheckCode(string code, string id)
        {
            List<Dic> list = new List<Dic>();

            if (string.IsNullOrEmpty(id))
            {
                list = GetList("select FID from Sys_Dics where Code = '" + code + "'", null).ToList();
            }
            else
            {
                list = GetList("select FID from Sys_Dics where Code = '" + code + "' and FID != " + id, null).ToList();
            }

            return list;
        }
    }
}
