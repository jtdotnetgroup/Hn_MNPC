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
    public partial class FrmIndex : Form
    {
        ApiService.APIServiceClient _service;
        public FrmMainB MainForm;
        public FrmIndex()
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);

        }

        private void FrmIndex_Load(object sender, EventArgs e)
        {
            gridControl请购计划列表.DataSource = _service.GetPurchasePlanList(Global.LoginUser, "", "", 3, "", "","","",false);
            gridControl待发货.DataSource = _service.GetDeliveryList(Global.LoginUser, "", "", "", 3, "", "", "", "", "", "", "", "","","", false);
        }

        private void FrmIndex_Shown(object sender, EventArgs e)
        {
            pnlLayout1.Width = xtraScrollableControl1.Width / 3;
            pnlLayout2.Width = xtraScrollableControl1.Width / 3;
            pnlLayout3.Width = xtraScrollableControl1.Width / 3;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenChildForm(new FrmPleasePurchasePlan(3));
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenChildForm(new FrmSendGoodsPlan(3));
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenChildForm(new FrmSendGoodsPlan(3));
        }
    }
}
