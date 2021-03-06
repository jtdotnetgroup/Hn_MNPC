﻿using System;
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

namespace hn.Mvc.Controllers
{
    public class MeasuringGroupController : BaseController
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

                return TB_UnitGroupBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
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
        public ActionResult MeasuringGroup()
        {
            return View();
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            int id = Request["id"].ToInt();
            //TB_MeasuringGroupModel model = TB_MeasuringGroupDal.Instance.Get(id);

            return View();
        }

        ///// <summary>
        ///// 新增保存
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public string Add(FormCollection context)
        //{
        //    try
        //    {
        //        UserBll.Instance.CheckUserOnlingState();

        //        //var rpm = GetRpm(context);

        //        //var r = new TB_MeasuringGroupModel();
        //        //r.InjectFrom(rpm.Entity);
        //        //string result;
        //        //if (!CheckData(r, out result))
        //        //{
        //        //    return JSONhelper.ToJson(new { errCode = -1, errMsg = result });
        //        //}

        //        //TB_MeasuringGroupDal.Instance.Insert(rpm.Entity);

        //        return JSONhelper.ToJson(new { errCode = 0 });

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(ex);
        //        return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
        //    }
        //}

        ///// <summary>
        ///// 编辑保存
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public string Edit(FormCollection context)
        //{
        //    try
        //    {
        //        UserBll.Instance.CheckUserOnlingState();

        //        var rpm = GetRpm(context);

        //        TB_UnitGroupModel model = new TB_UnitGroupModel();

        //        model.InjectFrom(rpm.Entity);

        //        model.FUPDATETIME = DateTime.Now;

        //        JsonMessage message = TB_UnitGroupBll.Instance.EditBT_UnitGroupModel(model);

        //        return JSONhelper.ToJson(new { errCode = 0, errMsg = message.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(ex);
        //        return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
        //    }
        //}

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

                JsonMessage message = TB_UnitGroupBll.Instance.DelectBT_UnitGroupModel(rpm.FID);

                return JSONhelper.ToJson(new { errCode = 0, errMsg = message.Message });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        ///// <summary>
        ///// 新增保存按钮
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public string Save(FormCollection context)
        //{
        //    try
        //    {
        //        var rpm = GetRpm(context);

        //        TB_UnitGroupModel model = new TB_UnitGroupModel();

        //        model.InjectFrom(rpm.Entity);

        //        //搜索数据库返回列表刷新
        //        model.FUPDATETIME = DateTime.Now;

        //        JsonMessage message = TB_UnitGroupBll.Instance.AddNewBT_UnitGroupModel(model);

        //        return JSONhelper.ToJson(new { errCode = 0, errMsg = message.Message });
        //    }
        //    catch (Exception ex)
        //    {

        //        return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
        //    }
        //}

        [HttpPost]
        public JsonResult Save(TB_UnitGroupModel model)
        {
            string result = TB_UnitGroupBll.Instance.Save(model);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("保存完成！");
            }
            else
            {
                return JsonResultHelper.ToFailed(result);
            }
        }


        /// <summary>
        /// 数据模型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private RequestParamModel<TB_UnitGroupModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_UnitGroupModel>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_UnitGroupModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }
    }
}
