using hn.Client.Core;
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
    public partial class FrmPPPConfirm : FrmBase
    {
        public DevExpress.XtraGrid.Views.Grid.GridView ParentGridView;
        ApiService.APIServiceClient _service;
        private string _Action = "";
        private V_ICPRBILLENTRYMODEL[] _DataSource;
        private string _icprbillid;
        public FrmPPPConfirm(string icprbillid, string action)
        {
            InitializeComponent();

            this._icprbillid = icprbillid;
            this._Action = action;
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            _DataSource = _service.GetPurchasePlanEntryList(icprbillid, action == "unconfirm" ? "7" : "3",false);
            foreach (var model in _DataSource)
            {
                model.FCOMMITQTY = model.FORDERUNITQTY;
            }
            gridControl请购计划明细.DataSource = _DataSource;

            if (_Action == "unconfirm")
            {
                btn采购确认.Text = "反确认";
            }

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
            txt经营场所.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FPREMISENAME").ToStr();
            txt品牌厂家.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FBRANDNAME").ToStr();
            txt单据编号.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FBILLNO").ToStr();
            txt申请日期.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FDATE").ToStr();
            txt申请人.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FBILLERNAME").ToStr();
            txt计划类型.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FTYPENAME").ToStr();
            txt联系电话.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FTELEPHONE").ToStr();
            txt签约主体.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "SIGN_MAIN").ToStr();
            txt运输方式.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FTRANSNAME").ToStr();
            txt运输结算.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FFREIGHTNAME").ToStr();
            txt收货地址.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FRECEIVINGADDR").ToStr();
            txt工程名称.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FPROJECTNAME").ToStr();
            txtJDE地址号.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "JDE").ToStr();
            txtCRM单号.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "CRMNO").ToStr();
            txt发货要求.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FREMARK").ToStr();
            txt采购订单.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FPURCHASE_NO").ToStr();
            txt结算分部号.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FSETTLE_ORG").ToStr();
            txt发货地点.Text = ParentGridView.GetRowCellValue(ParentGridView.FocusedRowHandle, "FDELIVERYADDR").ToStr();
        }

        private void btn采购确认_Click(object sender, EventArgs e)
        {
            int count = 0;
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
                        FCOMMITQTY = commitqty.ToDecimal(),
                        FORDERREMARK1 = orderremark1,
                        FORDERREMARK2 = orderremark2,
                        FCONFIRM_USER = Global.LoginUser.FID,
                        FCONFIRM_TIME = DateTime.Now
                });

                    if (list.Count == 50)
                    {
                        bool ret = _service.ConfirmSave(_Action, list.ToArray(), Global.LoginUser);
                        if (ret)
                        {
                            list = new List<ICPRBILLENTRYMODEL>();
                        }
                    }

                    count++;
                }
            }

            //if (list.Count > 0)
            //{
            //    bool ret = _service.ConfirmSave(_Action, list.ToArray(), Global.LoginUser);
            //    if (ret)
            //    {
            //        MsgHelper.ShowInformation("处理成功！");
            //        this.DialogResult = DialogResult.OK;
            //        this.Close();
            //    }
            //}

            if (count == 0)
            {
                MsgHelper.ShowInformation("请先勾选你要确认的数据！");
            }
            else
            {
                bool ret = _service.ConfirmSave(_Action, list.ToArray(), Global.LoginUser);
                if(ret)
                    MsgHelper.ShowInformation("处理成功！");

                //var fplanId = list.First().FID;
                // 更新计划单的采购确认时间----2019-08-05
                //_service.UpdateConfirmTime(fplanId);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn保存_Click(object sender, EventArgs e)
        {
         //  int count = 0;

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
                        FCOMMITQTY = commitqty.ToDecimal(),
                        FORDERREMARK1 = orderremark1,
                        FORDERREMARK2 = orderremark2
                    });

                    if (list.Count == 50)
                    {
                        bool ret = _service.ConfirmSave("", list.ToArray(), Global.LoginUser);
                        if (ret)
                        {
                            list = new List<ICPRBILLENTRYMODEL>();
                        }
                    }
                }
            }
            
            _service.ConfirmSave("", list.ToArray(), Global.LoginUser);
            MsgHelper.ShowInformation("保存成功！");
        }

        private void gridView请购计划明细_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

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

        private void btn查询_Click(object sender, EventArgs e)
        {
           

        }

        private void chk全选_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var model in _DataSource)
            {
                model.FCHECK = chk全选.Checked;
            }
            gridControl请购计划明细.DataSource = _DataSource;
            gridControl请购计划明细.RefreshDataSource();

        }

        private void btn附件_Click(object sender, EventArgs e)
        {
            frm请购计划附件 frm = new frm请购计划附件(_icprbillid);
            frm.ShowDialog();
        }

        private void btn发货_Click(object sender, EventArgs e)
        {

        }

        private void btn刷新价格政策编号_Click(object sender, EventArgs e)
        {
            List<string> ids = new List<string>();
            for (int i = 0; i < gridView请购计划明细.RowCount; i++)
            {
                bool b = gridView请购计划明细.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    string fid = gridView请购计划明细.GetRowCellValue(i, "FID").ToStr();

                    ids.Add(fid);
                }
            }

            if (ids.Count > 0)
            {
                _DataSource = _service.RefrshPriceNumberList(_icprbillid, ids.ToArray());
                foreach (var model in _DataSource)
                {
                    model.FCOMMITQTY = model.FORDERUNITQTY;
                }
                gridControl请购计划明细.DataSource = _DataSource;
            }
            else
            {
                MsgHelper.ShowInformation("请先勾选你要处理的数据");
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView请购计划明细.FocusedRowHandle != -1)
            {
                var list = gridControl请购计划明细.DataSource as V_ICPRBILLENTRYMODEL[];
                var row = list[gridView请购计划明细.GetDataSourceRowIndex(gridView请购计划明细.FocusedRowHandle)];

                FrmQueryPrice frm = new FrmQueryPrice(row.FITEMID);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    row.FPRICENUMBER = frm.SelectID;
                    gridControl请购计划明细.DataSource = list;
                    gridControl请购计划明细.RefreshDataSource();
                }
            }
        }
    }
}
