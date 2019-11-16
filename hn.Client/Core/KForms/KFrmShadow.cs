using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace hn.Client.Core
{
    public partial class KFrmShadow : Form
    {
        /* 原理：
         * 在显示控件层的时候，先显示一个绘图层用于绘制阴影
         * 绘图层是一个比控件层大5个像素的窗体  （绘图层的 X Y 比控件层 少5  宽高+10）
         * 控件层在上 绘图层在下
         * 
         */

        private KFrmBase _frmMain;//控件层

        public KFrmShadow(KFrmBase form)
        {
            InitializeComponent();


            _frmMain = form;
            _frmMain.TopMost = TopMost = _frmMain.TopMost;//置顶窗体
            _frmMain.BringToFront();

            //绘图层窗体移动
            _frmMain.LocationChanged += new EventHandler(Main_LocationChanged);
            _frmMain.SizeChanged += new EventHandler(Main_SizeChanged);
            _frmMain.VisibleChanged += new EventHandler(Main_VisibleChanged);



            //设置绘图层显示位置及大小 边框
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(_frmMain.Location.X - 5, _frmMain.Location.Y - 5);
            Width = _frmMain.Width + 10;
            Height = _frmMain.Height + 10;

            //窗体图标及任务栏图标 标题
            Icon = _frmMain.Icon;
            ShowIcon = _frmMain.ShowIcon;
            ShowInTaskbar = false;
            Text = _frmMain.Text;




            //CommonClass.SetTaskMenu(_frmMain);//还原任务栏右键菜单

            SetBits();//不规则无毛边方法

            CanPenetrate();//窗口鼠标穿透效果
        }

        #region ■------------------ 绘图层跟随控件层的事件  （移动、改变大小、显示隐藏）

        //移动主窗体时
        private void Main_LocationChanged(object sender, EventArgs e)
        {
            Location = new Point(_frmMain.Left - 5, _frmMain.Top - 5);
        }

        //主窗体大小改变时
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            //设置大小
            Width = _frmMain.Width + 10;
            Height = _frmMain.Height + 10;
            SetBits();
        }

        //主窗体显示或隐藏时
        private void Main_VisibleChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated)
            {
                if (_frmMain != null && !_frmMain.IsDisposed)
                    Visible = _frmMain.Visible;
            }
        }


        #endregion

        #region ■------------------ 使窗口有鼠标穿透功能

        /// <summary>
        /// 使窗口有鼠标穿透功能
        /// </summary>
        private void CanPenetrate()
        {
            int intExTemp = Win32API.GetWindowLong(this.Handle, Win32API.GWL_EXSTYLE);
            int oldGWLEx = Win32API.SetWindowLong(this.Handle, Win32API.GWL_EXSTYLE, Win32API.WS_EX_TRANSPARENT | Win32API.WS_EX_LAYERED);
        }

        #endregion

        #region ■------------------ 不规则无毛边方法

        public void SetBits()
        {
            //绘制绘图层画布:用于绘图
            Bitmap bitmap = new Bitmap(_frmMain.Width + 10, _frmMain.Height + 10);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量


            Rectangle _BacklightLTRB = new Rectangle(20, 20, 20, 20);//窗体光泽重绘边界
            DrawRect(g, Properties.Resources.shadow_rect, ClientRectangle, Rectangle.FromLTRB(_BacklightLTRB.X, _BacklightLTRB.Y, _BacklightLTRB.Width, _BacklightLTRB.Height), 1, 1);

            if (!Image.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Image.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("图片必须是32位带Alhpa通道的图片。");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32API.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32API.CreateCompatibleDC(screenDC);

            try
            {
                Win32API.Point topLoc = new Win32API.Point(Left, Top);
                Win32API.Size bitMapSize = new Win32API.Size(Width, Height);
                Win32API.BLENDFUNCTION blendFunc = new Win32API.BLENDFUNCTION();
                Win32API.Point srcLoc = new Win32API.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32API.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32API.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = byte.Parse("255");
                blendFunc.AlphaFormat = Win32API.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                if (!IsDisposed)
                {
                    Win32API.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32API.ULW_ALPHA);
                }
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32API.SelectObject(memDc, oldBits);
                    Win32API.DeleteObject(hBitmap);
                }
                Win32API.ReleaseDC(IntPtr.Zero, screenDC);
                Win32API.DeleteDC(memDc);
            }
        }

        /// <summary>
        /// 绘图对像
        /// </summary>
        /// <param name="g">绘图对像</param>
        /// <param name="img">图片</param>
        /// <param name="r">绘置的图片大小、坐标</param>
        /// <param name="lr">绘置的图片边界</param>
        /// <param name="index">当前状态</param> 
        /// <param name="Totalindex">状态总数</param>
        public static void DrawRect(Graphics g, Bitmap img, Rectangle r, Rectangle lr, int index, int Totalindex)
        {
            if (img == null) return;
            Rectangle r1, r2;
            int x = (index - 1) * img.Width / Totalindex;
            int y = 0;
            int x1 = r.Left;
            int y1 = r.Top;

            if (r.Height > img.Height && r.Width <= img.Width / Totalindex)
            {
                r1 = new Rectangle(x, y, img.Width / Totalindex, lr.Top);
                r2 = new Rectangle(x1, y1, r.Width, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + lr.Top, img.Width / Totalindex, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1, y1 + lr.Top, r.Width, r.Height - lr.Top - lr.Bottom);
                if ((lr.Top + lr.Bottom) == 0) r1.Height = r1.Height - 1;
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + img.Height - lr.Bottom, img.Width / Totalindex, lr.Bottom);
                r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, r.Width, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
            else if (r.Height <= img.Height && r.Width > img.Width / Totalindex)
            {
                r1 = new Rectangle(x, y, lr.Left, img.Height);
                r2 = new Rectangle(x1, y1, lr.Left, r.Height);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                r1 = new Rectangle(x + lr.Left, y, img.Width / Totalindex - lr.Left - lr.Right, img.Height);
                r2 = new Rectangle(x1 + lr.Left, y1, r.Width - lr.Left - lr.Right, r.Height);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y, lr.Right, img.Height);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, r.Height);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
            else if (r.Height <= img.Height && r.Width <= img.Width / Totalindex)
            {
                r1 = new Rectangle((index - 1) * img.Width / Totalindex, 0, img.Width / Totalindex, img.Height - 1);
                g.DrawImage(img, new Rectangle(x1, y1, r.Width, r.Height), r1, GraphicsUnit.Pixel);
            }
            else if (r.Height > img.Height && r.Width > img.Width / Totalindex)
            {
                //top-left
                r1 = new Rectangle(x, y, lr.Left, lr.Top);
                r2 = new Rectangle(x1, y1, lr.Left, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //top-bottom
                r1 = new Rectangle(x, y + img.Height - lr.Bottom, lr.Left, lr.Bottom);
                r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, lr.Left, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //left
                r1 = new Rectangle(x, y + lr.Top, lr.Left, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1, y1 + lr.Top, lr.Left, r.Height - lr.Top - lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //top
                r1 = new Rectangle(x + lr.Left, y,
                    img.Width / Totalindex - lr.Left - lr.Right, lr.Top);
                r2 = new Rectangle(x1 + lr.Left, y1,
                    r.Width - lr.Left - lr.Right, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //right-top
                r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y, lr.Right, lr.Top);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //Right
                r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y + lr.Top,
                    lr.Right, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + lr.Top,
                    lr.Right, r.Height - lr.Top - lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //right-bottom
                r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y + img.Height - lr.Bottom,
                    lr.Right, lr.Bottom);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + r.Height - lr.Bottom,
                    lr.Right, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //bottom
                r1 = new Rectangle(x + lr.Left, y + img.Height - lr.Bottom,
                    img.Width / Totalindex - lr.Left - lr.Right, lr.Bottom);
                r2 = new Rectangle(x1 + lr.Left, y1 + r.Height - lr.Bottom,
                    r.Width - lr.Left - lr.Right, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //Center
                r1 = new Rectangle(x + lr.Left, y + lr.Top,
                    img.Width / Totalindex - lr.Left - lr.Right, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1 + lr.Left, y1 + lr.Top,
                    r.Width - lr.Left - lr.Right, r.Height - lr.Top - lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
        }

        #endregion




        //#region ■------------------ 还原任务栏右键菜单

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
                return cParms;
            }
        }

        //public class CommonClass
        //{
        //    [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        //    static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        //    [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        //    static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        //    public const int WS_SYSMENU = 0x00080000;
        //    public const int WS_MINIMIZEBOX = 0x20000;
        //    public static void SetTaskMenu(Form form)
        //    {
        //        int windowLong = (GetWindowLong(new HandleRef(form, form.Handle), -16));
        //        SetWindowLong(new HandleRef(form, form.Handle), -16, windowLong | WS_SYSMENU | WS_MINIMIZEBOX);
        //    }
        //}

        //#endregion




        //#region ■------------------ 减少闪烁

        //private void SetStyles()
        //{
        //    SetStyle(
        //        ControlStyles.UserPaint |
        //        ControlStyles.AllPaintingInWmPaint |
        //        ControlStyles.OptimizedDoubleBuffer |
        //        ControlStyles.ResizeRedraw |
        //        ControlStyles.DoubleBuffer, true);
        //    //强制分配样式重新应用到控件上
        //    UpdateStyles();
        //    base.AutoScaleMode = AutoScaleMode.None;
        //}

        //#endregion
    }
}
