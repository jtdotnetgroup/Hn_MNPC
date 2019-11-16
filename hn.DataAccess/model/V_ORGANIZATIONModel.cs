using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using hn.Core.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Model
{
    [TableName("V_ORGANIZATION")]
    [Description("组织架构")]
    public class V_ORGANIZATIONModel
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string FPARENTALID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int FSTATUS { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DbField(false)]
        public virtual string FSTATUSNAME
        {
            get
            {
                return FSTATUS == 1 ? "启用" : "禁用";
            }
        }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string FORGCODE { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string FORGNAME { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string FHEADER { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string FHEADERNAME { get; set; }

        /// <summary>
        /// 组织类型
        /// </summary>
        public int FTYPE { get; set; }

        /// <summary>
        /// 组织类型
        /// </summary>
        [DbField(false)]
        public string FTYPENAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.TB_ORGANIZATION_FTYPE), FTYPE);
            }
        }

        public string FATTRIBUTE1 { get; set; }
        public string FATTRIBUTE2 { get; set; }
        public string FATTRIBUTE3 { get; set; }
        public string FATTRIBUTE4 { get; set; }
        public string FATTRIBUTE5 { get; set; }
        public string FATTRIBUTE6 { get; set; }
        public string FATTRIBUTE7 { get; set; }
        public string FATTRIBUTE8 { get; set; }
        public string FATTRIBUTE9 { get; set; }
        public string FATTRIBUTE10 { get; set; }

        ///// <summary>
        ///// 默认发货地点
        ///// </summary>
        //public string FDEFAULTADDR { get; set; }

        ///// <summary>
        ///// 仓库代码
        ///// </summary>
        //public string FSTOCKCODE { get; set; }

        ///// <summary>
        ///// 仓库名称
        ///// </summary>
        //public string FSTOCKNAME { get; set; }


        //[DbField(false)]
        //public IEnumerable<V_ORGANIZATIONModel> children
        //{
        //    get { return V_ORGANIZATIONDal.Instance.GetChildren(FID); }
        //}
    }
}
