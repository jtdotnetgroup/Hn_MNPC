using hn.Common.Data;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_PREMISE")]
    [Description("经营场所")]
    public class TB_PREMISEModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 所属销区
        /// </summary>
        public string FORGID { get; set; }

        /// <summary>
        /// 场所编号
        /// </summary>
        public string FCODE { get; set; }

        /// <summary>
        /// 场所名称
        /// </summary>
        public string FNAME { get; set; }

        /// <summary>
        /// 一级销区
        /// </summary>
        public string FCLASSAREA1 { get; set; }

        /// <summary>
        /// 二级销区
        /// </summary>
        public string FCLASSAREA2 { get; set; }

        /// <summary>
        /// 品牌部
        /// </summary>
        public string FBRAND { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int FSTATUS { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string FCOMPANY { get; set; }

        /// <summary>
        /// 是否开票
        /// </summary>
        public int FISTICKET { get; set; }
    }
}
