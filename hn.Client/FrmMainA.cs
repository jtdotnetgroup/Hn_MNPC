using hn.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hn.Client
{
    public partial class FrmMainA : Core.KFrmBase
    {
        #region ■------------------ 字段相关
        #endregion

        #region ■------------------ 构造加载

        private FrmMDIContainer _frmContainer = null;

        public FrmMainA()
        {
            InitializeComponent();
            lblCopyrightInfo.Location = new Point((Width - lblCopyrightInfo.Width) / 2, lblCopyrightInfo.Location.Y);

            //if (_frmContainer == null) _frmContainer = new FrmMDIContainer(this, 1, 38 + 26, 2, 38 + 26 + 27);
            //_frmContainer.TopLevel = true;


        }

        
        private void FrmMain_Resize(object sender, EventArgs e)
        {
            lblCopyrightInfo.Location = new Point((Width - lblCopyrightInfo.Width) / 2, lblCopyrightInfo.Location.Y);
        }

        #endregion

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmMainA), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmMainA), msg);
        }

        #endregion



        protected override void OnLostFocus(EventArgs e)
        {
            Debug.WriteLine("丢失焦点：" + DateTime.Now.ToString());
            base.OnLostFocus(e);
            if (_frmContainer != null)
            {
                _frmContainer.Main_LostFocus();
            }

        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            Debug.WriteLine("得到焦点：" + DateTime.Now.ToString());
        }

        private void 在线升级ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FrmMainA_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        }
    }
}
