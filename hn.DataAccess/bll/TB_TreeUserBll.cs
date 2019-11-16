using hn.Common.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class TB_TreeUserBll
    {
        public static TB_TreeUserBll Instance
        {
            get { return SingletonProvider<TB_TreeUserBll>.Instance; }
        }
    }
}
