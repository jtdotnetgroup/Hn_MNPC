using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core;
using hn.Core.Model;
using hn.Common;
using Omu.ValueInjecter;
using hn.Core.Bll;
using hn.Core.Dal;

namespace hn.Mvc.Controllers
{
    public class DictController : BaseController
    {
        //
        // GET: /Dict/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();
            return View();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var categoryId = PublicMethod.GetString(rpm.Request("categoryId"));

            return DicBll.Instance.GetDicListBy(categoryId);
        }

        public ActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public string Category(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            return DicBll.Instance.DicCategoryJson();
        }

        [HttpPost]
        public string AddCategory(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();
            var rpm = GetRpm(context);

            var dc = new SysDics
            {
                FClassCode = rpm.Request("FClassCode"),
                FClassName = rpm.Request("FClassName"),
                FCreateTime = DateTime.Now,
                FUpdateTime = DateTime.Now,
                FCreatorID = SysVisitor.Instance.UserId,
                FUpdaterID = SysVisitor.Instance.UserId,
                //Sortnum = PublicMethod.GetInt(rpm.Request("sortnum")),
                FRemark = rpm.Request("FRemark")
            };

            string k = SysDicsDal.Instance.Insert(dc);
            var msg = "添加成功。";
            if (k == "")
                msg = "添加失败。";
            return new JsonMessage { Success = true, Data = k.ToString(), Message = msg }.ToString();
        }

        [HttpPost]
        public string EditCategory(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();
            var rpm = GetRpm(context);

            var dc = new SysDics
            {
                FID = PublicMethod.GetString(rpm.Request("FID")),
                FClassCode = rpm.Request("FClassCode"),
                FClassName = rpm.Request("FClassName"),
                FUpdateTime = DateTime.Now,
                FUpdaterID = SysVisitor.Instance.UserId,
                //Sortnum = PublicMethod.GetInt(rpm.Request("sortnum")),
                FRemark = rpm.Request("FRemark")
            };

            int k = SysDicsDal.Instance.Update(dc);
            var msg = "编辑成功。";
            if (k <= 0)
                msg = "编辑失败。";
            return new JsonMessage { Success = true, Data = k.ToString(), Message = msg }.ToString();
        }

        [HttpPost]
        public string DelCategory(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();
            var rpm = GetRpm(context);

            var cateId = PublicMethod.GetString(rpm.Request("cateId"));

            int k = SysDicsDal.Instance.Delete(cateId);
            var msg = "删除成功。";
            if (k <= 0)
                msg = "删除失败。";
            return new JsonMessage { Success = true, Data = k.ToString(), Message = msg }.ToString();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var k = DicBll.Instance.Add(rpm.Entity);
            return new JsonMessage { Success = k != "", Data = k.ToString(), Message = (k != "" ? "添加成功！" : "字典编码已存在,请更改编码。") }.ToString();
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            if (rpm.FID == rpm.Entity.ParentId)
            {
                return new JsonMessage { Success = false, Data = "0", Message = "上级字典不能与当前字典相同！" }.ToString();
            }

            Dic d = new Dic();
            d.InjectFrom(rpm.Entity);
            d.FID = rpm.FID;
            var k = DicBll.Instance.Edit(d);
            return new JsonMessage { Success = k > 0, Data = k.ToString(), Message = (k > 0 ? "编辑成功！" : "字典编码已存在,请更改编码。") }.ToString();
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var k = DicBll.Instance.Delete(rpm.FID);
            var msg = "删除成功。";

            switch (k)
            {
                case 0:
                    msg = "参数错误！";
                    break;
                case 2:
                    msg = "请先删除子字典数据。";
                    break;
            }

            return new JsonMessage { Success = k == 1, Data = k.ToString(), Message = msg }.ToString();
        }

        private RequestParamModel<Dic> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<Dic>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Dic>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        [HttpPost]
        public string GetDic(FormCollection collection)
        {

            string categoryId = Request["categoryId"];
            if (categoryId == "")
            {
                categoryId = collection["categoryId"];
            }

            return DicBll.Instance.GetDicListByCode(categoryId);
        }

    }
}
