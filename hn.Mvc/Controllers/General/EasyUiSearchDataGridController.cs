using hn.Common;
using hn.DataAccess.bll;
using hn.DataAccess.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class EasyUiSearchDataGridController : Controller
    {
        [HttpPost]
        public string SYS_User(int page, int rows,string keywords = null)
        {
            return Core.Bll.UserBll.Instance.GetEasyUIJson(page, rows, keywords);
        }

        /// <summary>
        /// 请购计划
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="FOrgID">销区ID</param>
        /// <param name="FStatus">状态</param>
        /// <param name="approved">是否审核通过</param>
        /// <returns></returns>
        [HttpPost]
        public string ICPRBILL(
            int page = 1,
            int rows = 15, 
            string startDate = null, 
            string endDate = null, 
            string FOrgID = null, 
            int FStatus = 0, 
            string FCLASSAREA2NAME = null,
            string FTYPEID=null, 
            string brandid=null, 
            bool approved = false)
        {
            return V_ICPRBILLBLL.Instance.GetEasyUIJson(page, rows, startDate, endDate, FOrgID, FStatus, brandid, FCLASSAREA2NAME, FTYPEID,approved);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="FOrgID">销区ID</param>
        /// <param name="FStatus">状态</param>
        /// <param name="approved">是否审核通过</param>
        /// <returns></returns>
        [HttpPost]
        public string ICPOHISTORY(int page = 1, int rows = 15, string startDate = null, string endDate = null, string FOrgID = null, int FStatus = 0, bool approved = false)
        {
            return null;
            //return V_ICPOHISTORY.Instance.GetEasyUIJson(page, rows, startDate, endDate, FOrgID, FStatus, approved);
        }

        #region 计量单位

        [HttpPost]
        public string TB_UNIT(int page = 1, int rows = 15)
        {
            return TB_UnitBll.Instance.GetEasyUIJson(page, rows);
        }

        #endregion
    }
}