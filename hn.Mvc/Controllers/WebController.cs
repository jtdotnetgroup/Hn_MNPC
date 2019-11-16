using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Dal;
using hn.Core.Model;
using hn.Core.Bll;
using hn.Common;
using hn.Core;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System.Text;
using System.IO;

namespace hn.Mvc.Controllers
{
    public class WebController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Message()
        {
            string id = Request["id"];

            TB_MessageDal.Instance.UpdateWhatWhere(new { FState = 1 }, new { FID = id });

            TB_MESSAGEMODEL model = TB_MessageDal.Instance.Get(id);

            TB_SahowListMessageModel Model = new TB_SahowListMessageModel();

            Model.FID = model.FID;
            Model.FDate = model.FDATE;
            Model.FTitle = model.FTITLE;
            Model.FSenderID = model.FSENDERID;
            Model.FReceiverID = model.FID;
            Model.FState = model.FSTATE;
            Model.FContent = Encoding.UTF8.GetString(model.FCONTENT);
            Model.FType = model.FTYPE;
            Model.FSubType = model.FSUBTYPE;
            Model.FBillNo = model.FBILLNO;

            return View(Model);
        }

        public ActionResult Protocol()
        {
            return View();
        }

        public ActionResult Guide()
        {
            return View();
        }

        public ActionResult Ticket()
        {
            return View();
        }

        public string GetClientVer()
        {
            return JSONhelper.ToJson(
                new ResultClass()
                {
                    errCode = 0,
                    data = new VerModel()
                    {
                        VerCode = 37,
                        VerName = "v1.0.36",
                        IsMustUpdate = false
                    }
                }
               );
        }

        public string GetClientUpdateFiles()
        {
            List<string> list = new List<string>();
            var files = Directory.GetFiles(Server.MapPath("~/ClientUpdate"));

            foreach(string file in files)
            {
                list.Add(string.Format("http://{0}//ClientUpdate//{1}", Request.Url.Authority, Path.GetFileName(file)));
            }

            return JSONhelper.ToJson(new ResultClass()
            {
                errCode = 0,
                data = list
            });
        }


        public class VerModel
        {
            /// <summary>
            /// 内部版本号
            /// </summary>
            public int VerCode { get; set; }

            /// <summary>
            /// 版本号
            /// </summary>
            public string VerName { get; set; }

            /// <summary>
            /// 是否强制更新
            /// </summary>
            public bool IsMustUpdate { get; set; }
        }

        public class ResultClass
        {
            public int errCode;
            public string errMsg;
            public object data;
        }
    }
}
