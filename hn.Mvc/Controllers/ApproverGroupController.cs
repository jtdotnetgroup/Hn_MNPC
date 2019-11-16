using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Bll;
using hn.Core.Model;
using hn.Common;
using hn.Core;
using Omu.ValueInjecter;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using hn.Core.Dal;
using hn.DataAccess.Bll;
using hn.DataAccess.model;
using hn.DataAccess.bll;
using hn.Mvc.Models;
using hn.DataAccess.dal;

namespace hn.Mvc.Controllers
{
    public class ApproverGroupController : BaseController
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

                return TB_REVIEWTEAMBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
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

                //var rpm = GetRpm(context);

                TB_REVIEWTEAMModel model = new TB_REVIEWTEAMModel();
                model.FNAME = context["FNAME"];
                model.FBILL_TYPE = context["FBILL_TYPE"].ToInt();
                if (string.IsNullOrEmpty(model.FNAME))
                {
                    return JSONhelper.ToJson(new { message = "名称不能为空！", errCode = -1 });
                }
                model.FREMARK = context["FREMARK"];

                string fid=TB_REVIEWTEAMDal.Instance.Insert(model);
                if (fid!="")
                {
                    return JSONhelper.ToJson(new { message= "添加审批组成功！", errCode = 0 });
                }
                else
                {
                    return JSONhelper.ToJson(new { message = "添加审批组失败！", errCode = -1 });
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

                string fid = context["FID"];
                TB_REVIEWTEAMModel old = TB_REVIEWTEAMBll.Instance.GetByID(fid);

                if (old==null)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "保存失败！" });
                }
                old.FNAME = context["FNAME"];
                old.FREMARK = context["FREMARK"];
                old.FBILL_TYPE = context["FBILL_TYPE"].ToInt();
                int a=TB_REVIEWTEAMBll.Instance.Update(old);
                if (a>0)
                {
                    return JSONhelper.ToJson(new { errCode = 0});
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
                TB_REVIEWTEAMModel old = TB_REVIEWTEAMBll.Instance.GetByID(rpm.FID);
                if (old==null)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "删除失败！" });
                }
                int a = TB_REVIEWTEAMBll.Instance.DeleteByID(rpm.FID);
                
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
        private RequestParamModel<TB_REVIEWTEAMModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_REVIEWTEAMModel>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_REVIEWTEAMModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
