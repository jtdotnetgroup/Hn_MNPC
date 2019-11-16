using hn.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace hn.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Global.WcfUrl = IniHelper.ReadString(Global.IniUrl, "CONFIG", "URL", "");
            Global.WebUrl = IniHelper.ReadString(Global.IniUrl, "CONFIG", "Web", "");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

                Application.Run(new FrmLogin());
          
        }

        /// <summary>
        /// 判断是否需要更新程序
        /// </summary>
        /// <returns></returns>
        private static bool Update()
        {
            VerModel verModel = null;
            string error = "";
            bool isUpdate = false;
            if (GetVersion(out verModel, out error))
            {
                if (verModel != null && verModel.VerCode > Global.VersionInterior)//是否内部版本号比当前的大
                {
                    if (verModel.IsMustUpdate)//是否是强制更新
                    {
                        isUpdate = true;
                    }
                    else
                    {
                        if (MsgHelper.AskQuestion("有新的版本发布，是否下载更新？"))
                        {
                            isUpdate = true;
                        }
                        else
                        {
                            return true;
                        }
                    }

                    if (isUpdate)
                    {
                        //获取需要更新的文件，并且记录在Update.txt文件夹中
                        List<string> list = null;
                        if (GetUpdateFiles(out list, out error))
                        {
                            if (list == null || list.Count == 0)
                            {
                                MsgHelper.ShowError("获取更新文件为空！");
                                return false;
                            }

                            string path = Application.ExecutablePath;
                            char strSplit = '\\';
                            if (path.Contains("\\"))
                            {
                                strSplit = '\\';
                            }
                            else if (path.Contains("/"))
                            {
                                strSplit = '/';
                            }
                            string[] array = path.Split(strSplit);
                            string name = array[array.Length - 1].Split('.')[0];
                            using (StreamWriter sw = File.CreateText(Global.UpdatePath))
                            {
                                sw.WriteLine(name);
                                foreach (string str in list)
                                {
                                    sw.WriteLine(str);
                                }
                            }

                            IniHelper.WriteString(Global.IniUrl, "CONFIG", "VerName", verModel.VerName);

                            Process p = new Process();
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.FileName = Application.StartupPath + "\\AutoUpdate.exe";
                            p.Start();
                            Environment.Exit(0);
                            return false;
                        }
                        else
                        {
                            MsgHelper.ShowError("获取更新文件失败！/r/n" + error);
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MsgHelper.ShowError(error);
            }
            return false;
        }


        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="version"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool GetVersion(out VerModel verModel, out string error)
        {
            verModel = null;
            error = "异常！";

            string msg = "typeid=1";
            string result = Post.SendMsg("/Web/GetClientVer", msg);

            JsonRev rec = JsonHelper.ConvertToObject<JsonRev>(result);
            if (rec.errCode == 0)
            {
                if (rec.data != null)
                {
                    verModel = JsonHelper.ConvertToObject<VerModel>(rec.data.ToString());
                }
                return true;
            }
            else
            {
                error = rec.errMsg;
            }

            return false;
        }

        /// <summary>
        /// 查询要更新的文件列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool GetUpdateFiles(out List<string> list, out string error)
        {
            list = null;
            error = "异常！";

            string msg = "typeid=1";
            string result = Post.SendMsg("/Web/GetClientUpdateFiles", msg);

            UpdateFilesRev rec = JsonHelper.ConvertToObject<UpdateFilesRev>(result);
            if (rec.errCode == 0)
            {
                if (rec.data != null)
                {
                    list = rec.data;
                }
                return true;
            }
            else
            {
                error = rec.errMsg;
            }

            return false;
        }

        public class JsonRev
        {
            public int errCode;
            public string errMsg;
            public object data;
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

        public class UpdateFilesRev
        {
            public int errCode;
            public string errMsg;
            public List<string> data;
        }
    }
}
