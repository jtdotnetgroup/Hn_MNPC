using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hn.Client.Views.PleasePurchasePlan
{
    public partial class frm请购计划附件 : Form
    {
        ApiService.APIServiceClient _service;

        public frm请购计划附件(string id)
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);

            loadData(id);
        }

        private void loadData(string id)
        {
    

            var list = _service.GetAttachmentList(id);
            foreach (var model in list)
            {
                model.FREMARK = "下载";
            }
            gridControl附件.DataSource = list;

            //RepositoryItemHyperLinkEdit repHyperLink = new RepositoryItemHyperLinkEdit();
            ////repHyperLink.OpenLink += RepHyperLink_OpenLink;
            //gridControl附件.RepositoryItems.Add(repHyperLink);
            //gridColumn文件.ColumnEdit = repHyperLink;
            ////repHyperLink.LinkColor = Color.Maroon;
            //repHyperLink.Caption = "下载";
            //repHyperLink.bu += Frm请购计划附件_Click;
        }

        private void Frm请购计划附件_Click(object sender, EventArgs e)
        {
            MsgHelper.ShowInformation("");
        }

        private void RepHyperLink_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            if (gridView附件.FocusedRowHandle > -1)
            {
                string url = Global.WebUrl + gridView附件.GetRowCellValue(gridView附件.FocusedRowHandle, "FPATH").ToStr();
                System.Diagnostics.Process.Start(url);
            }
        }

        private void repositoryItemHyperLinkEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {


        }

        private void repositoryItemHyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            if (gridView附件.FocusedRowHandle > -1)
            {
                string url = Global.WebUrl + gridView附件.GetRowCellValue(gridView附件.FocusedRowHandle, "FPATH").ToStr();
                System.Diagnostics.Process.Start(url);
            }
        }

        private void gridControl附件_Click(object sender, EventArgs e)
        {

        }

        private void gridView附件_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
         
        }

        private void gridView附件_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "FREMARK")
            {
                if (gridView附件.FocusedRowHandle > -1)
                {
                    string url = Global.WebUrl + gridView附件.GetRowCellValue(gridView附件.FocusedRowHandle, "FPATH").ToStr();
                    System.Diagnostics.Process.Start(url);
                }
            }
        }
    }
}
