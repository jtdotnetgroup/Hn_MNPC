using hn.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MApiModel;
using System.Drawing;

namespace hn.Client
{
    public static class Helper
    {
        public static frm库存查询 frmQuery = new frm库存查询();

        public static void ShowQuery(bool bShow=true)
        {
            if (frmQuery.Visible)
            {
                frmQuery.Close();
                frmQuery = new frm库存查询();
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
            }
            else if(bShow)
            {
                frmQuery = new frm库存查询();
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
            }
        }
    }
}
