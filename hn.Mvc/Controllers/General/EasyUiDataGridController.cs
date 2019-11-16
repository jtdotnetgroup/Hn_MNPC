using hn.Common;
using hn.DataAccess.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class EasyUiDataGridController : Controller
    {
        //[HttpPost]
        //public string SubDicCategory(string parentID)
        //{
        //    List<object> list = new List<object>();

        //    foreach (var item in Sys_SubDicsBll.Instance.GetBy(parentID))
        //    {
        //        list.Add(new
        //        {
        //            id = item.FID,
        //            text = item.FName,
        //            iconCls = "icon-bullet_blue",
        //        });
        //    }

        //    return JSONhelper.ToJson(list);
        //}

        [HttpPost]
        public string SubDicCategory(int page, int rows, string parentID = null, string parentCode = null)
        {
            return SYS_SUBDICSBLL.Instance.GetEasyUIJson(page, rows, parentID, parentCode);
        }
    }
}