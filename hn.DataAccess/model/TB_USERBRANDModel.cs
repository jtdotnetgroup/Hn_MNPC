using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("TB_USERBRAND")]
    [Description("品牌用户授权表")]
    public class TB_USERBRANDModel
    {
        /// <summary>
        /// 内码
        /// </summary>
        public virtual string FID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual string FUSERID { get; set; }

        /// <summary>
        /// 品牌部ID
        /// </summary>
        public virtual string FBRANDID { get; set; }

        public virtual string FBRANDNUMBER { get; set; }
        public virtual string FBRANDNAME { get; set; }
    }
}
