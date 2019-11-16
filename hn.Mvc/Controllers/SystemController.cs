using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Bll;
using hn.Core.Model;
using hn.Core;
using hn.Common;
using hn.Core.Dal;
using System.IO;
using System.Text;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
using System.Data;
using hn.DataAccess.Bll;

namespace hn.Mvc.Controllers
{
    public class SystemController : BaseController
    {

        //
        // GET: /System/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            //SYS_PARAMSMODEL model = new SYS_PARAMSMODEL();
            //model.FKEY = "IntervalSP";
            //model.FVALUE = "888";
            ////Dictionary<string, SYS_PARAMSMODEL> dic = new Dictionary<string, SYS_PARAMSMODEL>();
            ////var list = SYS_PARAMSBLL.Instance.GetAll().ToList();
            ////foreach (var item in list)
            ////{
            ////    ViewData[item.FKEY] = item;
            ////}

            return View();
        }




        public ActionResult Log()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }



        [HttpPost]
        public string Log(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = new RequestParamModel<LogModel>(context)
            {
                CurrentContext = context,
                Action = Request["action"],
                FID = PublicMethod.GetString(Request["FID"])
            };

            return LogDal.Instance.JsonDataForEasyUIdataGrid(rpm.Pageindex, rpm.Pagesize, rpm.Filter); ;
        }

        [HttpPost]
        public string LogDetail(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = new RequestParamModel<LogModel>(context)
            {
                CurrentContext = context,
                Action = Request["action"],
                FID = PublicMethod.GetString(Request["FID"])
            };

            return JSONhelper.ToJson(LogDetailDal.Instance.GetBy(rpm.FID).ToList());
        }

        [HttpPost]
        public string LogClear(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            var rpm = new RequestParamModel<LogModel>(context)
            {
                CurrentContext = context,
                Action = Request["action"],
                FID = PublicMethod.GetString(Request["FID"])
            };

            LogBll<object> log = new LogBll<object>();
            int days = PublicMethod.GetInt(Request["days"]);
            return log.ClearLog(days);
        }

        public ActionResult Config()
        {
            return View();
        }

        public string ConfigJs()
        {
            User u = UserDal.Instance.Get(SysVisitor.Instance.UserId);
            string cj = u.ConfigJson;
            if (string.IsNullOrEmpty(cj))
                return "var sys_config ={\"theme\":{\"title\":\"默认皮肤\",\"name\":\"default\",\"selected\":true},\"showType\":\"Accordion\",\"gridRows\":20}";
            else
            {
                return "var sys_config = " + cj;
            }
        }

        [HttpPost]
        public string Config(FormCollection context)
        {
            int k = UserDal.Instance.UpdateUserConfig(SysVisitor.Instance.UserId, Request["json"]);
            SysVisitor.Instance.CurrentUser.ConfigJson = Request["json"];
            return k.ToString();
        }

        public ActionResult DataBase()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        [HttpPost]
        public string DataBase(FormCollection context)
        {
            string path = Server.MapPath("~/dbase/");
            DirectoryInfo di = new DirectoryInfo(path);
            var query = from n in di.GetFiles()
                        orderby n.CreationTime descending
                        select
                            new
                            {
                                FileName = n.Name,
                                FileSize = ((float)n.Length / 1024 / 1024).ToString("N3") + " M",
                                CreateDate = n.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")
                            };

            return JSONhelper.ToJson(query);
        }

        [HttpPost]
        public string BackUp(FormCollection context)
        {
            string dbname = ConfigHelper.GetValue("dbname");
            string backupName = StringHelper.CreateIDCode();
            string savePath = Server.MapPath("~/dbase/");
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("");
            sql.AppendLine("USE [master];");
            sql.AppendLine("ALTER DATABASE {0} SET RECOVERY SIMPLE WITH NO_WAIT;");
            sql.AppendLine("ALTER DATABASE {0} SET RECOVERY SIMPLE;");
            sql.AppendLine("USE {0};");
            sql.AppendLine("DBCC SHRINKFILE (N'{0}_Log' , 11, TRUNCATEONLY);");
            sql.AppendLine("USE [master]; ");
            sql.AppendLine("ALTER DATABASE {0} SET RECOVERY FULL WITH NO_WAIT;");
            sql.AppendLine("ALTER DATABASE {0} SET RECOVERY FULL;");
            sql.AppendLine("USE {0};");
            sql.AppendLine("BACKUP DATABASE {0} to DISK ='{1}'");

            string backupSql = sql.ToString();// "DUMP TRANSACTION {0} WITH NO_LOG; BACKUP DATABASE {0} to DISK ='{1}' ";
            backupSql = string.Format(backupSql, dbname, savePath + backupName + ".bak");
            try
            {
                //执行备份
                hn.Common.Data.DbUtils.ExecuteNonQuery(backupSql, null);
                //addZipEntry(backupName, savePath);

                //写入操作日志
                LogBll<object> log = new LogBll<object>();
                log.AddLog("备份数据库", "数据库备份成功，文件名：" + backupName + ".bak");

                return new JsonMessage { Data = "1", Message = "数据库备份成功。", Success = true }.ToString();
            }
            catch (Exception ex)
            {
                return new JsonMessage { Data = "1", Message = ex.StackTrace, Success = false }.ToString();
            }
        }

        [HttpPost]
        public string Download(FormCollection context)
        {
            //string basepath = context.Server.MapPath("~/dbase/");
            //string filename = context.Request["n"];
            //string downpath = basepath + filename;
            //MemoryStream ms = null;
            //context.Response.ContentType = "application/octet-stream";

            //context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            //ms = new MemoryStream(File.ReadAllBytes(downpath));

            ////写入操作日志
            //LogBll<object> log = new LogBll<object>();
            //log.AddLog("下载备份数据库", "数据库备份文件下载，文件名：" + filename);

            //context.Response.Clear();
            //context.Response.BinaryWrite(ms.ToArray());
            //context.Response.End();
            return "";
        }

        [HttpPost]
        public string DelBackup(FormCollection context)
        {
            string basepath = Server.MapPath("~/dbase/");
            string filename = Request["n"].ToString();
            string downpath = basepath + filename;
            System.IO.File.Delete(downpath);

            //写入操作日志
            LogBll<object> log = new LogBll<object>();
            log.AddLog("删除备份文件", "数据库备份文件删除成功，文件名：" + filename);

            return new JsonMessage { Data = "1", Message = "删除成功。", Success = true }.ToString();

        }




        public ActionResult Setting()
        {
            ViewBag.ToolBar = BuildToolbar();

            Dictionary<string, SYS_PARAMSMODEL> dic = new Dictionary<string, SYS_PARAMSMODEL>();
            var list = SYS_PARAMSBLL.Instance.GetAll().ToList();
            foreach (var item in list)
            {
                dic[item.FKEY] = item;
            }

            return View(dic);
        }

        [HttpPost]
        public string SettinSave(FormCollection context)
        {
            try
            {
                //var list = SYS_PARAMSBLL.Instance.GetWhereKey("IntervalSP");
                //SYS_PARAMSBLL.Instance.DeleteAll();

                //string result = "";
                //SYS_PARAMSMODEL model = new SYS_PARAMSMODEL();

                //var intervalSP = Request["intervalSP"];
                //model.FKEY = "IntervalSP";
                //model.FVALUE = intervalSP;
                //model.FDESCRIPTION = "商品资料同步时间间隔(秒)";
                //result = SYS_PARAMSBLL.Instance.Add(model);

                //var intervalCG = Request["intervalCG"];
                //model.FKEY = "IntervalCG";
                //model.FVALUE = intervalCG;
                //model.FDESCRIPTION = "采购订单同步时间间隔(秒)";
                //result = SYS_PARAMSBLL.Instance.Add(model);

                //var intervalFH = Request["intervalFH"];
                //model.FKEY = "IntervalFH";
                //model.FVALUE = intervalFH;
                //model.FDESCRIPTION = "发货通知同步时间间隔(秒)";
                //result = SYS_PARAMSBLL.Instance.Add(model);

                //var intervalXS = Request["intervalXS"];
                //model.FKEY = "IntervalXS";
                //model.FVALUE = intervalXS;
                //model.FDESCRIPTION = "销售出库同步时间间隔(秒)";
                //result = SYS_PARAMSBLL.Instance.Add(model);

                //var inventoryMax = Request["inventoryMax"];
                //model.FKEY = "InventoryMax";
                //model.FVALUE = inventoryMax;
                //model.FDESCRIPTION = "配柜库存浮动上限";
                //result = SYS_PARAMSBLL.Instance.Add(model);

                //var inventoryMin = Request["inventoryMin"];
                //model.FKEY = "InventoryMin";
                //model.FVALUE = inventoryMin;
                //model.FDESCRIPTION = "配柜库存浮动下限";
                //result = SYS_PARAMSBLL.Instance.Add(model);

                var list = SYS_PARAMSBLL.Instance.GetAll();
                SYS_PARAMSMODEL model = new SYS_PARAMSMODEL();
                var intervalSP = Request["intervalSP"];
                bool isHave = false;
                foreach (var item in list)
                {
                    if (item.FKEY== "IntervalSP")
                    {
                        item.FVALUE = intervalSP;
                        SYS_PARAMSBLL.Instance.Update(item);
                        isHave = true;
                        break;
                    }
                }
                if (!isHave)
                {
                    model.FKEY = "IntervalSP";
                    model.FVALUE = intervalSP;
                    model.FDESCRIPTION = "商品资料同步时间间隔(秒)";
                    SYS_PARAMSBLL.Instance.Add(model);
                }
                var intervalCG = Request["intervalCG"];
                isHave = false;
                foreach (var item in list)
                {
                    if (item.FKEY == "IntervalCG")
                    {
                        item.FVALUE = intervalCG;
                        SYS_PARAMSBLL.Instance.Update(item);
                        isHave = true;
                        break;
                    }
                }
                if (!isHave)
                {
                    model.FKEY = "IntervalCG";
                    model.FVALUE = intervalCG;
                    model.FDESCRIPTION = "采购订单同步时间间隔(秒)";
                    SYS_PARAMSBLL.Instance.Add(model);
                }
                var intervalFH = Request["intervalFH"];
                isHave = false;
                foreach (var item in list)
                {
                    if (item.FKEY == "IntervalFH")
                    {
                        item.FVALUE = intervalFH;
                        SYS_PARAMSBLL.Instance.Update(item);
                        isHave = true;
                        break;
                    }
                }
                if (!isHave)
                {
                    model.FKEY = "IntervalFH";
                    model.FVALUE = intervalFH;
                    model.FDESCRIPTION = "发货通知同步时间间隔(秒)";
                    SYS_PARAMSBLL.Instance.Add(model);
                }

                var intervalXS = Request["intervalXS"];
                isHave = false;
                foreach (var item in list)
                {
                    if (item.FKEY == "IntervalXS")
                    {
                        item.FVALUE = intervalXS;
                        SYS_PARAMSBLL.Instance.Update(item);
                        isHave = true;
                        break;
                    }
                }
                if (!isHave)
                {
                    model.FKEY = "IntervalXS";
                    model.FVALUE = intervalXS;
                    model.FDESCRIPTION = "销售出库同步时间间隔(秒)";
                    SYS_PARAMSBLL.Instance.Add(model);
                }

                var inventoryMax = Request["inventoryMax"];
                isHave = false;
                foreach (var item in list)
                {
                    if (item.FKEY == "InventoryMax")
                    {
                        item.FVALUE = inventoryMax;
                        SYS_PARAMSBLL.Instance.Update(item);
                        isHave = true;
                        break;
                    }
                }
                if (!isHave)
                {
                    model.FKEY = "InventoryMax";
                    model.FVALUE = inventoryMax;
                    model.FDESCRIPTION = "配柜库存浮动上限";
                    SYS_PARAMSBLL.Instance.Add(model);
                }

                var inventoryMin = Request["inventoryMin"];
                isHave = false;
                foreach (var item in list)
                {
                    if (item.FKEY == "InventoryMin")
                    {
                        item.FVALUE = inventoryMin;
                        SYS_PARAMSBLL.Instance.Update(item);
                        isHave = true;
                        break;
                    }
                }
                if (!isHave)
                {
                    model.FKEY = "InventoryMin";
                    model.FVALUE = inventoryMin;
                    model.FDESCRIPTION = "配柜库存浮动下限";
                    SYS_PARAMSBLL.Instance.Add(model);
                }

                return JSONhelper.ToJson("保存成功！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return ex.Message;
            }
        }



        public string Upload()
        {
            Response.ContentType = "text/plain";

            //接收上传后的文件
            HttpPostedFileBase file = Request.Files["Filedata"];

            if (Path.GetExtension(file.FileName) != ".apk")
            {
                return "您上传的文件类型不被允许";
            }

            //获取文件的保存路径
            string uploadPath;

            string uploadFullPath = Server.MapPath(getFilePath(out uploadPath));

            //判断上传的文件是否为空
            if (file != null)
            {
                if (!Directory.Exists(uploadFullPath))
                {
                    Directory.CreateDirectory(uploadFullPath);
                }
                //保存文件
                string newFileName = getFileName(uploadFullPath, file.FileName);
                string newFileFullPath = uploadFullPath + newFileName;
                try
                {
                    int fileLength = file.ContentLength;

                    file.SaveAs(newFileFullPath);

                    return uploadPath+ newFileName;
                }
                catch
                {
                    return "上传文件发生了错误";
                }
            }
            else
            {
                return "上传文件为空";
            }
        }

        /// <summary>
        /// 得到上传文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string getFileName(string filePath, string fileName)
        {
            while (System.IO.File.Exists(filePath + fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_"+DateTime.Now.ToString("yyyyMMddHHmmssfff") +  Path.GetExtension(fileName);
            }
            return fileName;
        }

        /// <summary>
        /// 得到文件保存路径
        /// </summary>
        /// <returns></returns>
        private string getFilePath(out string path1)
        {
            DateTime date = DateTime.Now;
            path1 = Url.Content("~/Upload/" + date.ToString("yyyyMM") + "/" + date.ToString("dd") + "/");
            return path1;
        }


        public string Init()
        {
            List<PlatFormModel> list = PlatFormDal.Instance.GetAll().ToList();
            foreach(PlatFormModel model in list)
            {
                OrganizeModel m = new OrganizeModel();
                m.FID = model.ID.ToString();
               m.CODE = model.CODE;
               m.NAME = model.NAME;
               m.SHORTNAME = model.SHORTNAME;
               m.LINKMAN = model.LINKMAN;
               m.PHONE = model.PHONE;
               m.FAX = model.FAX;
               m.EMAIL = model.EMAIL;
               m.ADDRESS = model.ADDRESS;
               m.POSTCODE = model.POSTCODE;
               m.DISABLED = model.DISABLED;
               m.DESCRIPTION = model.DESCRIPTION;
                m.TYPE = 0;
                OrganizeDal.Instance.Insert(m);

            }

            return "1";
        }

     
    }
}
