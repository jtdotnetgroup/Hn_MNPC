using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using hn.Core.Model;
using hn.DataAccess.Dal;

namespace hn.DataAccess.model
{
    [TableName("v_thd")]
    [Description("v_thd")]
    public class v_thd
    {
        public string fpremisename { get; set; }
    }
}
