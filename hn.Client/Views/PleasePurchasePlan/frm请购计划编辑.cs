using hn.Client.Core;
using hn.Client.Views.Diglog;
using hn.Client.Views.PleasePurchasePlan;
using hn.Common;
using hn.DataAccess.Model;
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
    public partial class frm请购计划编辑 : FrmBase
    {
        public V_ICPRBILLMODEL ICPRBILLData;
        ApiService.APIServiceClient _service;
        public bool IsRowClose = false;
        private string _icprbillid;
        public event EventHandler SaveAfter;

        

        public frm请购计划编辑(string icprbillid)
        {
            InitializeComponent();

            _icprbillid = icprbillid;
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            var list = _service.GetPurchasePlanEntryList(icprbillid, "",false);
            foreach (var model in list)
            {
                model.FCOMMITQTY = model.FORDERUNITQTY;
            }
            gridControl请购计划明细.DataSource = list;

        }

        public void set发货Visible()
        {
            btn发货.Visible = false;
        }

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmPPPConfirm), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmPPPConfirm), msg);
        }

        #endregion

        private void pnl跑龙套1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                using (Pen pen = new Pen(Color.FromArgb(165, 172, 181)))
                {
                    e.Graphics.DrawLine(pen, new Point(0, 0), new Point(pnl跑龙套1.Width, 0));
                    e.Graphics.DrawLine(pen, new Point(0, pnl跑龙套1.Height - 1), new Point(pnl跑龙套1.Width, pnl跑龙套1.Height - 1));

                    e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, pnl跑龙套1.Height));
                    e.Graphics.DrawLine(pen, new Point(pnl跑龙套1.Width - 1, 0), new Point(pnl跑龙套1.Width - 1, pnl跑龙套1.Height));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void btn关闭_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPPPConfirm_Load(object sender, EventArgs e)
        {
            txt经营场所.Text = ICPRBILLData.FPREMISENAME;
            txt品牌厂家.Text = ICPRBILLData.FBRANDNAME;
            txt单据编号.Text = ICPRBILLData.FBILLNO;
            txt申请日期.Text = ICPRBILLData.FDATE.ToString("yyyy-MM-dd");
            txt申请人.Text = ICPRBILLData.FBILLERNAME;
            txt计划类型.Text = ICPRBILLData.FTYPENAME;
            txt联系电话.Text = ICPRBILLData.FTELEPHONE;
            txt签约主体.Text = ICPRBILLData.SIGN_MAIN;
            txt运输方式.Text = ICPRBILLData.FTRANSNAME;
            txt运输结算.Text = ICPRBILLData.FFREIGHTNAME;
            txt收货地址.Text = ICPRBILLData.FRECEIVINGADDR;
            txt工程名称.Text = ICPRBILLData.FPROJECTNAME;
            txtJDE地址号.Text = ICPRBILLData.JDE;
            txtCRM单号.Text = ICPRBILLData.CRMNO;
            txt发货要求.Text = ICPRBILLData.FREMARK;
            txt采购订单.Text = ICPRBILLData.FPURCHASE_NO;
            txt结算分部号.Text = ICPRBILLData.FSETTLE_ORG;
            txt发货地点.Text = ICPRBILLData.FDELIVERYADDR;
            txt标识.Text = ICPRBILLData.FIDENTIFICATION;

            int status = ICPRBILLData.FSTATUS;
            if (status == 7 || status == 5)
            {
                gridColumn采购备注1.AppearanceCell.BackColor = Color.White;
                gridColumn采购备注1.AppearanceCell.BackColor2 = Color.White;
                gridColumn采购备注1.AppearanceCell.ForeColor = Color.Black;

                gridColumn采购备注1.OptionsColumn.AllowEdit = false;

                btn发货.Visible = true;
            }

            if (IsRowClose)
            {
                gridColumn采购备注1.AppearanceCell.BackColor = Color.White;
                gridColumn采购备注1.AppearanceCell.BackColor2 = Color.White;
                gridColumn采购备注1.AppearanceCell.ForeColor = Color.Black;
                gridColumn采购备注1.OptionsColumn.AllowEdit = false;

                gridColumn采购备注2.AppearanceCell.BackColor = Color.White;
                gridColumn采购备注2.AppearanceCell.BackColor2 = Color.White;
                gridColumn采购备注2.AppearanceCell.ForeColor = Color.Black;
                gridColumn采购备注2.OptionsColumn.AllowEdit = false;
            }

            //splitContainerControl1.SplitterPosition = splitContainerControl1.Height - splitContainerControl1.Height / 3;
        }

        private void btn采购确认_Click(object sender, EventArgs e)
        {
            List<ICPRBILLENTRYMODEL> list = new List<ICPRBILLENTRYMODEL>();
            for (int i = 0; i < gridView请购计划明细.RowCount; i++)
            {
                bool b = gridView请购计划明细.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    string fid = gridView请购计划明细.GetRowCellValue(i, "FID").ToStr();
                    string commitqty = gridView请购计划明细.GetRowCellValue(i, "FCOMMITQTY").ToStr();
                    string orderremark1 = gridView请购计划明细.GetRowCellValue(i, "FORDERREMARK1").ToStr();
                    string orderremark2 = gridView请购计划明细.GetRowCellValue(i, "FORDERREMARK2").ToStr();

                    list.Add(new ICPRBILLENTRYMODEL()
                    {
                        FID = fid,
                        FORDERREMARK1 = orderremark1,
                        FORDERREMARK2 = orderremark2
                    });
                }
            }

            if (list.Count > 0)
            {
                bool ret = _service.ConfirmSave("confirm", list.ToArray(), Global.LoginUser);
                if (ret)
                {
                    MsgHelper.ShowInformation("采购确认保存成功！");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MsgHelper.ShowInformation("请先勾选你要确认的数据！");
            }

        }

        private void btn保存_Click(object sender, EventArgs e)
        {

            string closereson = "";
            if (IsRowClose)
            {
                if (!MsgHelper.AskQuestion("确定要关闭选择的明细行吗？"))
                {
                    return;
                }

                FrmInput input = new FrmInput();
                if (input.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                closereson = input.Content;
            }
           
          
                int count = 0;
            List<ICPRBILLENTRYMODEL> list = new List<ICPRBILLENTRYMODEL>();
            for (int i = 0; i < gridView请购计划明细.RowCount; i++)
            {
                bool b = gridView请购计划明细.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    string fid = gridView请购计划明细.GetRowCellValue(i, "FID").ToStr();
                    //string commitqty = gridView请购计划明细.GetRowCellValue(i, "FCOMMITQTY").ToStr();
                    string orderremark1 = gridView请购计划明细.GetRowCellValue(i, "FORDERREMARK1").ToStr();
                    string orderremark2 = gridView请购计划明细.GetRowCellValue(i, "FORDERREMARK2").ToStr();

                    list.Add(new ICPRBILLENTRYMODEL()
                    {
                        FID = fid,
                        FORDERREMARK1 = orderremark1,
                        FORDERREMARK2 = orderremark2
                    });


                    if (list.Count == 50)
                    {
                        bool ret = _service.CloseSave(list.ToArray(), Global.LoginUser, closereson);
                        if (ret)
                        {
                            list = new List<ICPRBILLENTRYMODEL>();
                        }
                    }

                    count++;

                }
            }

            _service.UpdateICPRBILL(new ICPRBILLMODEL() { FID = _icprbillid, FIDENTIFICATION = txt标识.Text });
            if (SaveAfter != null)
            {
                SaveAfter(null, null);
            }


            if (list.Count == 0)
            {
                this.Close();
                //MsgHelper.ShowError("请勾选要处理的数据！");
                return;
            }

            if (IsRowClose)
            {
                if (MsgHelper.AskQuestion("确定要关闭选择的明细行吗？"))
                {
                  

                        bool ret = _service.CloseSave(list.ToArray(), Global.LoginUser, closereson);
                        if (ret)
                        {
                            MsgHelper.ShowInformation("处理成功！");
                            this.DialogResult = DialogResult.OK;

                        }
                    

                }
            }
            else
            {
      

                bool ret = _service.ConfirmSave("", list.ToArray(), Global.LoginUser);
                if (ret)
                {
                    MsgHelper.ShowInformation("保存成功！");
                 
                    //this.Close();
                }
            }


           

        }

        private void gridView请购计划明细_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
              
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          
        }

        private void gridView请购计划明细_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                //if (chk实时刷新.Checked)
                //{
                //    if (backgroundWorker1.IsBusy)
                //    {
                //        backgroundWorker1.CancelAsync();
                //    }
                //    backgroundWorker1.RunWorkerAsync();
                //}
               
            }
        }

     
        private void btn附件_Click(object sender, EventArgs e)
        {
            frm请购计划附件 frm = new frm请购计划附件(_icprbillid);
            frm.ShowDialog();
        }

        private void btn发货_Click(object sender, EventArgs e)
        {
            int status = ICPRBILLData.FSTATUS;
            if (status != 7)
            {
                MsgHelper.ShowInformation("单据未确认，无法发货！");
                return;
            }


            FrmPPPImmediateSendGoods frm = new FrmPPPImmediateSendGoods();
            frm.请购计划Data = this.ICPRBILLData;
            frm.请购计划明细Data = this.gridView请购计划明细.DataSource as hn.DataAccess.Model.V_ICPRBILLENTRYMODEL[];
            frm.SaveAfter += Frm_SaveAfter;
            frm.Show();
        }

        private void Frm_SaveAfter(object sender, EventArgs e)
        {
            if (SaveAfter != null)
            {
                SaveAfter(null, null);
            }
        }

        private void chk全选_CheckedChanged(object sender, EventArgs e)
        {
            var list = this.gridView请购计划明细.DataSource as hn.DataAccess.Model.V_ICPRBILLENTRYMODEL[];
            foreach (var model in list)
            {
                model.FCHECK = chk全选.Checked;
            }
            gridControl请购计划明细.DataSource = list;
            gridControl请购计划明细.RefreshDataSource();
        }
    }
}
