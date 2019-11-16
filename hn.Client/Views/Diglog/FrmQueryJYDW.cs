using hn.DataAccess.model;
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
    public partial class FrmQueryJYDW : FrmBase
    {
        ApiService.APIServiceClient _service;

     

        public FrmQueryJYDW()
        {
            InitializeComponent();
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
          

        }

        private void FrmQueryMarketArea_Load(object sender, EventArgs e)
        {
            onSearch("");
        }

        private void onSearch(string keyword)
        {
            var list = _service.GetJYDW(keyword);
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

     

        private void FrmQueryDictionary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.onSearch(txt关键字.Text);
            }
        }

        public TB_PREMISEModel result = new TB_PREMISEModel();


        private void gridView名称代码_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView名称代码.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    result.FCLASSAREA2= gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FCLASSAREA2").ToString();
                    result.FNAME= gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FNAME").ToString();
                    result.FID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FID").ToString();
                    result.FCODE =gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FCODE").ToString();
                  
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void gridControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                result.FCLASSAREA2 = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FCLASSAREA2").ToString();
                result.FNAME = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FNAME").ToString();
                result.FID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FID").ToString();
                result.FCODE = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FCODE").ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
