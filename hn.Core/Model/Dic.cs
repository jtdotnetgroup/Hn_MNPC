using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Core.Dal;
using hn.Common.Data;

namespace hn.Core.Model
{
    [TableName("Sys_Dics")]
    [Description("数据字典")]
    public class Dic
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Description("主键")]
        public string FID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public string Title { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Description("编码")]
        public string Code { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        [Description("类别ID")]
        public string CategoryId { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        [Description("类别ID")]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        [Description("上级Id")]
        public string ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Description("排序")]
        public int Sortnum { get; set; }
        /// <summary>
        /// 描述说明
        /// </summary>
        [Description("描述")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Description("状态")]
        public int Status { get; set; }


        [DbField(false)]
        public SysDics Category { get; set; }

        [DbField(false)]
        public IEnumerable<Dic> children
        {
            get { return DicDal.Instance.GetListBy(CategoryId,FID); }
        }


    }
}
