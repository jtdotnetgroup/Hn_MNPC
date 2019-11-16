using hn.Common.Data;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_REVIEWTEAM")]
    [Description("审批组")]
    public class TB_REVIEWTEAMModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FNAME { get; set; }

        public int FBILL_TYPE { get; set;}

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }
    }
}
