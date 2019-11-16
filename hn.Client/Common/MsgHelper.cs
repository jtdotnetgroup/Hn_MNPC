﻿using System;
using System.Windows.Forms;

namespace hn.Client
{
    public class MsgHelper
    {
        /// <summary>
        /// 打开对话框         /// </summary>
        /// <param name="msg">本次对话内容</param>
        /// <returns></returns>
        public static bool AskQuestion(string msg)
        {
            DialogResult r;
            r = MessageBox.Show(msg, "确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            return (r == DialogResult.Yes);
        }

        /// <summary>
        /// 显示系统异常
        /// </summary>
        /// <param name="e">系统异常</param>
        public static void ShowException(Exception e)
        {
            string s = e.Message;
            string innerMsg = string.Empty;

            if (e.InnerException != null)
            {
                innerMsg = e.InnerException.Message;
                s += "\n" + innerMsg;
            }

            Warning(s);
        }

        public static void ShowException(Exception ex, string customMessage)
        {
            Warning(ex.Message);
        }

        /// <summary>
        /// 警告提示框         /// </summary>
        /// <param name="msg">警告内容</param>
        public static void Warning(string msg)
        {
            MessageBox.Show(msg, "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 错误消息提示框         /// </summary>
        /// <param name="msg">错误消息内容</param>
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Hand,
                MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 信息提示框         /// </summary>
        /// <param name="msg">本次显示的消息</param>
        public static void ShowInformation(string msg)
        {
            MessageBox.Show(msg, "信息",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button1);
        }

        public static void ShowVersion()
        {
            string ver = "0.0.90020150710_MD。";
            MessageBox.Show("版本信息：" + ver, "信息",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button1);
        }
    }
}
