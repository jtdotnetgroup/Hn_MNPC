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
namespace hn.DataAccess.Model
{
    [TableName("V_ICPOBILL")]
    [Description("采购订单")]
    public class V_ICPOBILLMODEL : ICPOBILLMODEL
    {
        public string FICSEBILLNAME { get; set; }
        public string FBRANDNAME { get; set; }

        public string FSRCCODE { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DbField(false)]
        public string FSTATUSNAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.ICPOBILL_FSTATUS), FSTATUS);
            }
        }
    }
}
