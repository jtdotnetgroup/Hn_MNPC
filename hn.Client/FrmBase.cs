using hn.Client.Core;
using hn.Client.Properties;
using hn.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hn.Client
{
    public partial class FrmBase : Form
    {
        public FrmBase()
        {
            InitializeComponent();


        }

        #region ■------------------ 运行日志

        public virtual void LogError(Exception ex,Type type)
        {
            LogHelper.WriteLog(type, ex);
        }

        public virtual void LogInfo(string msg,Type type)
        {
            LogHelper.WriteLog(type, msg);
        }

        #endregion

        private void picClose_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LogError(ex, GetType());
            }
        }

        private void FrmBase_Resize(object sender, EventArgs e)
        {
            //try
            //{
            //    pictureBox1.Visible = MaximizeBox;
            //    picClose.Location = new Point(Width - picClose.Width - 4, 4);
            //    pictureBox1.Location = new Point(Width - picClose.Width - 4 - picClose.Width - 6, 4);
            //}
            //catch (Exception ex)
            //{
            //    LogError(ex, GetType());
            //}
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    MaximizeWindow(!this.K_IsMaximized);
            //}
            //catch (Exception ex)
            //{
            //    LogError(ex,GetType());
            //}
        }

        private void FrmBase_K_WindowStateChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    pictureBox1.Image = K_IsMaximized ? Resources.TitleNormal222 : Resources.TitleMax22;
            //}
            //catch (Exception ex)
            //{
            //    LogError(ex,GetType());
            //}
        }
    }
}
