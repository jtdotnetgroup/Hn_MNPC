
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hn.Client.Update
{
    public partial class frmAutoUpdate : Form
    {
        private bool _isSuccess = true;
        private string ApplicationName = "IC卡读写";
        private List<string> listFilePath = new List<string>();
        public readonly static string UpdatePath = Application.StartupPath + "\\Update.txt";

        public frmAutoUpdate()
        {
            InitializeComponent();
        }

        private void frmAutoUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = File.OpenText(Globals.UpdatePath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        listFilePath.Add(s);
                    }
                }

                if (listFilePath.Count > 0)
                {
                    ApplicationName = listFilePath[0];

                    KillApplication();

                    backgroundWorker下载.RunWorkerAsync();
                }
                else
                {
                    AddTextBox("未查询到更新文件！");
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// 杀死所有的主程序
        /// </summary>
        /// <returns></returns>
        private bool KillApplication()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ApplicationName);
                foreach (Process process in processes)
                {
                    process.Kill();
                    System.Threading.Thread.Sleep(500);
                }

                return true;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 备份程序
        /// </summary>
        private void BackupApplication()
        {
            try
            {
                string folder = "Backup\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("yyyyMM") + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss");
                folder = Application.StartupPath + "\\" + folder;

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string[] files = Directory.GetFiles(Application.StartupPath);//读出需要复制的文件
                backgroundWorker下载.ReportProgress(0, files.Length);
                for (int i = 0; i < files.Length; i++)
                {
                    string[] array = files[i].Split('\\');
                    if (array != null && array.Length > 0)
                    {
                        string fileName = array[array.Length - 1];
                        FileInfo fi = new FileInfo(files[i]);
                        fi.CopyTo(folder + "\\" + fileName, true);//拷贝
                        backgroundWorker下载.ReportProgress(1, i + 1);
                        backgroundWorker下载.ReportProgress(2, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        /// <summary>
        /// Http下载文件
        /// </summary>
        /// <param name="urlFilePath">域名+文件的相对路径</param>
        /// <param name="fileName">需要生成的文件名称，包括后缀</param>
        /// <returns></returns>
        private bool HttpDownloadFile(string urlFilePath, int num)
        {
            try
            {
                string filePath = null;
                string fileName = null;

                char strSplit = '\\';
                if(urlFilePath.Contains('\\'))
                {
                    strSplit = '\\';
                }
                else if(urlFilePath.Contains('/'))
                {
                    strSplit = '/';
                }
                string[] array = urlFilePath.Split(strSplit);
                if (array != null && array.Length > 0)
                {
                    fileName = array[array.Length - 1];
                    filePath = Application.StartupPath + "\\" + fileName;
                }
                if (string.IsNullOrEmpty(filePath))
                    return false;

                // 设置参数
                HttpWebRequest request = WebRequest.Create(urlFilePath) as HttpWebRequest;

                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();

                //创建本地文件写入流
                Stream stream = new FileStream(filePath, FileMode.Create);

                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, bArr.Length);
                }
                stream.Close();
                responseStream.Close();

                backgroundWorker下载.ReportProgress(3, num);
                backgroundWorker下载.ReportProgress(4, fileName);

                return true;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
                return false;
            }
        }

        private void backgroundWorker下载_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackupApplication();
                backgroundWorker下载.ReportProgress(5, "备份完成！");
                System.Threading.Thread.Sleep(1000);

                backgroundWorker下载.ReportProgress(0, listFilePath.Count - 1);
                for (int i = 1; i < listFilePath.Count; i++)
                {
                    if(!HttpDownloadFile(listFilePath[i], i))
                    {
                        backgroundWorker下载.ReportProgress(5, "更新失败！");
                        return;
                    }
                }
                backgroundWorker下载.ReportProgress(5, "更新完成！");
                System.Threading.Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        private void backgroundWorker下载_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                switch (e.ProgressPercentage)
                {
                    case 0://设置进度条最大值
                        progressBar更新进度.Value = 0;
                        progressBar更新进度.Maximum = int.Parse(e.UserState.ToString());
                        break;
                    case 1://备份时的进度设置
                        progressBar更新进度.Value = int.Parse(e.UserState.ToString());
                        break;
                    case 2://备份时的文本框说明
                        AddTextBox("正在备份文件：" + e.UserState.ToString());
                        break;
                    case 3://下载文件的进度设置
                        progressBar更新进度.Value = int.Parse(e.UserState.ToString());
                        break;
                    case 4://下载文件时的文本框说明
                        AddTextBox("正在更新文件：" + e.UserState.ToString());
                        break;
                    case 5://文本提示
                        AddTextBox(e.UserState.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        private void backgroundWorker下载_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                File.Delete(Globals.UpdatePath);
                if (_isSuccess)
                {
                    if (MsgHelper.AskQuestion("更新完成，是否启动程序？"))
                    {
                        Process p = new Process();
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.FileName = Application.StartupPath + "\\" + ApplicationName + ".exe";
                        p.Start();
                    }
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        private void AddTextBox(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
                string update = DateTime.Now.ToString() + " - " + text + "\r\n";

                txt更新进度记录.Text += update;

                txt更新进度记录.Focus();
                //设置光标的位置到文本尾 
                txt更新进度记录.Select(txt更新进度记录.TextLength, 0);

                txt更新进度记录.ScrollToCaret();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }

        private void WriteErrorLog(Exception ex)
        {
            _isSuccess = false;
            MsgHelper.ShowError(ex.Message + ex.ToString());
            File.Delete(Globals.UpdatePath);
            Environment.Exit(0);
        }
    }
}
