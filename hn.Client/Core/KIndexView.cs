using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hn.Client.Core
{
    public partial class KIndexView : UserControl
    {
        public KIndexView()
        {
            InitializeComponent();
        }

        [Category("K")]
        public string K_Text
        {
            get { return lblTitle.Text; }
            set {
                if (value!=null)
                {
                    lblTitle.Text = value.Trim();
                }
            }
        }

        private void KIndexView_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen=new Pen(Color.Black))
            {
                //e.Graphics.DrawLine(pen,);
            }
        }
    }
}
