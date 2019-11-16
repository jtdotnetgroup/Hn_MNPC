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
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();

            txtUrl.Text = IniHelper.ReadString(Global.IniUrl, "CONFIG", "URL", "");
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            IniHelper.WriteString(Global.IniUrl, "CONFIG", "URL", txtUrl.Text);

            Global.WcfUrl = txtUrl.Text;

            this.Close();
        }
    }
}
