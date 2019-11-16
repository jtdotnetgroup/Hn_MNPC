using hn.Common;
using hn.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace hn.Client
{
    public partial class FrmLogin : Form
    {
        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        public FrmLogin()
        {
            InitializeComponent();

            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW); //API函数加载，实现窗体边框阴影效果
            txtACC.Focus();
            txtACC.Text= IniHelper.ReadString(Global.IniUrl, "CONFIG", "username", "");
            txtPAS.Text = IniHelper.ReadString(Global.IniUrl, "CONFIG", "password", "");
            lblVerName.Text = IniHelper.ReadString(Global.IniUrl, "CONFIG", "VerName", "");
        }

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmLogin), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmLogin), msg);
        }

        #endregion

        private FrmMainB _frmMain;


        private void FrmLogin_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                using (Pen pen = new Pen(Color.Gray))
                {
                    graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ApiService.APIServiceClient service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
                User user = service.Login(txtACC.Text, txtPAS.Text);
                if (user == null)
                {
                    MsgHelper.ShowError("用户名或密码不正确！");
                    return;
                }

                Global.LoginUser = user;

                Hide();
               
                if (_frmMain == null) _frmMain = new FrmMainB(this);
                {
                    IniHelper.WriteString(Global.IniUrl, "CONFIG", "username", txtACC.Text);
                    IniHelper.WriteString(Global.IniUrl, "CONFIG", "password", txtPAS.Text);
                    _frmMain.Show();
                }
               
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                LogError(ex);
                MsgHelper.ShowError("登陆异常！");
            }
        }



        private int _locationX;
        private int _locationY;
        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Normal)
                {
                    _locationX = e.X;
                    _locationY = e.Y;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Normal)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Point pointOld = Location;
                        Location = new Point(pointOld.X + (e.X - _locationX), pointOld.Y + (e.Y - _locationY));
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void btnSysSet_Click(object sender, EventArgs e)
        {
            FrmConfig frm = new FrmConfig();
            frm.ShowDialog();
        }

        private void btn退出_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (MsgHelper.AskQuestion("确认退出登录吗？"))
                    {
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void btnClose_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_Properties_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void btnSysSet_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
