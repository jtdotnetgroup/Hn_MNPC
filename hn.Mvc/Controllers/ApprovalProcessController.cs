using System;
using System.Web.Mvc;
using hn.Core.Bll;
using hn.Common;
using hn.Core;
using hn.DataAccess.model;
using hn.DataAccess.bll;
using hn.DataAccess.dal;
using Omu.ValueInjecter;

namespace hn.Mvc.Controllers
{
    public class ApprovalProcessController : BaseController
    {
        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //工具栏
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }


        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string List(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);

                var a= V_ROUTINGBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
                return a;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "";
            }
        }

        /// <summary>
        /// 添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edits()
        {
            return View();
        }

        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Add(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);

                string fid = TB_ROUTINGDal.Instance.Insert(rpm.Entity);
                if (fid != "")
                {
                    return JSONhelper.ToJson(new { message = "保存成功！", errCode = 0 });
                }
                else
                {
                    return JSONhelper.ToJson(new { message = "保存失败！", errCode = -1 });
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, message = ex.Message });
            }
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Edit(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);
                TB_ROUTINGModel model = TB_ROUTINGDal.Instance.Get(rpm.FID);
                TB_ROUTINGModel d = new TB_ROUTINGModel();
                d.InjectFrom(rpm.Entity);
                d.FID = model.FID;

                int a = TB_ROUTINGBll.Instance.Update(d);
                if (a > 0)
                {
                    return JSONhelper.ToJson(new { errCode = 0 });
                }
                else
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "保存失败！" });
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        /// <summary>
        /// 删除处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);
                int a = TB_ROUTINGDal.Instance.Delete(rpm.FID);

                if (a > 0)
                {
                    return JSONhelper.ToJson(new { errCode = 0 });
                }
                else
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "删除失败！" });
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }


        /// <summary>
        /// 数据模型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private RequestParamModel<TB_ROUTINGModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_ROUTINGModel>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_ROUTINGModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
