using hn.Common.Data;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("V_PREMISE")]
    [Description("经营场所")]
    public class V_PREMISEModel: TB_PREMISEModel
    {
        /// <summary>
        /// 集团名称
        /// </summary>
        public string FORGNAME { get; set; }

        /// <summary>
        /// 一级销区名称
        /// </summary>
        public string FCLASSAREA1NAME { get; set; }

        /// <summary>
        /// 二级销区名称
        /// </summary>
        public string FCLASSAREA2NAME { get; set; }

        /// <summary>
        /// 品牌部名称
        /// </summary>
        public string FBRANDNAME { get; set; }
    }
}
