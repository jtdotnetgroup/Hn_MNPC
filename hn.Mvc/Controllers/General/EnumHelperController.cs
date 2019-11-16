using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.DataAccess.Model;
using hn.DataAccess.Bll;
using hn.Common;
using hn.DataAccess;

namespace hn.Mvc.Controllers
{
    public class EnumHelperController : Controller
    {

        ///// <summary>
        ///// 请购计划业务类型
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public string ICPRBILL_FTRANSTYPE()
        //{
        //    return JSONhelper.ToJson(GetEmumList(typeof(Constant.ICPRBILL_FTRANSTYPE)));
        //}

        /// <summary>
        /// 请购计划状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string ICPRBILL_FSTATUS()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.BILL_FSTATUS)));
        }


        /// <summary>
        /// 组织架构类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string TB_ORGANIZATION_FTYPE()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.TB_ORGANIZATION_FTYPE)));
        }


        /// <summary>
        /// 遍历枚举列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<object> GetEmumList(Type type)
        {
            List<object> list = new List<object>();

            foreach (var value in Enum.GetValues(type))
            {
                list.Add(new
                {
                    id = Convert.ToInt32(value),
                    text = value.ToString(),
                });
            }

            return list;
        }
    }
}
