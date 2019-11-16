using hn.Client.Core;
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
    public partial class FrmMDIContainer : Form
    {

        private FrmMainA _frmMain;

        private int _offsetX, _offsetY, _offsetW, _offsetH;
        public FrmMDIContainer(FrmMainA frmMain,int offsetX, int offsetY, int offsetW, int offsetH)
        {
            InitializeComponent();
            _frmMain = frmMain;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _offsetW = offsetW;
            _offsetH = offsetH;
            Location = new Point(_frmMain.Left + _offsetX, _frmMain.Top + _offsetY);
            Width = _frmMain.Width - _offsetW;
            Height = _frmMain.Height - _offsetH;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            BringToFront();
            Show();


            //跟随主窗体改变
            _frmMain.LocationChanged += Main_LocationChanged;
            _frmMain.SizeChanged += Main_SizeChanged;
            _frmMain.VisibleChanged += Main_VisibleChanged;
            _frmMain.Activated += Main_Activated;

            FrmIndex frm = new FrmIndex();
            frm.MdiParent = this;
            frm.Activate();
            frm.Show();

        }



        #region ■------------------ 绘图层跟随控件层的事件  （移动、改变大小、显示隐藏）

        //移动主窗体时
        private void Main_LocationChanged(object sender, EventArgs e)
        {
            Location = new Point(_frmMain.Left + _offsetX, _frmMain.Top + _offsetY);
        }

        //主窗体大小改变时
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            //设置大小
            Width = _frmMain.Width - _offsetW;
            Height = _frmMain.Height - _offsetH;
        }

        //主窗体显示或隐藏时
        private void Main_VisibleChanged(object sender, EventArgs e)
        {
            if (_frmMain.Visible)
            {
                Debug.WriteLine("窗口显示：" + DateTime.Now.ToString());
            }
            else
            {
                Debug.WriteLine("窗口隐藏：" + DateTime.Now.ToString());
            }
            
            if (IsHandleCreated)
            {
                if (_frmMain != null && !_frmMain.IsDisposed)
                    Visible = _frmMain.Visible;
            }
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            //TopMost = true;
            //Visible = true;
        }

        public void Main_LostFocus()
        {
            //TopMost = false;
            //Visible = false;
        }

        #endregion
    }
}
