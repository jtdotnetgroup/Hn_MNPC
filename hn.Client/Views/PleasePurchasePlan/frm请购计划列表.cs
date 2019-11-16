using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FrmPleasePurchasePlan : Form
    {
        #region ■------------------ 字段相关

        private DataTable _table = new DataTable();

        private DataTable _tableMarketArea;

        ApiService.APIServiceClient _service;

        #endregion

        #region ■------------------ 构造加载

        public FrmPleasePurchasePlan(int status = 3)
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            try
            {
                #region 请购计划列表

                initComboBox();


                foreach (CodeValueClass item in cbo状态.Properties.Items)
                {
                    if (item.value == status.ToStr())
                    {
                        cbo状态.SelectedItem = item;
                    }
                }

                //var list = _service.GetPurchasePlanList("", "", status, "", !chkClose.Checked);
                //gridControl请购计划列表.DataSource = list;
                //lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());
                #endregion

                #region 销区列表
                var marketAreaList = _service.GetDics("101", "", true);

                treeList销区.DataSource = marketAreaList;

                #endregion

                bgw加载数据.RunWorkerAsync();


            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void initComboBox()
        {
            //初始化品牌列表
            var list = _service.GetBrandList(Global.LoginUser);
            foreach (var item in list)
            {
                cbo品牌.Properties.Items.Add(item);

                string brandid = IniHelper.ReadString(Global.IniUrl, "CONFIG", "FBRANDID", "");
                if (item.FID == brandid)
                {
                    cbo品牌.SelectedItem = item;
                }
            }

            //初始化单据状态选择            
            cbo状态.Properties.Items.Add(new CodeValueClass("0", "全部"));
            cbo状态.Properties.Items.Add(new CodeValueClass("3", "审核通过"));
            cbo状态.Properties.Items.Add(new CodeValueClass("7", "采购确认"));
            cbo状态.Properties.Items.Add(new CodeValueClass("1", "草稿"));
            cbo状态.Properties.Items.Add(new CodeValueClass("2", "待审核"));            
            cbo状态.Properties.Items.Add(new CodeValueClass("4", "审核不通过"));
            cbo状态.Properties.Items.Add(new CodeValueClass("6", "完成"));
            cbo状态.Properties.Items.Add(new CodeValueClass("5", "关闭"));
        }
        #endregion

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmPleasePurchasePlan), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmPleasePurchasePlan), msg);
        }

        #endregion

        #region ■------------------ 界面实现

        private void panel左_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //using (Pen pen = new Pen(Color.FromArgb(204, 206, 219)))
                //{
                //    e.Graphics.DrawLine(pen, new Point(panel左.Width - 1, 0), new Point(panel左.Width - 1, panel左.Height));
                //}
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void panel销区标题_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //using (Pen pen = new Pen(Color.FromArgb(204, 206, 219)))
                //{
                //    e.Graphics.DrawLine(pen, new Point(0, panel销区标题.Height - 1), new Point(panel销区标题.Width, panel销区标题.Height - 1));
                //}
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void pnl跑龙套1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                using (Pen pen = new Pen(Color.FromArgb(204, 206, 219)))
                {
                    e.Graphics.DrawLine(pen, new Point(0, pnl跑龙套1.Height - 1), new Point(pnl跑龙套1.Width, pnl跑龙套1.Height - 1));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }


        /// <summary>
        /// 调整行号宽度
        /// </summary>
        /// <param name="gridView"></param>
        private void RefreshDataGridHX(int rowsCount, GridView gridView)
        {
            try
            {
                //行号宽度
                int Num = rowsCount;
                if (Num <= 99)
                {
                    if (gridView.IndicatorWidth != 37)
                    {
                        gridView.IndicatorWidth = 37;
                        gridView.InvalidateRows();
                    }
                }
                else if (Num > 99 && Num <= 999)
                {
                    if (gridView.IndicatorWidth != 45)
                    {
                        gridView.IndicatorWidth = 45;
                        gridView.InvalidateRows();
                    }
                }
                else if (Num > 999)
                {
                    if (gridView.IndicatorWidth != 53)
                    {
                        gridView.IndicatorWidth = 53;
                        gridView.InvalidateRows();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void gridView设备列表_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void treeList销区_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
        {
            e.SelectImageIndex = 8;
        }

        #endregion


        #region ■------------------ 数据加载

        private void bgw加载数据_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region 请购计划列表

                for (int i = 0; i < 31; i++)
                {
                    DataRow newRow = _table.NewRow();
                    newRow["经营场所"] = "经营场所" + i;
                    newRow["二级销区"] = "二级销区" + i;
                    newRow["品牌部"] = "品牌部" + i;
                    newRow["计划类型"] = "计划类型" + i;
                    newRow["请购单号"] = Convert.ToInt64(DateTime.Now.ToString("yyyMMdd")) * 10000 + (long)1 + (long)i;
                    newRow["状态"] = i;
                    newRow["申请日期"] = DateTime.Now.ToString("yyy-MM-dd");
                    newRow["运输方式"] = "运输方式" + i;
                    newRow["工程性质"] = "工程性质" + i;
                    newRow["运费结算"] = "运费结算" + i;
                    newRow["收货地址"] = "收货地址" + i;
                    bgw加载数据.ReportProgress(1, newRow);
                    //_table.Rows.Add(newRow);
                }
                bgw加载数据.ReportProgress(2, null);
                //RefreshDataGridHX(_table.Rows.Count, gridView请购计划列表);

                #endregion


            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void bgw加载数据_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                switch (e.ProgressPercentage)
                {
                    case 1:
                        {
                            _table.Rows.Add(e.UserState as DataRow);
                        }
                        break;
                    case 2:
                        {
                            RefreshDataGridHX(_table.Rows.Count, gridView请购计划列表);
                        }
                        break;
                    case 3:
                        {
                            Global.TableMarketArea.Rows.Add(e.UserState as DataRow);
                        }
                        break;
                    case 4:
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void bgw加载数据_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        #endregion



        #region ■------------------ 按钮实现

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (gridView请购计划列表.FocusedRowHandle > -1)
            {
                string fid = gridView请购计划列表.GetRowCellValue(gridView请购计划列表.FocusedRowHandle, "FID").ToString();
                int status = gridView请购计划列表.GetRowCellValue(gridView请购计划列表.FocusedRowHandle, "FSTATUS").ToInt();
                if (status == 3)
                {
                    FrmPPPConfirm frm = new FrmPPPConfirm(fid, "confirm");
                    frm.ParentGridView = gridView请购计划列表;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.onSearch();
                    }
                }
                else
                {
                    MsgHelper.ShowInformation("只能确认已审核的数据！");
                }
            }
            else
            {
                MsgHelper.ShowInformation("请选择你要确认的数据！");
            }
        }

        private void btn直接发货_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView请购计划列表.FocusedRowHandle > -1)
                {
                    int status = gridView请购计划列表.GetRowCellValue(gridView请购计划列表.FocusedRowHandle, "FSTATUS").ToInt();
                    if (status != 7)
                    {
                        MsgHelper.ShowInformation("单据未确认，无法发货！");
                        return;
                    }

                    var list = gridView请购计划列表.DataSource as V_ICPRBILLMODEL[];

                     FrmPPPImmediateSendGoods frm = new FrmPPPImmediateSendGoods();
                    frm.请购计划Data = list[gridView请购计划列表.GetDataSourceRowIndex(gridView请购计划列表.FocusedRowHandle)];
                    frm.请购计划明细Data = this.gridView请购计划明细.DataSource as hn.DataAccess.Model.V_ICPRBILLENTRYMODEL[];
                    frm.SaveAfter += Frm_SaveAfter1;
                    frm.Show();
                }
                else
                {
                    MsgHelper.ShowInformation("请选择你要发货的数据！");
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void Frm_SaveAfter1(object sender, EventArgs e)
        {
            onSearch();
        }




        #endregion

        #region ■------------------ 数据筛选

        private void searchControl经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                FrmQueryMarketArea frm = new FrmQueryMarketArea();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txt经营场所.Text = frm.SelectName;
                    txt经营场所.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }





        #endregion

        private void gridView请购计划列表_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }
        private void gridView请购计划列表_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                this.Cursor = Cursors.WaitCursor;
                string fid = gridView请购计划列表.GetRowCellValue(e.RowHandle, "FID").ToString();
                gridControl请购计划明细.DataSource = _service.GetPurchasePlanEntryList(fid, null,false);
                this.Cursor = Cursors.Default;
            }

            //gridControl请购计划明细.DataSource = _service.GetPurchasePlanEntryList(fid);
        }

        private void gridView请购计划列表_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                this.Cursor = Cursors.WaitCursor;
                string fid = gridView请购计划列表.GetRowCellValue(e.FocusedRowHandle, "FID").ToString();
                gridControl请购计划明细.DataSource = _service.GetPurchasePlanEntryList(fid, null,false);
                this.Cursor = Cursors.Default;
            }
        }

        private void btn退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
            onSearch();
        }

        private void onSearch()
        {
            string xq = txt销区.Text;
            string brand = "";
            int status = 0;
            string premiseid = "";
            string billno = txt请购单号.Text;

            if (cbo品牌.SelectedItem != null)
            {
                TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                if (model != null)
                {
                    brand = model.FID;
                    IniHelper.WriteString(Global.IniUrl, "CONFIG", "FBRANDID", model.FID);
                }
            }
            if (cbo状态.SelectedItem != null)
            {
                CodeValueClass model = cbo状态.SelectedItem as CodeValueClass;
                if (model != null)
                    status = model.value.ToInt();
            }

            if (txt经营场所.Tag != null)
            {
                premiseid = txt经营场所.Tag.ToString();
            }

            gridControl请购计划明细.DataSource = null;

            var list = _service.GetPurchasePlanList(
                Global.LoginUser,
                xq == "全部" ? "" : xq, brand, status, premiseid,billno,
                formatDateTime(txt日期开始.DateTime),
                formatDateTime(txt日期结束.DateTime),
                !chkClose.Checked);
            gridControl请购计划列表.DataSource = list;
            lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());
        }

        private void txt重置_Click(object sender, EventArgs e)
        {
            txt销区.Text = "";
            txt请购单号.Text = "";
            cbo品牌.Text = "";
            txt日期开始.Text = "";
            txt日期结束.Text = "";
            foreach (CodeValueClass item in cbo状态.Properties.Items)
            {
                if (item.value == "3")
                {
                    cbo状态.SelectedItem = item;
                }
            }
            txt经营场所.Text = "";
            txt经营场所.Tag = null;
            chkClose.Checked = true;
            var list = _service.GetPurchasePlanList(Global.LoginUser, "", "", 3, "", "",
                formatDateTime(txt日期开始.DateTime),
                formatDateTime(txt日期结束.DateTime), 
                !chkClose.Checked);
            gridControl请购计划列表.DataSource = list;
            lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());
        }

        private string formatDateTime(DateTime date)
        {
            System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd";

            return date.ToString("yyyy/MM/dd", dtFormat);
        }

        public class CodeValueClass
        {
            public string value;
            public string text;

            public CodeValueClass(string v, string t)
            {
                value = v;
                text = t;
            }

            public override string ToString()
            {
                return text;
            }
        }

        private void treeList销区_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //string text = treeList销区.FocusedNode.GetValue("FNAME").ToString();

            //var list = _service.GetPurchasePlanList(text == "全部" ? "" : text, "", 0, "");
            //gridControl请购计划列表.DataSource = list;
            //lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());

            string text = treeList销区.FocusedNode.GetValue("FNAME").ToString();
            txt销区.Text = text;

            this.onSearch();
        }

        private void btn导出_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gridControl请购计划列表.ExportToXls(fileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    {
                        btn导出_Click(null, null);
                        break;
                    }
                case Keys.F3:
                    {
                        btnConfirm_Click(null, null);
                        break;
                    }
                case Keys.F4:
                    {
                        btn反确认_Click(null, null);
                        break;
                    }
                case Keys.F5:
                    {
                        btn人工关闭_Click(null, null);
                        break;
                    }                    
                case Keys.F6:
                    {
                        btn直接发货_Click(null, null);
                        break;
                    }
                case Keys.Escape:
                    {
                        this.Close();
                        break;
                    }
            }
            base.OnKeyDown(e);
        }

        private void gridView请购计划列表_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView请购计划列表.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    if (gridView请购计划列表.FocusedRowHandle > -1)
                    {
                        var list = gridView请购计划列表.DataSource as V_ICPRBILLMODEL[];
                        var icprbilldata = list[gridView请购计划列表.GetDataSourceRowIndex(gridView请购计划列表.FocusedRowHandle)];

                        frm请购计划编辑 frm = new frm请购计划编辑(icprbilldata.FID);
                        frm.ICPRBILLData = icprbilldata;
                        frm.SaveAfter += Frm_SaveAfter;
                        frm.Show();
                    }
                }
            }
        }

        private void Frm_SaveAfter(object sender, EventArgs e)
        {
            onSearch();
        }

        private void btn反确认_Click(object sender, EventArgs e)
        {
            if (gridView请购计划列表.FocusedRowHandle > -1)
            {
                string fid = gridView请购计划列表.GetRowCellValue(gridView请购计划列表.FocusedRowHandle, "FID").ToString();
                int status = gridView请购计划列表.GetRowCellValue(gridView请购计划列表.FocusedRowHandle, "FSTATUS").ToInt();
                if (status == 7)
                {
                    FrmPPPConfirm frm = new FrmPPPConfirm(fid, "unconfirm");
                    frm.ParentGridView = gridView请购计划列表;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.onSearch();
                    }
                }
                else
                {
                    MsgHelper.ShowInformation("只能反确认已确认的数据！");
                }
            }
            else
            {
                MsgHelper.ShowInformation("请选择你要确认的数据！");
            }
        }

        private void btn人工关闭_Click(object sender, EventArgs e)
        {
            if (gridView请购计划列表.FocusedRowHandle > -1)
            {
                var list = gridView请购计划列表.DataSource as V_ICPRBILLMODEL[];
                var icprbilldata = list[gridView请购计划列表.GetDataSourceRowIndex(gridView请购计划列表.FocusedRowHandle)];

                frm请购计划编辑 frm = new frm请购计划编辑(icprbilldata.FID);
                frm.IsRowClose = true;
                frm.Text = "请购计划-人工关闭";
                frm.ICPRBILLData = icprbilldata;
                frm.Show();
            }
        }
    }
}
