using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class Newv_thdDal : BaseRepository<v_thd>
    {
        public static Newv_thdDal Instance
        {
            get { return SingletonProvider<Newv_thdDal>.Instance; }
        }
    }
}
