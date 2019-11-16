using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("TB_USERPREMISE")]
    [Description("经营场所用户授权表")]
    public class TB_USERPREMISEModel
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
        /// 经营场所ID
        /// </summary>
        public virtual string FPREMISEID { get; set; }

        public virtual string FPREMISECODE { get; set; }
        public virtual string FPREMISENAME { get; set; }
        public virtual string FPREMISEBRAND { get; set; }
    }
}
