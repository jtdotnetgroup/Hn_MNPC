using hn.Client.Core;
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
    public partial class FrmTestA : KFrmBase
    {
        public FrmTestA()
        {
            InitializeComponent();
        }

        private void FrmTest_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(
                new Pen(Color.Red,2),
                new Rectangle(1,64,Width-3,Height-64-27)
                );
            e.Graphics.DrawLine(new Pen(Color.Red,5),0,10,Width,10);
        }
    }
}
