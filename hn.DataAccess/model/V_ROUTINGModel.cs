using hn.Common.Data;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("V_ROUTING")]
    [Description("审批流程表")]
    public class V_ROUTINGModel : TB_ROUTINGModel
    {
        /// <summary>
        /// 经营场所名称
        /// </summary>
        public string FPREMISENAME { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string FBRANDNAME { get; set; }

        public string FTYPENAME { get; set; }

        /// <summary>
        /// 审批组名称
        /// </summary>
        public string FREVIEWTEAMNAME { get; set; }

        public string FAPPROVER_USERNAME1 { get; set; }
        public string FAPPROVER_TRUENAME1 { get; set; }
        public string FAPPROVER_USERNAME2 { get; set; }
        public string FAPPROVER_TRUENAME2 { get; set; }
        public string FAPPROVER_USERNAME3 { get; set; }
        public string FAPPROVER_TRUENAME3 { get; set; }

    }
}
