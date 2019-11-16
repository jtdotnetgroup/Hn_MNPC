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
using hn.DataAccess.Bll;

namespace hn.DataAccess.Dal
{
    public class TP_INVENTORYDAL : BaseRepository<TP_INVENTORYMODEL>
    {
        public static TP_INVENTORYDAL Instance
        {
            get { return SingletonProvider<TP_INVENTORYDAL>.Instance; }
        }

    }
}
