using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core;
using hn.Core.Model;
using hn.Core.Bll;
using hn.Common;
using hn.Common.Data;

namespace hn.Mvc
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Action执行前判断
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string msg;
            if (SysVisitor.Instance.IsGuest)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new HttpStatusCodeResult(401, "Loginout");
                }
                else
                {
                    filterContext.Result = Content(string.Concat("<script>", "", "top.location='", Url.Content("~/Login"), "'</script>"), "text/html");
                }
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 创建页面工具栏
        /// </summary>
        /// <returns></returns>
        public string BuildToolbar()
        {
            return UserBll.Instance.PageButtons(SysVisitor.Instance.UserId, PublicMethod.GetString(Request["navid"]));
        }

        public string GetBillNo(string billtype = null)
        {
            return JSONhelper.ToJson(DbUtils.GetBillNo(billtype, "年月"));
        }
    }
}