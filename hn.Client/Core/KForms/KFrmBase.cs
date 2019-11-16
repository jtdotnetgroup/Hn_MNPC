using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace hn.Client.Core
{
    public partial class KFrmBase : Form
    {
        public event EventHandler K_WindowStateChanged;

        #region ■------------------ 字段相关

        private KFrmShadow _frmShadow;

        private bool _formIsMaximized;

        /// <summary>
        /// 拖动边框改变窗体大小的鼠标图标状态集合
        /// </summary>
        private readonly Cursor[] _resizeCursors = { Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNS };
        private readonly Dictionary<int, int> resizingLocationsToCmd = new Dictionary<int, int>
        {
            {HT_TOP,         WMSZ_TOP},
            {HT_TOPLEFT,     WMSZ_TOPLEFT},
            {HT_TOPRIGHT,    WMSZ_TOPRIGHT},
            {HT_LEFT,        WMSZ_LEFT},
            {HT_RIGHT,       WMSZ_RIGHT},
            {HT_BOTTOM,      WMSZ_BOTTOM},
            {HT_BOTTOMLEFT,  WMSZ_BOTTOMLEFT},
            {HT_BOTTOMRIGHT, WMSZ_BOTTOMRIGHT}
        };
        /// <summary>
        /// 拖动边框改变窗体大小的方向
        /// </summary>
        private KResizeDirection _resizeDir;

        private Rectangle _menuBarBounds;
        private Rectangle _titleBarBounds;
        private Rectangle _stateBarBounds;
        private Size previousSize;
        private Point previousLocation;
        private bool _headerMouseDown;
        private Point _headerMouseDownPoint;

        /// <summary>
        /// 打开当前窗口的父窗口对象
        /// </summary>
        public Form ParentFormEx;

        //private KControls.KPictureBox pic关闭;
        //private KControls.KPictureBox pic最大化;
        //private KControls.KPictureBox pic最小化;

        #region 〓〓〓〓〓〓 属性

        [Category("KT"), Description("窗体边框显示阴影")]
        public bool K_IsShowShadow { get; set; }

        [Category("KT"), Description("是否可以通过拖动边框改变界面大小")]
        public bool K_ResizeAble { get; set; }

        [Category("KT"), Description("标题栏状态栏菜单栏颜色是否跟随主题变化")]
        public bool K_IsFollowTheme { get; set; }

        private Color _titleBarColor1 = Color.FromArgb(57, 141, 238);
        [Category("KT"), Description("标题栏颜色，K_IsFollowTheme属性为false时有效")]
        public Color K_ColorTitleBar
        {
            get { return _titleBarColor1; }
            set { _titleBarColor1 = value; Invalidate(); }
        }

        private Color _stateBarColor2 = Color.FromArgb(57, 141, 238);
        [Category("KT"), Description("菜单栏状态栏颜色，K_IsFollowTheme属性为false时有效")]
        public Color K_ColorStateBar
        {
            get { return _stateBarColor2; }
            set { _stateBarColor2 = value; Invalidate(); }
        }

        private bool _isShowFormBorder = true;
        [Category("KT"), Description("窗体是否显示边框")]
        public bool K_IsShowFormBorder
        {
            get { return _isShowFormBorder; }
            set
            {
                _isShowFormBorder = value; Invalidate();
            }
        }

        private Color _formBorderColor = Color.DimGray;
        [Category("KT"), Description("窗体边框颜色")]
        public Color K_FormBorderColor
        {
            get { return _formBorderColor; }
            set { _formBorderColor = value; Invalidate(); }
        }

        private int _formBorderWidth = 1;
        [Category("KT"), Description("窗体边框宽度")]
        public int K_FormBorderWidth
        {
            get { return _formBorderWidth; }
            set { _formBorderWidth = value; Invalidate(); }
        }

        private Color _titleTextColor = Color.White;
        [Category("KT"), Description("标题文本颜色")]
        public Color K_TitleTextColor
        {
            get { return _titleTextColor; }
            set
            {
                _titleTextColor = value;
                Invalidate(_titleBarBounds);
            }
        }

        private Font titleFont = new Font("微软雅黑", 10.5f);
        [Category("KT"), Description("标题字体")]
        public Font K_TitleTextFont
        {
            get { return titleFont; }
            set
            {
                titleFont = value;
                Invalidate(_titleBarBounds);
            }
        }

        private Rectangle titleBounds = new Rectangle(30, 3, 500, 24);
        [Category("KT"), Description("标题文本限制范围，高最大100像素，宽最大500像素")]
        public Rectangle K_TitleTextBounds
        {
            get { return titleBounds; }
            set
            {
                if (value.X > Width)
                {
                    value.X = Width;
                }
                if (value.Y > K_TitleBarHeight)
                {
                    value.Y = K_TitleBarHeight;
                }
                if (value.Width > 500)
                {
                    value.Width = 500;
                }
                if (value.Height > 100)
                {
                    value.Width = 100;
                }
                titleBounds = value;
                Invalidate(_titleBarBounds);
            }
        }

        private int titleBarHeight = 30;
        [Category("KT"), Description("标题栏区域高度")]
        public int K_TitleBarHeight
        {
            get { return titleBarHeight; }
            set
            {
                if (value < 0) value = 0;
                if (value > Height) value = Height;
                titleBarHeight = value;
                //_titleBarBounds = new Rectangle(0, 0, Width, K_TitleBarHeight);
                if (titleBarHeight != 0)
                {
                    _titleBarBounds = new Rectangle(-1, -1, Width + 1, K_TitleBarHeight + 1);
                }
                else
                {
                    _titleBarBounds = new Rectangle(0, 0, 0, 0);
                }

                Invalidate(_titleBarBounds);
            }
        }

        private int menuBarHeight = 0;
        [Category("KT"), Description("菜单栏区域高度,取值（0-100像素）")]
        public int K_MenuBarHeight
        {
            get { return menuBarHeight; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 100)
                {
                    value = 100;
                }
                menuBarHeight = value;
                //_menuBarBounds = new Rectangle(0, K_TitleBarHeight - 1, Width, K_MenuBarHeight);
                if (menuBarHeight != 0)
                {
                    _menuBarBounds = new Rectangle(-1, K_TitleBarHeight - 1, Width + 1, K_MenuBarHeight + 1);
                }
                else
                {
                    _menuBarBounds = new Rectangle(0, 0, 0, 0);
                }

                Invalidate();
            }
        }

        private int stateBarHeight = 0;
        [Category("KT"), Description("状态栏区域高度,取值（0-100像素）")]
        public int K_StateBarHeight
        {
            get { return stateBarHeight; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 100)
                {
                    value = 100;
                }
                stateBarHeight = value;
                if (stateBarHeight != 0)
                {
                    _stateBarBounds = new Rectangle(-1, Height - K_StateBarHeight, Width + 1, K_StateBarHeight + 1);
                }
                else
                {
                    _stateBarBounds = new Rectangle(0, 0, 0, 0);
                }
                //_stateBarBounds = new Rectangle(0, Height - K_StateBarHeight, Width, K_StateBarHeight);

                Invalidate();
            }
        }

        private Image _icon;
        [Category("KT"), Description("界面图标，可以是ico、png、jpeg等类型")]
        public Image K_Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                Invalidate(_titleBarBounds);
            }
        }

        private Rectangle iconBounds = new Rectangle(new Point(3, 3), new Size(23, 23));
        [Category("KT"), Description("图标绘制的限制范围，高宽限制100像素，X100，Y小于标题栏高度")]
        public Rectangle K_IconBounds
        {
            get { return iconBounds; }
            set
            {
                if (iconBounds.X > 100)
                {
                    iconBounds.X = 100;
                }
                if (iconBounds.Y > K_TitleBarHeight)
                {
                    iconBounds.Y = K_TitleBarHeight;
                }
                if (iconBounds.Width > 100)
                {
                    iconBounds.Width = 100;
                }
                if (iconBounds.Height > 100)
                {
                    iconBounds.Height = 100;
                }
                iconBounds = value;
                Invalidate(_titleBarBounds);
            }
        }

        [Category("KT"), Description("窗口第一次打开时的模式")]
        public FormWindowState K_WindowState { get; set; }

        private bool _IsShowText;
        [Category("KT"), Description("是否窗口显示标题")]
        public bool K_IsShowText
        {
            get { return _IsShowText; }
            set { _IsShowText = value; Invalidate(_titleBarBounds); }
        }

        [Browsable(false),Category("KT"), Description("窗口是否是最大化状态")]
        public bool K_IsMaximized
        {
            get
            {
                return _formIsMaximized;
            }
            set
            {
                _formIsMaximized = value;
            }
        }

        #endregion

        #region 〓〓〓〓〓〓 常量

        private const uint TPM_LEFTALIGN = 0x0000;
        private const uint TPM_RETURNCMD = 0x0100;

        private const int WM_SYSCOMMAND = 0x0112;

        private const int BORDER_WIDTH = 7;

        //系统消息
        public const int WM_PALETTECHANGED = 0x0311;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_NCHITTEST = 0x0084;

        public const int HT_CAPTION = 2;
        public const int HT_LEFT = 10;
        public const int HT_RIGHT = 11;
        public const int HT_TOP = 12;
        public const int HT_TOPLEFT = 13;
        public const int HT_TOPRIGHT = 14;
        public const int HT_BOTTOM = 15;
        public const int HT_BOTTOMLEFT = 16;
        public const int HT_BOTTOMRIGHT = 17;

        private const int WMSZ_TOP = 3;
        private const int WMSZ_TOPLEFT = 4;
        private const int WMSZ_TOPRIGHT = 5;
        private const int WMSZ_LEFT = 1;
        private const int WMSZ_RIGHT = 2;
        private const int WMSZ_BOTTOM = 6;
        private const int WMSZ_BOTTOMLEFT = 7;
        private const int WMSZ_BOTTOMRIGHT = 8;

        private const int MONITOR_DEFAULTTONEAREST = 2;

        private const int WS_MINIMIZEBOX = 0x20000;
        private const int WS_SYSMENU = 0x00080000;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public class MONITORINFOEX
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public int Width()
            {
                return right - left;
            }

            public int Height()
            {
                return bottom - top;
            }
        }

        #endregion

        #endregion

        #region ■------------------ 构造加载

        public KFrmBase()
        {
            InitializeComponent();
            SetStyles();

            //pic关闭 = new KControls.KPictureBox();
            //pic最大化 = new KControls.KPictureBox();
            //pic最小化 = new KControls.KPictureBox();
            //this.Controls.Add(this.pic最小化);
            //this.Controls.Add(this.pic最大化);
            //this.Controls.Add(this.pic关闭);

            //// 
            //// pic关闭
            //// 
            //this.pic关闭.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.pic关闭.BackColor = System.Drawing.Color.Transparent;
            //this.pic关闭.BackgroundImage = Resources.关闭;
            //this.pic关闭.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            //this.pic关闭.K_Radiuse = 3F;
            ////this.pic关闭.Location = new System.Drawing.Point(1332, 5);
            //pic关闭.Location = new System.Drawing.Point(Width - 25 - 2, 5);
            //this.pic关闭.Name = "pic关闭";
            //this.pic关闭.Size = new System.Drawing.Size(25, 25);
            //this.pic关闭.TabIndex = 3;
            //this.pic关闭.TabStop = false;
            //this.pic关闭.Click += new System.EventHandler(this.pic关闭_Click);
            //// 
            //// pic最大化
            //// 
            //this.pic最大化.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.pic最大化.BackColor = System.Drawing.Color.Transparent;
            //this.pic最大化.BackgroundImage = Resources.最大;
            //this.pic最大化.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            //this.pic最大化.K_Radiuse = 3F;
            ////this.pic最大化.Location = new System.Drawing.Point(1301, 5);
            //pic最大化.Location = new System.Drawing.Point(Width - 25 * 2 - 2 - 6, 5);
            //this.pic最大化.Name = "pic最大化";
            //this.pic最大化.Size = new System.Drawing.Size(25, 25);
            //this.pic最大化.TabIndex = 4;
            //this.pic最大化.TabStop = false;
            //this.pic最大化.Click += new System.EventHandler(this.pic最大化_Click);
            //pic最大化.Visible = MaximizeBox;
            //// 
            //// pic最小化
            //// 
            //this.pic最小化.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.pic最小化.BackColor = System.Drawing.Color.Transparent;
            //this.pic最小化.BackgroundImage = Resources.最小;
            //this.pic最小化.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            //this.pic最小化.K_Radiuse = 3F;
            ////this.pic最小化.Location = new System.Drawing.Point(1270, 5);
            //if (MaximizeBox)
            //    pic最小化.Location = new System.Drawing.Point(Width - 25 * 3 - 2 - 12, 5);
            //else
            //    pic最小化.Location = new System.Drawing.Point(Width - 25 * 2 - 2 - 6, 5);
            //this.pic最小化.Name = "pic最小化";
            //this.pic最小化.Size = new System.Drawing.Size(25, 25);
            //this.pic最小化.TabIndex = 5;
            //this.pic最小化.TabStop = false;
            //this.pic最小化.Click += new System.EventHandler(this.pic最小化_Click);
            //pic最小化.Visible = MinimizeBox;

            K_ResizeAble = true;//可拖动拉大拉小窗口
            K_WindowState = FormWindowState.Normal;

            //鼠标从拖动窗体大小的状态中恢复  屏蔽 鼠标从外面移到边框再移动子控件上不会恢复鼠标图标状态
            Application.AddMessageFilter(new MouseMessageFilter());
            MouseMessageFilter.MouseMove += OnGlobalMouseMove;
        }

        private void pic最小化_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pic最大化_Click(object sender, EventArgs e)
        {
            MaximizeWindow(!K_IsMaximized);
        }

        private void pic关闭_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            UpdateStyles();//强制分配样式重新应用到控件上
            base.AutoScaleMode = AutoScaleMode.None;
        }

        #endregion

        #region ■------------------ WindowAPI

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);

        #endregion

        #region ■------------------ 重写父类方法

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e); 放这里会先触发事件，也就是Paint事件处理方法的绘制，然后再执行后面的绘制可能会覆盖，
            Graphics formGraphics = e.Graphics;
            Bitmap bufferBitmap = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bufferBitmap);

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.Default;
            g.Clear(BackColor);

            //if (ControlBox)
            //{
            //    pic关闭.Location = new Point(Width - 25 - 2, 5);
            //    if (MaximizeBox)
            //    {
            //        pic最大化.Visible = true;
            //        pic最大化.Location = new Point(Width - 25 * 2 - 2 - 6, 5);
            //        if (MinimizeBox)
            //        {
            //            pic最小化.Visible = true;
            //            pic最小化.Location = new Point(Width - 25 * 3 - 2 - 12, 5);
            //        }
            //        else
            //        {
            //            pic最小化.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        pic最大化.Visible = false;
            //        if (MinimizeBox)
            //        {
            //            pic最小化.Visible = true;
            //            pic最小化.Location = new Point(Width - 25 * 2 - 2 - 6, 5);
            //        }
            //        else
            //        {
            //            pic最小化.Visible = false;
            //        }
            //    }
            //}
            //else
            //{
            //    pic最大化.Visible = false;
            //    pic最小化.Visible = false;
            //    pic关闭.Visible = false;
            //}


            #region 〓〓〓〓〓〓 绘制标题栏|菜单栏|状态栏区域（纯色）

            if (K_IsFollowTheme)
            {
                //g.FillRectangle(SkinManager.MatchColor.BrushPrimary, _titleBarBounds);//填充标题栏
                //g.FillRectangle(SkinManager.MatchColor.BrushSecond, _menuBarBounds);  //填充菜单栏
                //g.FillRectangle(SkinManager.MatchColor.BrushSecond, _stateBarBounds); //填充状态栏
            }
            else
            {
                if (K_TitleBarHeight > 0)
                {
                    using (SolidBrush brush = new SolidBrush(K_ColorTitleBar))
                    {
                        g.FillRectangle(brush, _titleBarBounds);//填充标题栏
                    }
                }
                if (K_StateBarHeight > 0)
                {
                    using (SolidBrush brush = new SolidBrush(K_ColorStateBar))
                    {
                        g.FillRectangle(brush, _menuBarBounds);  //填充菜单栏
                        g.FillRectangle(brush, _stateBarBounds); //填充状态栏
                    }
                }

            }

            #endregion

            #region 〓〓〓〓〓〓 绘制边框

            if (K_IsShowFormBorder)
            {
                //绘制边框
                if (!_formIsMaximized)
                {
                    using (Pen p = new Pen(K_FormBorderColor, K_FormBorderWidth))
                    {
                        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
                    }
                }
            }

            #endregion

            #region 〓〓〓〓〓〓 绘制标题

            if (K_IsShowText)
            {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                using (Brush brush = new SolidBrush(K_TitleTextColor))
                {
                    g.DrawString(Text, K_TitleTextFont, brush, K_TitleTextBounds, sf);
                }
            }

            #endregion

            #region 〓〓〓〓〓〓 绘制图标

            if (ShowIcon)
            {
                if (K_Icon != null)
                {
                    g.DrawImage(K_Icon, K_IconBounds);
                }
                else
                {
                    if (base.Icon != null)
                    {
                        g.DrawImage(base.Icon.ToBitmap(), K_IconBounds);
                    }
                }
            }

            #endregion

            formGraphics.DrawImage(bufferBitmap, 0, 0);
            g.Dispose();

            base.OnPaint(e);
        }

        //Show或Hide被调用时：涉及阴影
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (K_IsShowShadow)
            {
                if (Visible)
                {
                    if (!DesignMode && _frmShadow == null)
                    {
                        _frmShadow = new KFrmShadow(this);
                        _frmShadow.Show(this);
                        _frmShadow.TopMost = TopMost;
                    }

                    base.OnVisibleChanged(e);

                }
                else
                {
                    base.OnVisibleChanged(e);
                }
            }
        }

        //窗体关闭时：涉及阴影
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (e.Cancel != true)
            {
                //先关闭阴影窗体
                if (_frmShadow != null)
                {
                    _frmShadow.Close();
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _titleBarBounds = new Rectangle(-1, -1, Width + 1, K_TitleBarHeight + 1);
            _menuBarBounds = new Rectangle(-1, K_TitleBarHeight - 1, Width + 1, K_MenuBarHeight + 1);
            _stateBarBounds = new Rectangle(-1, Height - K_StateBarHeight, Width + 1, K_StateBarHeight + 1);

            Invalidate();
        }

        //窗体大小改变时
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (DesignMode || IsDisposed) return;

            if (m.Msg == WM_LBUTTONDBLCLK)//双击
            {
                Point mousePoint = PointToClient(Cursor.Position);
                if (_titleBarBounds.Contains(mousePoint))
                {
                    MaximizeWindow(!K_IsMaximized);
                }
            }
            else if (m.Msg == WM_MOUSEMOVE)
            {
                if (K_IsMaximized && _titleBarBounds.Contains(Cursor.Position))
                {
                    if (_headerMouseDown && _headerMouseDownPoint != Cursor.Position)
                    {
                        K_IsMaximized = false;
                        _headerMouseDown = false;

                        Point mousePoint = PointToClient(Cursor.Position);
                        if (mousePoint.X < Width / 2)
                            Location = mousePoint.X < previousSize.Width / 2 ?
                                new Point(Cursor.Position.X - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                                new Point(Cursor.Position.X - previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);
                        else
                            Location = Width - mousePoint.X < previousSize.Width / 2 ?
                                new Point(Cursor.Position.X - previousSize.Width + Width - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                                new Point(Cursor.Position.X - previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);

                        Size = previousSize;
                        ReleaseCapture();
                        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    }
                }
            }
            else if (m.Msg == WM_LBUTTONDOWN)
            {
                if (_titleBarBounds.Contains(PointToClient(Cursor.Position)))
                {
                    if (!K_IsMaximized)
                    {
                        ReleaseCapture();
                        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    }
                    else
                    {
                        _headerMouseDown = true;
                        _headerMouseDownPoint = Cursor.Position;
                    }
                }
            }
            else if (m.Msg == WM_RBUTTONDOWN)//鼠标右击
            {
                //Point cursorPos = PointToClient(Cursor.Position);//获取鼠标在工作区域内点击的坐标

                //if (_titleBounds.Contains(cursorPos))
                //{
                //    // Show default system menu when right clicking titlebar
                //    int id = TrackPopupMenuEx(
                //        GetSystemMenu(Handle, false),
                //        TPM_LEFTALIGN | TPM_RETURNCMD,
                //        Cursor.Position.X, Cursor.Position.Y, Handle, IntPtr.Zero);

                //    // Pass the command as a WM_SYSCOMMAND message
                //    SendMessage(Handle, WM_SYSCOMMAND, id, 0);
                //}
            }
            else if (m.Msg == WM_NCLBUTTONDOWN)
            {
                ////是否允许通过边来改变窗体的大小
                //if (!K_ResizeAble) return;

                //byte bFlag = 0;

                //// 获取是从哪条边改变窗体大小
                //if (resizingLocationsToCmd.ContainsKey((int)m.WParam))
                //{
                //    bFlag = (byte)resizingLocationsToCmd[(int)m.WParam];
                //}

                //if (bFlag != 0)
                //{
                //    SendMessage(Handle, WM_SYSCOMMAND, 0xF000 | bFlag, (int)m.LParam);//发消息给Form改变Form大小
                //}


                // This re-enables resizing by letting the application know when the
                // user is trying to resize a side. This is disabled by default when using WS_SYSMENU.
                if (!K_ResizeAble) return;

                byte bFlag = 0;

                // Get which side to resize from
                if (resizingLocationsToCmd.ContainsKey((int)m.WParam))
                    bFlag = (byte)resizingLocationsToCmd[(int)m.WParam];

                if (bFlag != 0)
                    SendMessage(Handle, WM_SYSCOMMAND, 0xF000 | bFlag, (int)m.LParam);
            }
            else if (m.Msg == WM_LBUTTONUP)
            {
                _headerMouseDown = false;
            }
            else if (m.Msg == WM_PALETTECHANGED)
            {

            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams par = base.CreateParams;
                par.Style = par.Style | WS_MINIMIZEBOX | WS_SYSMENU;
                return par;
            }
        }

        #region 〓〓〓〓〓〓〓 鼠标事件

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (DesignMode) return;

            base.OnMouseDown(e);

            UpdateButtons(e);

            if (e.Button == MouseButtons.Left && !_formIsMaximized)//左键且窗口不是最大化状态
            {
                ResizeForm(_resizeDir);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (DesignMode) return;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            if (K_ResizeAble)
            {
                bool isChildUnderMouse = GetChildAtPoint(e.Location) != null;

                if (e.Location.X < BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_formIsMaximized)
                {
                    _resizeDir = KResizeDirection.BottomLeft;
                    Cursor = Cursors.SizeNESW;
                }
                else if (e.Location.X < BORDER_WIDTH && !isChildUnderMouse && !_formIsMaximized)
                {
                    _resizeDir = KResizeDirection.Left;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_formIsMaximized)
                {
                    _resizeDir = KResizeDirection.BottomRight;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && !isChildUnderMouse && !_formIsMaximized)
                {
                    _resizeDir = KResizeDirection.Right;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_formIsMaximized)
                {
                    _resizeDir = KResizeDirection.Bottom;
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    _resizeDir = KResizeDirection.None;

                    if (_resizeCursors.Contains(Cursor))
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }

            UpdateButtons(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DesignMode) return;
            UpdateButtons(e, true);

            base.OnMouseUp(e);
            ReleaseCapture();
        }

        #endregion

        #endregion

        #region ■------------------ 其它方法

        /// <summary>
        /// 改变窗体的大小：拖动边框改变
        /// </summary>
        /// <param name="direction">改变的方向</param>
        private void ResizeForm(KResizeDirection direction)
        {
            if (DesignMode) return;
            int dir = -1;
            switch (direction)
            {
                case KResizeDirection.BottomLeft:
                    dir = HT_BOTTOMLEFT;
                    break;
                case KResizeDirection.Left:
                    dir = HT_LEFT;
                    break;
                case KResizeDirection.Right:
                    dir = HT_RIGHT;
                    break;
                case KResizeDirection.BottomRight:
                    dir = HT_BOTTOMRIGHT;
                    break;
                case KResizeDirection.Bottom:
                    dir = HT_BOTTOM;
                    break;
            }

            ReleaseCapture();

            if (dir != -1)
            {
                SendMessage(Handle, WM_NCLBUTTONDOWN, dir, 0);
            }
        }

        /// <summary>
        /// 将父窗口的图标作为子窗口的图标，以及蒙版功能
        /// </summary>
        /// <param name="parentFormEx"></param>
        /// <param name="frmDamBoard">需要蒙板的窗体</param>
        public void SetIcon(Form parentFormEx, Form frmDamBoard = null)
        {
            if (frmDamBoard != null)
            {
                ParentFormEx = frmDamBoard;
            }
            else
            {
                ParentFormEx = parentFormEx;
            }
            if (parentFormEx != null && parentFormEx.Icon != null)
            {
                Icon = parentFormEx.Icon;
            }
        }

        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="maximize"></param>
        public void MaximizeWindow(bool maximize)
        {
            if (!MaximizeBox)
            {
                return;
            }

            K_IsMaximized = maximize;

            if (maximize)
            {
                IntPtr monitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
                MONITORINFOEX monitorInfo = new MONITORINFOEX();
                GetMonitorInfo(new HandleRef(null, monitorHandle), monitorInfo);
                previousSize = Size;
                previousLocation = Location;
                Size = new Size(monitorInfo.rcWork.Width(), monitorInfo.rcWork.Height());
                Location = new Point(monitorInfo.rcWork.left, monitorInfo.rcWork.top);

                SetReion(1);
            }
            else
            {
                Size = previousSize;
                Location = previousLocation;

                SetReion(3);
            }
            if (K_WindowStateChanged != null)
            {
                K_WindowStateChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// 窗体圆角 已屏蔽
        /// </summary>
        /// <param name="radius"></param>
        private void SetReion(int radius)
        {
            //using (GraphicsPath path = DrawHelper.CreatePath(new Rectangle(Point.Empty, base.Size), radius, 1, true))
            //{
            //    _radius = radius;
            //    Region region = new Region(path);
            //    path.Widen(Pens.White);
            //    region.Union(path);
            //    this.Region = region;
            //}
        }

        /// <summary>
        /// 更改鼠标在标题栏的状态 已屏蔽 标题栏最大化最小化关闭按钮不进行绘制
        /// </summary>
        /// <param name="e"></param>
        /// <param name="up"></param>
        private void UpdateButtons(MouseEventArgs e, bool up = false)
        {
            //if (DesignMode) return;

            //KButtonState oldState = _buttonState;

            //bool showMin = MinimizeBox && ControlBox;
            //bool showMax = MaximizeBox && ControlBox;

            //if (e.Button == MouseButtons.Left && !up)
            //{
            //    if (showMin && !showMax && _maxBtnBounds.Contains(e.Location))
            //        _buttonState = KButtonState.MinDown;
            //    else if (showMin && showMax && _minBtnBounds.Contains(e.Location))
            //        _buttonState = KButtonState.MinDown;
            //    else if (showMax && _maxBtnBounds.Contains(e.Location))
            //        _buttonState = KButtonState.MaxDown;
            //    else if (ControlBox && _xBtnBounds.Contains(e.Location))
            //        _buttonState = KButtonState.XDown;
            //    else
            //        _buttonState = KButtonState.None;
            //}
            //else
            //{
            //    if (showMin && !showMax && _maxBtnBounds.Contains(e.Location))
            //    {
            //        _buttonState = KButtonState.MinOver;

            //        if (oldState == KButtonState.MinDown && up)
            //            WindowState = FormWindowState.Minimized;
            //    }
            //    else if (showMin && showMax && _minBtnBounds.Contains(e.Location))
            //    {
            //        _buttonState = KButtonState.MinOver;

            //        if (oldState == KButtonState.MinDown && up)
            //            WindowState = FormWindowState.Minimized;
            //    }
            //    else if (MaximizeBox && ControlBox && _maxBtnBounds.Contains(e.Location))
            //    {
            //        _buttonState = KButtonState.MaxOver;

            //        if (oldState == KButtonState.MaxDown && up)
            //            MaximizeForm(!_formIsMaximized);

            //    }
            //    else if (ControlBox && _xBtnBounds.Contains(e.Location))
            //    {
            //        _buttonState = KButtonState.XOver;

            //        if (oldState == KButtonState.XDown && up)
            //            Close();
            //    }
            //    else _buttonState = KButtonState.None;
            //}

            //if (oldState != _buttonState) Invalidate(_titleBarBounds);

        }

        #endregion











        /// <summary>
        /// MouseMessageFilter.MouseMove += OnGlobalMouseMove;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnGlobalMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDisposed)
            {
                //通过鼠标拖动来变换应用程序的位置 
                Point clientCursorPos = PointToClient(e.Location);//将指定屏幕点的位置计算成工作区坐标。
                MouseEventArgs new_e = new MouseEventArgs(MouseButtons.None, 0, clientCursorPos.X, clientCursorPos.Y, 0);
                OnMouseMove(new_e);
            }
        }
    }

    /// <summary>
    /// 鼠标消息过滤器类 【IMessageFilter允许您停止引发的事件或在调用事件处理程序之前执行特殊操作】
    /// </summary>
    public class MouseMessageFilter : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public static event MouseEventHandler MouseMove;

        /// <summary>
        /// 在调度消息之前将其筛选出来。
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)//鼠标移动消息
            {
                if (MouseMove != null)
                {
                    int x = Control.MousePosition.X, y = Control.MousePosition.Y;

                    MouseMove(null, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
                }
            }
            return false;// 返回结果:
                         // 如果筛选消息并禁止消息被调度，则为 true；如果允许消息继续到达下一个筛选器或控件，则为 false。
        }
    }
    public enum KResizeDirection { None, Top, TopLeft, TopRight, Left, BottomLeft, Bottom, BottomRight, Right }
}
