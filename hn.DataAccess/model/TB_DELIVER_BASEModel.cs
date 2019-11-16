using hn.Common.Data;
using System.ComponentModel;

namespace hn.DataAccess.model
{
    [TableName("TB_DELIVER_BASE")]
    [Description("厂家发货基地")]
    public class TB_DELIVER_BASEModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string FBRAND { get; set; }

        /// <summary>
        /// 发货基地
        /// </summary>
        public string FBASEA_NAME { get; set; }

        /// <summary>
        /// 省级ID
        /// </summary>
        public string FPROVINCEID { get; set; }

        /// <summary>
        /// 市级ID
        /// </summary>
        public string FCITYID { get; set; }

        /// <summary>
        /// 区县级ID
        /// </summary>
        public string FDISTRICTID { get; set; }

        /// <summary>
        /// 镇&街道ID
        /// </summary>
        public string FCOUNTYID { get; set; }

        /// <summary>
        /// 详细发货地址
        /// </summary>
        public string FADDRESS { get; set; }

        [DbField(false)]
        public string FBRANDNAME { get; set; }
        [DbField(false)]
        public string FPROVINCENAME { get; set; }
        [DbField(false)]
        public string FCITYNAME { get; set; }
        [DbField(false)]
        public string FDISTRICTNAME { get; set; }
        [DbField(false)]
        public string FCOUNTYNAME { get; set; }
    }
}
