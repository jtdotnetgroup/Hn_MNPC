using hn.Common.Data;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_EBPL")]
    [Description("三级城市")]
    public class TB_EBPLModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string EBPL_TYPE { get; set; }

        /// <summary>
        /// 地址编号
        /// </summary>
        public string EBPL_CODE { get; set; }

        /// <summary>
        /// 地址名称
        /// </summary>
        public string EBPL_NAME_CN { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public string EBPL_IS_ABLE { get; set; }

        /// <summary>
        /// 父项代码
        /// </summary>
        public string EBPL_PARENT_PM_CODE { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string EBPL_LONGITUDE { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public string EBPL_LATITUDE { get; set; }

        public override string ToString()
        {
            return EBPL_NAME_CN;
        }

    }
}
