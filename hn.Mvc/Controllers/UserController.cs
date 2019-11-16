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
using hn.DataAccess.model;
using hn.DataAccess.dal;
using hn.Mvc.Models;

namespace hn.Mvc.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

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

            return UserBll.Instance.GetJsonData(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);
        }

        [HttpPost]
        public string All(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return UserBll.Instance.GetJsonData(rpm.Pageindex, 10000, rpm.Filter, rpm.Sort, rpm.Order);
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var roleIds = rpm.Request("roles");

            return UserBll.Instance.AddUser(rpm.Entity, roleIds);
        }

        [HttpPost]
        public string Edit(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var r = new User();
            r.InjectFrom(rpm.Entity);
            r.FID = rpm.FID;
            return UserBll.Instance.EditUser(r);
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return UserBll.Instance.DeleteUser(rpm.FID);
        }

        public ActionResult Password()
        {
            return View();
        }

        [HttpPost]
        public string Password(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return UserBll.Instance.EditPassword(rpm.FID, rpm.Request("password")).ToString();
        }

        [HttpPost]
        public string Password2(FormCollection context)
        {

            return UserBll.Instance.EditPassword(SysVisitor.Instance.CurrentUser.FID, context["old"], context["new"]).ToString();
        }

        [HttpPost]
        public string SetAttr(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var u = UserBll.Instance.GetUser(rpm.FID);
            if (u != null)
            {
                if (rpm.Action == "isadmin")
                {
                    var isamdin = rpm.Request("val");
                    u.IsAdmin =PublicMethod.GetDecimal(isamdin);
                }
                else if (rpm.Action == "isdisabled")
                {

                    var isdisabled = rpm.Request("val");
                    u.IsDisabled = PublicMethod.GetDecimal(isdisabled);
                }
                return UserBll.Instance.EditUser(u);
            }
            else
            {
                return "0";
            }
        }
       
        [HttpPost]
        public string SetRoles(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var rolse = rpm.Request("roles");
            return UserBll.Instance.AddUserToRoles(rpm.FID, rolse).ToString();
        }


        
        public string RolesByID()
        {
            UserBll.Instance.CheckUserOnlingState();

            //var rpm = GetRpm(context);

            return UserBll.Instance.GetRolesBy(PublicMethod.GetString(Request["id"]));
        }

        [HttpPost]
        public string Menus(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return UserBll.Instance.GetNavBtnsJson(rpm.FID);
        }

        [HttpPost]
        public string Authorize(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            var data = rpm.Request("data");
            if (string.IsNullOrEmpty(data))
            {
                return "参数错误！";
            }

            return UserBll.Instance.UserAuthorize(data).ToString();
        }

        [HttpPost]
        public string SetDepartment(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var roleid = PublicMethod.GetInt(Request["FID"]);
            var deps = Request["deps"];
            return UserBll.Instance.SetDepartments(roleid, deps).ToString();
        }

        public ActionResult Roles()
        {
            return View();
        }


        public string RolesList()
        {
            UserBll.Instance.CheckUserOnlingState();

            return UserBll.Instance.GetAllRoles();
        }

        private RequestParamModel<User> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<User>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<User>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }


        public ActionResult Authorized()
        {
            return View();
        }


        [HttpPost]
        public string USERPREMISEData(int page = 1, int rows = 15, string Flag = "0", string userid = null)
        {
            return TB_USERPREMISEDal.Instance.GetJson(page, rows, userid);
        }

        [HttpPost]
        public JsonResult AuthorizedSave(string userid, string premiselist,string brandlist)
        {
            try
            {
                List<TB_USERPREMISEModel> list = JSONhelper.ConvertToObject<List<TB_USERPREMISEModel>>(premiselist);

                TB_USERPREMISEDal.Instance.DeleteWhere(new { FUSERID = userid });
                foreach (TB_USERPREMISEModel model in list)
                {
                    model.FUSERID = userid;
                    TB_USERPREMISEDal.Instance.Insert(model);
                }

                List<TB_USERBRANDModel> brands = JSONhelper.ConvertToObject<List<TB_USERBRANDModel>>(brandlist);

                TB_USERBRANDDal.Instance.DeleteWhere(new { FUSERID = userid });
                foreach (TB_USERBRANDModel model in brands)
                {
                    model.FUSERID = userid;
                    TB_USERBRANDDal.Instance.Insert(model);
                }
                

                return JsonResultHelper.ToSuccess("授权成功");
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JsonResultHelper.ToFailed(ex.Message);
            }
        }

        [HttpPost]
        public string USERBRANDData(int page = 1, int rows = 15, string Flag = "0", string userid = null)
        {
            return TB_USERBRANDDal.Instance.GetJson(page, rows, userid);
        }
              
    }
}
