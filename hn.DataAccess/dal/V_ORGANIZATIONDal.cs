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

namespace hn.DataAccess.Dal
{
    public class V_ORGANIZATIONDal : BaseRepository<V_ORGANIZATIONModel>
    {
        public static V_ORGANIZATIONDal Instance
        {
            get { return SingletonProvider<V_ORGANIZATIONDal>.Instance; }
        }
    }
}
