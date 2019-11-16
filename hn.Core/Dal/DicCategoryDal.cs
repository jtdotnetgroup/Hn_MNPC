using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Core.Model;
using hn.Common.Data;
using hn.Common.Provider;

namespace hn.Core.Dal
{
    public class DicCategoryDal : BaseRepository<DicCategory>
    {
        public static DicCategoryDal Instance
        {
            get { return SingletonProvider<DicCategoryDal>.Instance; }
        }

        
    }

}
