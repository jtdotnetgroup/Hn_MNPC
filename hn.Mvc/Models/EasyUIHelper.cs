using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Models
{
    public class EasyUI<T>
    {
        public virtual int total { get; set; }

        public virtual IEnumerable<T> rows { get; set; }
    }

    public class EasyUIHelper<T>
    {
        public static EasyUI<T> Easy(IEnumerable<T> entity, int totalSize)
        {
            EasyUI<T> easyUI = new EasyUI<T>();
            easyUI.total = totalSize;
            easyUI.rows = entity;

            return easyUI;
        }

        public static JsonResult ReturnEasyUI(IEnumerable<T> entity, int totalSize)
        {
            EasyUI<T> easyUI = new EasyUI<T>();
            easyUI.total = totalSize;
            easyUI.rows = entity;

            var jsonResult = new JsonResult();

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
            jsonResult.Data = easyUI;

            return jsonResult;
        }
    }
}