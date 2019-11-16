using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hn.Mvc.Models
{
    public class ComboBox
    {
        public virtual string id { get; set; }

        public virtual string text { get; set; }
    }

    public class EnumHelper
    {
        /// <summary>
        /// 商品状态
        /// </summary>
        public enum ProductStatus
        {
            禁用 = 0,
            启用 = 1
        }
    }
}