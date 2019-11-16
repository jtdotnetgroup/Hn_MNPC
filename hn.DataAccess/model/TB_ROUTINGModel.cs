using hn.Common.Data;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_ROUTING")]
    [Description("审批流程表")]
    public class TB_ROUTINGModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string FTYPE { get; set; }

        /// <summary>
        /// 经营场所ID
        /// </summary>
        public string FPREMISEID { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public string FBRANDID { get; set; }

        /// <summary>
        /// 审批组ID
        /// </summary>
        public string FREVIEWTEAMID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int FNODECOUNT { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        public string FAPPROVER_USERID1 { get; set; }
        public string FAPPROVER_USERID2 { get; set; }
        public string FAPPROVER_USERID3 { get; set; }
    }
}
