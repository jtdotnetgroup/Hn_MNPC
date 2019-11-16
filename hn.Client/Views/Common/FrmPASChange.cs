using hn.Core;
using System;
using System.Windows.Forms;

namespace hn.Client
{
    public partial class FrmPASChange : FrmBase
    {
        ApiService.APIServiceClient _service;

        public FrmPASChange()
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOld.Text == "")
                {
                    MsgHelper.ShowInformation("请输入旧密码！");
                    return;
                }

                if (txtNew1.Text == "")
                {
                    MsgHelper.ShowInformation("请输入新密码！");
                    return;
                }

                if (txtNew1.Text != txtNew2.Text)
                {
                    MsgHelper.ShowInformation("两次输入的密码不一致！");
                    return;
                }


                JsonMessage json = JsonHelper.ConvertToObject<JsonMessage>(_service.ModifyPassword(Global.LoginUser.FID, txtOld.Text, txtNew1.Text));
                if (json.Success)
                {
                    MsgHelper.ShowInformation("密码修改成功！");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MsgHelper.ShowError(json.Message);
                }
                
            }
            catch (Exception ex)
            {
                LogError(ex, GetType());
            }
        }

        private void btnViewPAS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtOld.Properties.PasswordChar = !btnViewPAS.Checked ? '*' : '\0';
                txtNew1.Properties.PasswordChar = !btnViewPAS.Checked ? '*' : '\0';
                txtNew2.Properties.PasswordChar = !btnViewPAS.Checked ? '*' : '\0';
                btnViewPAS.Text = btnViewPAS.Checked ? "隐藏密码" : "显示密码";
            }
            catch (Exception ex)
            {
                LogError(ex, GetType());
            }
        }

    }
}
