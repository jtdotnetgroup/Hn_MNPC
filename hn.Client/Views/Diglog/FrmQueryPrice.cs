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
    public partial class FrmQueryPrice : FrmBase
    {
        ApiService.APIServiceClient _service;

        private string _ProductID = "";

        public string SelectID;
        public string SelectName;

        public string SelectType = "product";

        public FrmQueryPrice(string productid,string sType="product")
        {
            InitializeComponent();
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            _ProductID = productid;
            SelectType = sType;
        }

        private void FrmQueryMarketArea_Load(object sender, EventArgs e)
        {
            onSearch("");
        }

        private void onSearch(string keyword)
        {
            if (SelectType == "product")
            {
                var list = _service.GetPriceNumberListByItemID(_ProductID);
                gridControl.DataSource = list;
                gridControl.Focus();
            }
            else
            {
                var list = _service.GetPriceNumberListByBrandID(_ProductID);
                gridControl.DataSource = list;
                gridControl.Focus();
            }
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
                    //var list = new List<TB_PRICEPOLICYModel>(gridControl.DataSource as TB_PRICEPOLICYModel[]);
                 
                    this.SelectID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FPRICENUMBER").ToString();
                    this.SelectName = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FPRICENAME").ToString();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void gridControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FPRICENUMBER").ToString();
                this.SelectName = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "FPRICENAME").ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
