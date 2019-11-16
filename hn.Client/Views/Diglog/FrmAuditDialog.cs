using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hn.Client.Views.Diglog
{
    public partial class FrmAuditDialog : Form
    {
        public FrmAuditDialog(string title, string msg, string button1Text, string button2Text, string button3Text = "取消")
        {
            InitializeComponent();

            this.Text = title;
            this.lbl内容.Text = msg;
            this.btn按钮1.Text = button1Text;
            this.btn按钮2.Text = button2Text;
            this.btn取消.Text = button3Text;
        }

        private void btn按钮1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btn按钮2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btn取消_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
