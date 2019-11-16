using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.Model
{
    [TableName("TB_CLIENTACCOUNT")]
    [Description("厂家账户")]
    public class TB_CLIENTACCOUNTModel
    {
        //内码ID
        public string FID { get; set; }
        public string FACCOUNT { get; set; }
        public string FNAME { get; set; }
        public string FUSERID { get; set; }
        public decimal FSTATUS { get; set; }
        public string FREMARK { get; set; }
        public string FBRANDID { get; set; }
        public decimal FCOMMONFLAG { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string FCOMPANY { get; set; }
        /// <summary>
        /// JDE編碼
        /// </summary>
        public string FJDE { get; set; }
        public int FSORT { get; set; }
    }
}
