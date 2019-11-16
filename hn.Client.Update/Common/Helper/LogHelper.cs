using System;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Common
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void Debug(Type t, string msg)

        public static void Debug(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Debug(msg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void Info(Type t, string msg)

        public static void Info(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Info(msg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void ShowInfo(Type t, string msg)

        public static void ShowInfo(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Info(msg);

            MsgHelper.ShowInformation(msg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <param name="showMsg"></param>
        #region static void ShowInfo(Type t, string msg, string showMsg)

        public static void ShowInfo(Type t, string msg, string showMsg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Info(msg);

            MsgHelper.ShowInformation(showMsg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void Warn(Type t, string msg)

        public static void Warn(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Warn(msg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void Error(Type t, Exception ex)

        public static void Error(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Error("Error", ex);

            //System.Windows.Forms.MessageBox.Show(ex.ToString());
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void Error(Type t, string msg)

        public static void Error(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Error(msg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void ShowError(Type t, Exception ex)

        public static void ShowError(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Error("Error", ex);

            MsgHelper.ShowError(ex.ToString());
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void ShowError(Type t, string msg)

        public static void ShowError(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Error(msg);

            MsgHelper.ShowError(msg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <param name="showMsg"></param>
        #region static void ShowError(Type t, string msg, string showMsg)

        public static void ShowError(Type t, string msg, string showMsg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Error(msg);

            MsgHelper.ShowError(showMsg);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void Fatal(Type t, Exception ex)

        public static void Fatal(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t.Name);
            log.Fatal("Fatal", ex);
        }

        #endregion
    }
}
