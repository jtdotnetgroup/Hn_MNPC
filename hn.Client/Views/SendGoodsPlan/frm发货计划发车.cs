using hn.Client.Core;
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
    public partial class FrmSGPArrangeCar : FrmBase
    {
        #region ■------------------ 字段相关

        #endregion

        #region ■------------------ 构造加载

        public FrmSGPArrangeCar()
        {
            InitializeComponent();
            textEdit1.Focus();
        }

        #endregion

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmSGPArrangeCar), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmSGPArrangeCar), msg);
        }

        #endregion

        private void btnAPPClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btn生成发货计划_Click(object sender, EventArgs e)
        {

        }

        private void pnl跑龙套1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                using (Pen pen = new Pen(Color.FromArgb(165, 172, 181)))
                {
                    //e.Graphics.DrawLine(pen, new Point(0, 0), new Point(pnl跑龙套3.Width, 0));
                    e.Graphics.DrawLine(pen, new Point(0, pnl跑龙套1.Height - 1), new Point(pnl跑龙套1.Width, pnl跑龙套1.Height - 1));

                    //e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, pnl跑龙套3.Height));
                    //e.Graphics.DrawLine(pen, new Point(pnl跑龙套3.Width - 1, 0), new Point(pnl跑龙套3.Width - 1, pnl跑龙套3.Height));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryMarketArea frm = new FrmQueryMarketArea();
            frm.ShowDialog();
        }

        private void searchControl2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryMarketArea frm = new FrmQueryMarketArea();
            frm.ShowDialog();
        }
    }
}
