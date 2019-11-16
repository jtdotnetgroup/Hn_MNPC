using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Common;
using hn.Core.Bll;
using hn.Core;
using hn.Core.Model;
using Omu.ValueInjecter;
using hn.DataAccess.Bll;
using System.Data;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using Newtonsoft.Json.Linq;
using hn.DataAccess.model;
using hn.DataAccess.bll;
using hn.DataAccess.dal;
using hn.Mvc.Models;
using hn.DataAccess;

namespace hn.Mvc.Controllers
{
    public class MessageController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();
            return View();
        }

        public ActionResult Send()
        {
            return View();
        }


        [HttpPost]
        public string GetTreeNode(FormCollection context)
        {
            try
            {
                //sys_users
                TB_MessageBll dal = new TB_MessageBll();
                string strTreeNode = JSONhelper.ToJson(dal.GetOrganizationTreegridData().ToList());
                return strTreeNode.Replace("FID", "id").Replace("UserName", "text").Replace("RoleName", "text");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                return ex.Message;
            }

        }


        [HttpPost]
        public string Delete(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                //var rpm = GetRpm(context);

                string ids = context["json"];

                TB_MessageBll.Instance.DelectBT_MessageModel(ids);

                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });

            }
            catch (Exception ex)
            {
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }


        [HttpPost]
        public string List(FormCollection context)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                var rpm = GetRpm(context);

                return TB_MessageBll.Instance.GetJson(rpm.Pageindex, rpm.Pagesize, rpm.Filter, rpm.Sort, rpm.Order);

            }
            catch (Exception ex)
            {
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        [HttpPost]
        public string onSearch(FormCollection context)
        {
            try
            {
                var json = context.ToString();
                var rpm = GetRpm(context);

                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }



        [HttpPost]
        public string SendMessage(FormCollection context)
        {
            try
            {
                //用户ID
                string strUserID = SysVisitor.Instance.UserId;

                TB_MESSAGEMODEL mode;
                var json = context["json"];
                var strJson = HttpUtility.UrlDecode(json);
                var jObject = JObject.Parse(strJson);

                string ids = jObject["ids"].ToString();
                string names = jObject["names"].ToString();
                string Title = jObject["title"].ToString();
                string content = jObject["content"].ToString();



                string[] arrayids = ids.Split(',');
                string[] arraynames = names.Split(',');

                string message = "";

                for (int i = 0; i < arrayids.Length; i++)
                {
                    mode = new TB_MESSAGEMODEL();
                    mode.FDATE = DateTime.Now;
                    mode.FID = "0";
                    mode.FRECEIVERID = arrayids[i];
                    mode.FSENDERID = strUserID;
                    mode.FTITLE = Title;
                    mode.FCONTENT = System.Text.Encoding.UTF8.GetBytes(content);
                    mode.FSTATE = 0;
                    mode.FSUBTYPE = 0;
                    mode.FTYPE = 0;
                    mode.FBILLNO = "";
                    message += TB_MessageBll.Instance.AddNewBT_MessageModel(mode, i);
                }

                if (message.Equals(""))
                {
                    message = "添加失败";
                }

                return JSONhelper.ToJson(new { errCode = 0, errMsg = message });
            }
            catch (Exception ex)
            {
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }



        private RequestParamModel<TB_MESSAGEMODEL> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<TB_MESSAGEMODEL>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<TB_MESSAGEMODEL>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        [HttpPost]
        public string TreeJson(FormCollection context)
        {
            return "";// DriverBll.Instance.TreeJson();
        }

        [HttpPost]
        public string Search(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = GetRpm(context);

            return MessageBll.Instance.GetListJson(rpm.Pageindex, rpm.Pagesize, "", rpm.Filter, rpm.Sort, rpm.Order);
        }

        //private RequestParamModel<MessageModel> GetRpm(FormCollection context)
        //{
        //    var json = context["json"];
        //    var rpm = new RequestParamModel<MessageModel>(context) { CurrentContext = context, Action = Request["action"] };
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        rpm = JSONhelper.ConvertToObject<RequestParamModel<MessageModel>>(json);
        //        rpm.CurrentContext = context;
        //    }

        //    return rpm;
        //}

        //public ActionResult Send()
        //{
        //    ViewBag.ToolBar = BuildToolbar();
        //    return View();
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public string Send(FormCollection context)
        //{
        //    try
        //    {
        //        var ids = Request["ids"];
        //        var title = Request["title"];
        //        var content = Request["content"];
        //        if (string.IsNullOrEmpty(ids))
        //        {
        //            try
        //            {
        //                List<AppUserModel> list = AppUserDal.Instance.GetWhereStr(" and MASTER_ID IS NOT NULL").ToList();
        //                foreach (AppUserModel userModel in list)
        //                {
        //                    MessageModel messageModel = new MessageModel();
        //                    messageModel.RECEIVE_ID = 0;// PublicMethod.GetDecimal(id);
        //                                                // model.CONTENT = content;
        //                    messageModel.HTML_CONTENT = content;
        //                    messageModel.IS_RREAD = 0;
        //                    messageModel.SEND_TIME = DateTime.Now;
        //                    messageModel.TITLE = title;
        //                    messageModel.USER_ID = userModel.FID;
        //                    MessageDal.Instance.Insert(messageModel);
        //                }

        //                JPushClient client = new JPushClient(SysVisitor.Instance.PushAppKey, SysVisitor.Instance.PushMasterSecret);

        //                PushPayload pushPayload = new PushPayload();
        //                pushPayload.platform = Platform.all();
        //                pushPayload.audience = Audience.all();
        //                pushPayload.notification = new Notification().setAlert(title);

        //                client.SendPush(pushPayload);
        //            }
        //            catch (Exception ex)
        //            {
        //                LogHelper.WriteLog(string.Format("Error: appKey={0},masterSecret={1},phone={2},title={3},msg={4}", SysVisitor.Instance.PushAppKey, SysVisitor.Instance.PushMasterSecret, "全体", title, ex.Message));
        //            }
        //        }
        //        else
        //        {

        //            string[] array = ids.Split(',');
        //            foreach (string id in array)
        //            {
        //                MessageModel model = new MessageModel();
        //                model.RECEIVE_ID = 0;// PublicMethod.GetDecimal(id);
        //                                     // model.CONTENT = content;
        //                model.HTML_CONTENT = content;
        //                model.IS_RREAD = 0;
        //                model.SEND_TIME = DateTime.Now;
        //                model.TITLE = title;
        //                model.USER_ID = id;
        //                if (MessageDal.Instance.Insert(model) != "")
        //                {
        //                    OnPushMessage(id, title);
        //                }
        //            }
        //        }



        //        return JSONhelper.ToJson("发送成功！");
        //    }
        //    catch (Exception ex)
        //    {
        //        return JSONhelper.ToJson(ex.Message);
        //    }

        //}

        private void OnPushMessage(string id, string title)
        {
            //DriverModel model = DriverDal.Instance.GetByID(id);
            AppUserModel model = AppUserDal.Instance.Get(id);


            if (model != null)
            {
                try
                {
                    JPushClient client = new JPushClient(SysVisitor.Instance.PushAppKey, SysVisitor.Instance.PushMasterSecret);

                    PushPayload pushPayload = new PushPayload();
                    pushPayload.platform = Platform.all();
                    pushPayload.audience = Audience.s_alias(model.PHONE);
                    pushPayload.notification = new Notification().setAlert(title);

                    client.SendPush(pushPayload);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(string.Format("Error: appKey={0},masterSecret={1},phone={2},title={3},msg={4}", SysVisitor.Instance.PushAppKey, SysVisitor.Instance.PushMasterSecret, model.PHONE, title, ex.Message));
                }
            }


        }

        //public string Delete()
        //{
        //    string[] ids = Request["id"].Split(',');
        //    foreach (string id in ids)
        //    {
        //        MessageDal.Instance.Delete(id);
        //    }
        //
        //    return JSONhelper.ToJson("删除成功！");
        //}


        [HttpPost]
        public string Data(int page = 1, int rows = 15, string startDate = null, string endDate = null, string FRECEIVERID = null)
        {
            return V_MESSAGEBll.Instance.GetEasyUIJson(page, rows, startDate, endDate, FRECEIVERID);
        }

        [HttpPost]
        public JsonResult Delete(string FIDLIST)
        {
            TB_MessageBll.Instance.BatchDelete(FIDLIST);

            return JsonResultHelper.ToSuccess("删除完成！");
        }


        [HttpPost]
        [ValidateInput(false)]

        public JsonResult Add(string FReceiverIDList = null, string title = null, string content = null)
        {

            string result = MessageBll.Instance.BatchSend(FReceiverIDList, title, content);

            if (result.IsNullOrEmpty())
            {
                return JsonResultHelper.ToSuccess("发送完成！");
            }
            else
            {
                return JsonResultHelper.ToSuccess(result);
            }

        }
    }
}

