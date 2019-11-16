using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.Model
{
    [TableName("V_DELIVERY_DETAIL")]
    [Description("装车发货明细")]
    public class V_DELIVERY_DETAILModel: TB_DELIVERY_DETAILModel
    {
        //内码ID
        public string FCLASSAREA2NAME { get; set; }
        public string FBRANDNAME { get; set; }
        public string FTRANSNAME { get; set; }  
        public string FEXPRESSCOMPANYNAME { get; set; }

    }
}
