using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Layout;
using log4net.Appender;
using System.IO;
using log4net.Config;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace hn.APIService
{

    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            LoadFileAppender();
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        public static void WriteLog(Exception ex)
        {
            LoadFileAppender();
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(LogHelper));
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)

        public static void WriteLog(Type t, string msg)
        {
            LoadFileAppender();
            //if(PubConstant)
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        public static void WriteLog(string msg)
        {
            LoadFileAppender();
            //if(PubConstant)
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(LogHelper));
            log.Error(msg);
        }
        #endregion


        /// <summary>
        /// 使用文本记录异常日志
        /// </summary>
        /// <Author>Ryanding</Author>
        /// <date>2011-05-01</date>
        public static void LoadFileAppender()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string txtLogPath = string.Empty;
            string iisBinPath = AppDomain.CurrentDomain.RelativeSearchPath;

            if (!string.IsNullOrEmpty(iisBinPath))
                txtLogPath = CreateDirectory(iisBinPath);// Path.Combine(iisBinPath, "ErrorLog.txt");
            else
                txtLogPath = CreateDirectory(currentPath);// Path.Combine(currentPath, "ErrorLog.txt");



            log4net.Repository.Hierarchy.Hierarchy hier =
              log4net.LogManager.GetLoggerRepository() as log4net.Repository.Hierarchy.Hierarchy;

            FileAppender fileAppender = new FileAppender();
            fileAppender.Name = "LogFileAppender";
            fileAppender.File = txtLogPath;
            fileAppender.AppendToFile = true;

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "记录时间：%date LOG描述：%message%newline";
            patternLayout.ActivateOptions();
            fileAppender.Layout = patternLayout;

            //选择UTF8编码，确保中文不乱码。
            fileAppender.Encoding = Encoding.UTF8;

            fileAppender.ActivateOptions();
            BasicConfigurator.Configure(fileAppender);

        }

        private static string CreateDirectory(string path)
        {
            path = path + "\\log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMM");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";


            return path;

        }

    }
}
