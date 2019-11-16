using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.Model
{
    [TableName("V_CLIENTACCOUNT")]
    [Description("厂家账户视图")]
    public class V_CLIENTACCOUNTModel : TB_CLIENTACCOUNTModel
    {
        public string FUSERNAME { get; set; }
        public string FBRANDNAME { get; set; }
    }
}
