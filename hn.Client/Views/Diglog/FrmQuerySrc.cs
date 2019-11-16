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
    public partial class FrmQuerySrc : FrmBase
    {
        ApiService.APIServiceClient _service;

        private string _ProductID = "";

        public string SelectID;
        public string SelectName;
        public SRCModel SelectData;

        public FrmQuerySrc(string productid)
        {
            InitializeComponent();
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            _ProductID = productid;
        }

        private void FrmQueryMarketArea_Load(object sender, EventArgs e)
        {
            onSearch("");
        }

        private void onSearch(string keyword)
        {
            var list = _service.GetSrcList(_ProductID, keyword);
            gridControl.DataSource = list;
            gridControl.Focus();
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
            onSearch(txt关键字.Text);
        }

        private void btn重置_Click(object sender, EventArgs e)
        {
            txt关键字.Text = "";
            onSearch("");
        }

        private void gridView名称代码_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView名称代码_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView名称代码.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    var list = new List<SRCModel>(gridControl.DataSource as SRCModel[]);
                    this.SelectData = list[gridView名称代码.GetDataSourceRowIndex(gridView名称代码.FocusedRowHandle)];
                    //this.SelectID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FID").ToString();
                   // this.SelectName = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FNAME").ToString();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void FrmQuerySrc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.onSearch(txt关键字.Text);
            }
        }

        private void gridControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var list = new List<SRCModel>(gridControl.DataSource as SRCModel[]);
                this.SelectData = list[gridView名称代码.GetDataSourceRowIndex(gridView名称代码.FocusedRowHandle)];
                //this.SelectID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FID").ToString();
                // this.SelectName = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FNAME").ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
