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
    public partial class FrmInput : Form
    {
        public string Content;
        public FrmInput()
        {
            InitializeComponent();

        }

        private void btn按钮1_Click(object sender, EventArgs e)
        {
            Content = txt内容.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
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
