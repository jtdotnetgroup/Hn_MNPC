using hn.Client.Properties;
using hn.Client.Views;
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
    public partial class FrmMainB :  Core.KFrmBase
    {
        #region ■------------------ 字段相关
        #endregion

        #region ■------------------ 构造加载

        public FrmMainB(FrmLogin frmLogin)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            MaximizeWindow(true);
            OpenChildForm(new FrmIndex() { MainForm = this });

            #region 销区列表

            if (Global.TableMarketArea == null) Global.TableMarketArea = new DataTable();
            {
                Global.TableMarketArea.Columns.Add("ID");
                Global.TableMarketArea.Columns.Add("Name");
            }

            for (int i = 0; i < 10; i++)
            {
                DataRow row = Global.TableMarketArea.NewRow();
                row["ID"] = i;
                row["Name"] = "销区" + i;
                Global.TableMarketArea.Rows.Add(row);
            }

            #endregion
        }

        private void FrmMainB_Load(object sender, EventArgs e)
        {
            lblCopyrightInfo.Location = new Point((pnlStateBar.Width - lblCopyrightInfo.Width) / 2, lblCopyrightInfo.Location.Y);
            lbl登录用户.Text = string.Format("{0}，欢迎登录！", Global.LoginUser.UserName);
        }

        #endregion

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmMainB), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmMainB), msg);
        }

        #endregion






        #region ■------------------ 子窗口管理

        public void OpenChildForm(Form child)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                bool isHave = false;
                foreach (var form in MdiChildren)
                {
                    if (form.GetType().Equals(child.GetType()))
                    {
                        xtraTabbedMdiManager1.SelectedPage = xtraTabbedMdiManager1.Pages.First(o => o.MdiChild.GetType() == child.GetType());
                        isHave = true;
                        break;
                    }
                }
                if (!isHave)
                {
                    child.MdiParent = this;
                    child.Activate();
                    child.Show();
                }
            }           
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void xtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            try
            {
                string s = e.Page.MdiChild.GetType().ToString();
                if (s == "hn.Client.FrmSendGoodsPlan")
                {
                    e.Page.ImageIndex = 3;
                }
                else
                {
                    e.Page.ImageIndex = 0;
                }

                if (s == "hn.Client.FrmIndex")
                {
                    e.Page.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
                }
                
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        #endregion



        #region ■------------------ 标题栏

        private int _locationX;
        private int _locationY;
        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
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
                //LogError(ex);
            }
        }
        private void pnlTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!K_IsMaximized)/*(WindowState == FormWindowState.Normal)*/
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
                //LogError(ex);
            }
        }
        private void pnlTitleBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button== MouseButtons.Left)
                {
                    MaximizeWindow(!K_IsMaximized);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void pnlTitleBar_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                using (Brush brush=new SolidBrush(K_TitleTextColor))
                {
                    g.DrawString(Text, K_TitleTextFont, brush,K_TitleTextBounds,new StringFormat() { LineAlignment= StringAlignment.Center});
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAPPClose_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnTitleMaxMin_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button== MouseButtons.Left)
                {
                    MaximizeWindow(!K_IsMaximized);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FrmMainB_K_WindowStateChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = K_IsMaximized ? Resources.TitleNormal222 : Resources.TitleMax22;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        #endregion

        #region ■------------------ 菜单栏

        private FrmLogin _frmLogin;
        private void barBtn切换用户_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Hide();
                if (_frmLogin == null) _frmLogin = new FrmLogin();
                _frmLogin.Show();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }


        private void barbtn请购计划_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenChildForm(new FrmPleasePurchasePlan());
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void barbtn发货计划_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenChildForm(new FrmSendGoodsPlan());
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void barbtn退出_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void 请购计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChildForm(new FrmPleasePurchasePlan());
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void 发货计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChildForm(new FrmSendGoodsPlan());
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region ■------------------ 状态栏

        private void pnlStateBar_Resize(object sender, EventArgs e)
        {
            lblCopyrightInfo.Location = new Point((pnlStateBar.Width - lblCopyrightInfo.Width) / 2, lblCopyrightInfo.Location.Y);
        }











        #endregion

        private void btn修改密码_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPASChange frm = new FrmPASChange();
                if (frm.ShowDialog()== DialogResult.OK)
                {

                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            //OpenChildForm(new frm库存查询());
            frm库存查询 frmQuery = new frm库存查询();
            frmQuery.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmQuery.Width = 900;
            frmQuery.Height = 300;
            frmQuery.ShowIcon = false;

            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - frmQuery.Width;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - frmQuery.Height;
            Point p = new Point(x, y);
            frmQuery.PointToScreen(p);
            frmQuery.Location = p;
            frmQuery.TopMost = true;
            frmQuery.Show();
            */
            Helper.ShowQuery();

        }

        private void FrmMainB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MsgHelper.AskQuestion("确定要退出系统吗？"))
            {
                e.Cancel = true;
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenChildForm(new FrmOrderList());
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }
    }
}
