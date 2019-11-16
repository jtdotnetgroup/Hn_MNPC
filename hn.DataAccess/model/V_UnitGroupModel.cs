using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("V_UNITGROUP")]
    [Description("计量单位组")]
    public class V_UNITGROUPModel : TB_UnitGroupModel
    {
        public string FNUMBER { get; set; }
        public string FNAME { get; set;}

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
