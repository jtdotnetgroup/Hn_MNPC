using hn.Common.Data;
using System;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("ICPRBILLAUDIT")]
    [Description("审批记录表")]
    public class ICPRBILLAUDITModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        public string FFLOWID { get; set; }
        public int FBILLTYPE { get; set; }
        public string FBILLID { get; set; }
        public string FAUDITOR { get; set; }
        public DateTime FAUDIT_TIME { get; set; }
        public int FSTATUS { get; set; }
        public string FREMARK { get; set; }

        public int FSORT { get; set; }

        [DbField(false)]
        public string TRUENAME { get; set; }
        ///// <summary>
        ///// 审批节点名称
        ///// </summary>
        //[DbField(false)]
        //public string FREVIEWTEAMNAME { get; set; }
    }
}
