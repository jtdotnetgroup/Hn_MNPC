using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Common.Data;
using hn.Common.Provider;
using hn.Core.Model;


namespace hn.Core.Dal
{
    public class SeqDal : BaseRepository<SeqModel>
    {
        public static SeqDal Instance
        {
            get { return SingletonProvider<SeqDal>.Instance; }
        }

        //public static SeqModel Get(string seq_no)
        //{
        //    Instance.Get(
        //}
    }
}