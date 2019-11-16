using hn.Common;
using hn.DataAccess.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    /// <summary>
    /// 选择弹窗数据
    /// </summary>
    public class ChooseDialogController : Controller
    {
        /// <summary>
        /// 品牌--分页
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Brand(int page, int pageSize)
        {

            return TB_BrandBll.Instance.GetEasyUIJson(page, pageSize);
        }

        /// <summary>
        /// 商品类别--树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string ProductCategory(int page, int pageSize)
        {
            return CategoryBll.Instance.GetCategoryTreegridData();
        }
    }
}