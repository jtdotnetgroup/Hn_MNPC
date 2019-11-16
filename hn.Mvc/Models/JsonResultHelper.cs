using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Models
{
    public class JsonResultHelper
    {

        public static JsonResult ToSuccess(string message, object data = null)
        {
            return returnJsonResult(true, message, data);
        }

        public static JsonResult ToFailed(string message, object data = null)
        {
            return returnJsonResult(false, message, data);
        }

        private static JsonResult returnJsonResult(bool success, string message = null, object data = null)
        {
            var jsonResult = new JsonResult();

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
            jsonResult.Data = new
            {
                Success = success,
                Message = message,
                Data = data
            };

            return jsonResult;
        }

    }
}