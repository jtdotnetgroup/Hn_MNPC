using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.DataAccess.model;
using hn.Common.Provider;

namespace hn.DataAccess.dal.TB_DZmanuscript_DAL
{
    public class TB_DZmanuscript_DAL: BaseRepository<TB_DZmanuscript>
    {
        public static TB_DZmanuscript_DAL Instance()
        {
            return SingletonProvider<TB_DZmanuscript_DAL>.Instance;
        }

        //public Insert (TB_DZmanuscript input)
        //{

        //}
    }
}
