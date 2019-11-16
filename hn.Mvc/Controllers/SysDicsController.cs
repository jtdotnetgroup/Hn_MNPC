using hn.Common;
using hn.Core;
using hn.Core.Bll;
using hn.Core.Model;
using hn.DataAccess.Bll;
using hn.DataAccess.Model;
using hn.Mvc.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class SysDicsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string List(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var categoryId = PublicMethod.GetString(rpm.Request("FClassIdent"));

            return SysDicsBll.Instance.GetDicListBy(categoryId);
        }

        [HttpPost]
        public JsonResult GetProductStatusForCombobox(int code = 0)
        {
            IList<ComboBox> combobox = new List<ComboBox>();
            foreach (EnumHelper.ProductStatus productStatus in Enum.GetValues(typeof(EnumHelper.ProductStatus)))
            {
                combobox.Add(new ComboBox() { id = ((int)productStatus).ToString(), text = productStatus.ToString() });
            }

            return Json(combobox, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public string Category(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();
            
            return SysDicsBll.Instance.DicCategoryJson("0");
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
                FClassIdent = "0",
                FCreateTime = DateTime.Now,
                FUpdateTime = DateTime.Now,
                FCreatorID = SysVisitor.Instance.UserId,
                FUpdaterID = SysVisitor.Instance.UserId,
                //Sortnum = PublicMethod.GetInt(rpm.Request("sortnum")),
                FRemark = rpm.Request("FRemark")
            };

            string k = SysDicsBll.Instance.Add(dc);
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

            int k = SysDicsBll.Instance.Update(dc);
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

            int k = SysDicsBll.Instance.Delete(cateId);
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

            var rpm = GetRpm1(context);

            var k = "";// Sys_SubDicsBll.Instance.Add(rpm.Entity);
            return new JsonMessage { Success = k != "", Data = k.ToString(), Message = (k != "" ? "添加成功！" : "字典编码已存在,请更改编码。") }.ToString();
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            //if (rpm.FID == rpm.Entity.ParentId)
            //{
            //    return new JsonMessage { Success = false, Data = "0", Message = "上级字典不能与当前字典相同！" }.ToString();
            //}

            SysDics d = new SysDics();
            d.InjectFrom(rpm.Entity);
            d.FID = rpm.FID;
            var k = SysDicsBll.Instance.Update(d);
            return new JsonMessage { Success = k > 0, Data = k.ToString(), Message = (k > 0 ? "编辑成功！" : "字典编码已存在,请更改编码。") }.ToString();
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var k = SysDicsBll.Instance.Delete(rpm.FID);
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

        private RequestParamModel<SYS_SUBDICSMODEL> GetRpm1(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<SYS_SUBDICSMODEL>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<SYS_SUBDICSMODEL>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        private RequestParamModel<SYS_DICSMODEL> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<SYS_DICSMODEL>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<SYS_DICSMODEL>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        [HttpPost]
        public string GetDic(FormCollection collection)
        {

            string FClassIdent = Request["FClassIdent"];
            if (string.IsNullOrEmpty(FClassIdent))
            {
                FClassIdent = collection["FClassIdent"];
            }

            return SysDicsBll.Instance.GetDicListByCode(FClassIdent);
        }

    }
}
