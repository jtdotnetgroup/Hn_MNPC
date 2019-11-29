using DevExpress.XtraGrid.Views.Grid;
using hn.Client.Views.Diglog;
using hn.Common;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static hn.Client.FrmPleasePurchasePlan;

namespace hn.Client
{
    public partial class FrmSendGoodsPlan : Form
    {
        #region ■------------------ 字段相关

        private DataTable _table = new DataTable();

        private ApiService.APIServiceClient _service;

        private V_ICSEOUTBILLMODEL[] _dataSrouce;

        private int _status = 0;

        #endregion

        #region ■------------------ 构造加载

        public FrmSendGoodsPlan(int status = -1)
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            try
            {
                //var list = _service.GetDeliveryList("", "", "", 0, "", "");
                //gridControl发货计划列表.DataSource = list;
                ////lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());



                // bgw加载数据.RunWorkerAsync();

                initComboBox();

                txt日期开始.Text = formatDateTime(DateTime.Now.AddDays(-16));
                txt日期结束.Text = formatDateTime(DateTime.Now);
                gridView发货计划列表.OptionsView.ColumnAutoWidth = true;
                foreach (CodeValueClass item in cbo状态.Properties.Items)
                {
                    if (item.value == status.ToStr())
                    {
                        cbo状态.SelectedItem = item;
                    }
                }

                #region 销区列表
                var marketAreaList = _service.GetDics("101", "", true);
                treeList销区.DataSource = marketAreaList;
                #endregion
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
            cbo状态.Properties.Items.Add(new CodeValueClass("1", "草稿"));
            cbo状态.Properties.Items.Add(new CodeValueClass("2", "待审核"));
            cbo状态.Properties.Items.Add(new CodeValueClass("3", "审核通过"));
            cbo状态.Properties.Items.Add(new CodeValueClass("4", "审核不通过"));
            cbo状态.Properties.Items.Add(new CodeValueClass("5", "关闭"));
            cbo状态.Properties.Items.Add(new CodeValueClass("6", "完成"));

        }

        #endregion


        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmSendGoodsPlan), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmSendGoodsPlan), msg);
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

        private void gridView发货计划列表_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
            e.SelectImageIndex = 18;
        }

        #endregion

        #region ■------------------ 按钮实现

        private void btn新增_Click(object sender, EventArgs e)
        {
            FrmPPPImmediateSendGoods frm = new FrmPPPImmediateSendGoods();
            frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    this.onSearch();
            //}
        }

        public void FrmPPPImmediateSendGoods_SaveAfter(object obj, EventArgs e)
        {
            optype = "0";
            this.onSearch();
        }

        private void Frm_SaveAfter(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btn编辑_Click(object sender, EventArgs e)
        {
            if(gridControl发货计划明细.DataSource==null|| (gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>).Count == 0)
            {
                return;
            }

            if (gridView发货计划列表.FocusedRowHandle > -1)
            {
                var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(gridView发货计划列表.FocusedRowHandle);
                var rowData = _dataSrouce[rowIndex];

                string billno = gridView发货计划列表.GetRowCellValue(gridView发货计划列表.FocusedRowHandle, "FBILLNO").ToStr();
                if (!string.IsNullOrEmpty(billno))
                {
                    //进入发货计划编辑
                    FrmPPPImmediateSendGoods frm = new FrmPPPImmediateSendGoods();
                    frm.IcseoutbillModel = rowData;
                    //frm.IcseoutbillEntrys = new List<V_ICSEOUTBILLENTRYMODEL>(gridControl发货计划明细.DataSource as V_ICSEOUTBILLENTRYMODEL[]);
                    frm.IcseoutbillEntrys = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                    frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
                    frm.Show();
                    //if (frm.ShowDialog() == DialogResult.OK)
                    //{
                    //    this.onSearch();
                    //}
                }
                else
                {
                    //进入组柜编辑
                    FrmSGPGroupCounter frm = new FrmSGPGroupCounter();
                    frm.IcseoutbillModel = rowData;
                    frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
                    frm.Show();
                    //if (frm.ShowDialog() == DialogResult.OK)
                    //{
                    //    this.onSearch();
                    //}
                }
            }
        }

        private void btn组柜_Click(object sender, EventArgs e)
        {
            FrmSGPGroupCounter frm = new FrmSGPGroupCounter();
            frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.onSearch();
                btn组柜_Click(null, null);
            }
        }

        private void btn重新组柜_Click(object sender, EventArgs e)
        {
            if (gridView发货计划列表.FocusedRowHandle > -1)
            {
                var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(gridView发货计划列表.FocusedRowHandle);
                var rowData = _dataSrouce[rowIndex];

                //进入组柜编辑
                FrmSGPGroupCounter frm = new FrmSGPGroupCounter();
                frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
                frm.IcseoutbillModel = rowData;
                frm.Show();
                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    this.onSearch();
                //}
            }
            else
            {
                MsgHelper.ShowInformation("请选择你要处理的数据！");
            }
        }

        private void btn标识批录_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            var selectRowIndexs = gridView发货计划列表.GetSelectedRows();

            foreach (var index in selectRowIndexs)
            {
                var rowData = GetRowByIndex<V_ICSEOUTBILLMODEL>(index);
                list.Add(rowData.FID);
                
            }

            if (list.Count > 0)
            {
                ids = string.Join(",", list.ToArray());
            }

            if (ids != "")
            {
                string centen = Interaction.InputBox("请是输入标识内容", "标题", "", -1, -1);
                if (_service.UpdateIdentification(ids, centen))
                {
                    MsgHelper.ShowError("标识保存成功！");
                    this.onSearch();
                }
            }
            else
            {
                MsgHelper.ShowError("请选择要处理的数据！");
            }
        }




        #endregion

        #region ■------------------ 数据筛选

        private void searchControl经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        #endregion


        #region ■------------------ 数据加载

        private void bgw加载数据_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i < 31; i++)
                {
                    DataRow newRow = _table.NewRow();
                    newRow["组柜编号"] = "组柜编号" + i;
                    newRow["车辆信息"] = "车辆信息" + i;
                    newRow["品牌"] = "品牌" + i;
                    newRow["二级销区"] = "二级销区" + i;
                    newRow["单据编号"] = "单据编号" + i;
                    newRow["状态"] = i;
                    newRow["运输方式"] = "运输方式" + i;
                    newRow["车牌号"] = "车牌号" + i;
                    newRow["载重"] = "载重" + i;
                    newRow["工程名称"] = "工程名称" + i;
                    newRow["发货仓库"] = "发货仓库" + i;
                    newRow["同步状态"] = "同步状态" + i;
                    //_table.Rows.Add(newRow);
                    bgw加载数据.ReportProgress(1, newRow);
                }
                //RefreshDataGridHX(_table.Rows.Count, gridView发货计划列表);
                bgw加载数据.ReportProgress(2, null);
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
                        _table.Rows.Add(e.UserState as DataRow);
                        break;
                    case 2:
                        RefreshDataGridHX(_table.Rows.Count, gridView发货计划列表);
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

        private void gridView发货计划列表_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (e.FocusedRowHandle > -1)
            //{
            //    string fid = gridView发货计划列表.GetRowCellValue(e.FocusedRowHandle, "FID").ToString();

            //    Action<object> action = (data) =>
            //    {
            //        getDeliveryEntryListByFid(data);
            //    };


            //    BeginInvoke(action, fid);
            //}
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
            this.onSearch();
        }

        private void btn重置_Click(object sender, EventArgs e)
        {
            txt销区.Text = "";
            cbo品牌.Text = "";
            cbo状态.Text = "";

            txt厂家账户.Text = "";
            txt厂家账户.Tag = null;
            txt承运公司.Text = "";
            txt承运公司.Tag = null;

            txt厂家单号.Text = "";
            txt车辆.Text = "";
            txt发货计划单号.Text = "";
            txt工程名称.Text = "";
            txt组柜单号.Text = "";
            txt日期开始.Text = formatDateTime(DateTime.Now);
            txt日期结束.Text = formatDateTime(DateTime.Now);

            var list = _service.GetDeliveryList(Global.LoginUser, "", "", "", 0, "", "", "", "", "", "", "", "",
                formatDateTime(txt日期开始.DateTime),
                formatDateTime(txt日期结束.DateTime), !chkClose.Checked);
            gridControl发货计划列表.DataSource = list;
        }

        private void treeList销区_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string text = treeList销区.FocusedNode.GetValue("FNAME").ToString();
            txt销区.Text = text;

            this.onSearch();
        }

        private void btn刷新_Click(object sender, EventArgs e)
        {
            this.onSearch();
        }


      

        string xq = "";
        string brand = "";
        int status = 0;
        string account = "";
        string expresscompany = "";

        string startdate = "";
        string enddate = "";
        string car = "";
        private void onSearch()
        {
            xq = txt销区.Text;
            brand = "";
            status = 0;
            account = "";
            expresscompany = "";
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

            if (txt厂家账户.Tag != null)
            {
                account = txt厂家账户.Tag.ToString();
            }
            if (txt承运公司.Tag != null)
            {
                expresscompany = txt承运公司.Tag.ToString();
            }

            car = txt车辆.Text;

            startdate = formatDateTime(txt日期开始.DateTime);
            enddate = formatDateTime(txt日期结束.DateTime);


            gridControl发货计划明细.DataSource = null;


            optype = "0";
            if (backgroundWorker2.IsBusy == false)
            {
                setButton(false);
                backgroundWorker2.RunWorkerAsync();
            }

            



        }

        private void btn退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn删除_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            var selectRowIndexs = gridView发货计划列表.GetSelectedRows();

            foreach (var index in selectRowIndexs)
            {
                //var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(index);
                //var rowData = _dataSrouce[rowIndex];
                var rowData = GetRowByIndex<V_ICSEOUTBILLMODEL>(index);
                if ( rowData!=null&&(rowData.FSTATUS == 1 || rowData.FSTATUS == 2 || rowData.FSTATUS == 4 || rowData.FSTATUS == 6))
                {
                    list.Add(rowData.FID);
                }

            }

            //for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            //{
            //    bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
            //    if (b)
            //    {
            //        var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
            //        var rowData = _dataSrouce[rowIndex];
            //        if (rowData.FSTATUS == 1 || rowData.FSTATUS == 2 || rowData.FSTATUS == 4 || rowData.FSTATUS == 6)
            //        {
            //            list.Add(rowData.FID);
            //        }
            //    }
            //}

            if (list.Count > 0)
            {
                ids = string.Join(",", list.ToArray());
            }

            if (ids != "")
            {
                if (MsgHelper.AskQuestion("确认要删除选中的单据，以及删除对应的其他关联信息吗?"))
                {
                    //调用发货计划删除接口
                    int res = _service.DeleteDeliveryByIDs(ids);
                    if (res > 0)
                    {
                        MsgHelper.ShowInformation("删除成功！");
                        this.onSearch();
                    }
                }
            }
            else
            {
                MsgHelper.ShowError("请选择要删除的数据，只能删除未审核的数据");
            }
        }


        string ids_sh = "";
        List<string> list = new List<string>();
        private void btn审核_Click(object sender, EventArgs e)
        {
            string ids = "";
             list = new List<string>();

             var selectRowIndexs = gridView发货计划列表.GetSelectedRows();

             foreach (var index in selectRowIndexs)
             {
                //var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(index);
                //var rowData = _dataSrouce[rowIndex];
                var rowData = GetRowByIndex<V_ICSEOUTBILLMODEL>(index);
                if ( rowData!=null&&(rowData.FSTATUS == 1 || rowData.FSTATUS == 2) && !string.IsNullOrEmpty(rowData.FBILLNO))
                {
                    list.Add(rowData.FID);
                }

            }

           
            if (list.Count > 0)
            {
                ids_sh = string.Join(",", list.ToArray());
            }

            if (ids_sh != "")
            {
                if (!backgroundWorker2.IsBusy)
                {
                    optype = "2";
                    setButton(false);
                    backgroundWorker2.RunWorkerAsync();
                }


              

            }
            else
            {
                MsgHelper.ShowError("审核只能针对待已审核和已生成单号的数据，请确认您选择数据是否正确！");
            }
        }

        /// <summary>
        /// 根据行索引获取行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        private T GetRowByIndex<T>(int index) where T:class
        {
            var result = gridView发货计划列表.GetRow(index) as T;
            return result;
        }


        private void btn反审_Click(object sender, EventArgs e)
        {
            string ids = "";
            list = new List<string>();

            //for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            //{
            //    bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
            //    if (b)
            //    {
            //        var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
            //        var rowData = _dataSrouce[rowIndex];
            //        if (rowData.FSYNCSTATUS < 1)
            //        {
            //            list.Add(rowData.FID);
            //        }
            //    }
            //}

            var selectRowIndexs = gridView发货计划列表.GetSelectedRows();

            foreach (var index in selectRowIndexs)
            {
                var rowData = GetRowByIndex<V_ICSEOUTBILLMODEL>(index);
                if (rowData!=null&&rowData.FSYNCSTATUS < 1)
                {
                    list.Add(rowData.FID);
                }

            }

            if (list.Count > 0)
            {
                ids_sh = string.Join(",", list.ToArray());
            }

            if (ids_sh != "")
            {
                optype = "3";

             

                if (MsgHelper.AskQuestion("点击确认退回到待审核状态，可修改资料！"))
                {
                    if (!backgroundWorker2.IsBusy)
                    {
                        setButton(false);
                        backgroundWorker2.RunWorkerAsync();
                    }

                  
                }
            }
            else
            {
                MsgHelper.ShowError("反审核只能针对已审核并且未同步到厂家的数据，请先勾选您要处理的数据！");
            }
        }


        Dictionary<string, MApiModel.api12.Rootobject> fid_tb = new Dictionary<string, MApiModel.api12.Rootobject>();


        List<V_ICSEOUTBILLMODEL> listModels = new List<V_ICSEOUTBILLMODEL>();

        private void btn提交同步_Click(object sender, EventArgs e)
        {
            fid_tb.Clear();
            List<string> list = new List<string>();
            listModels.Clear();
          


            string comid = "2";
            //for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            //{

            //    bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
            //    if (b)
            //    {

            //        var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
            //        var rowData = _dataSrouce[rowIndex];
            //        if (rowData.FSTATUS == 3 && (rowData.FSYNCSTATUS == 0 || rowData.FSYNCSTATUS == -1))
            //        {
            //            listModels.Add(rowData);  
            //        }

            //    }
            //}
            var selectRowIndexs = gridView发货计划列表.GetSelectedRows();

            foreach (var index in selectRowIndexs)
            {
                var rowData = GetRowByIndex<V_ICSEOUTBILLMODEL>(index);
                if (rowData!=null&&(rowData.FSTATUS == 3 && (rowData.FSYNCSTATUS == 0 || rowData.FSYNCSTATUS == -1)))
                {
                    listModels.Add(rowData);
                }
            }

            if (listModels.Count > 0)
            {
                if (!backgroundWorker2.IsBusy)
                {
                    optype = "4";
                    backgroundWorker2.RunWorkerAsync();
                }else
                {
                    MsgHelper.ShowInformation("系统繁忙，请稍后再试！");
                }
            }
            else
            {
                MsgHelper.ShowError("请选中符合条件的记录！");
            }

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
                gridControl发货计划列表.ExportToXls(fileDialog.FileName);
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
                        btn新增_Click(null, null);
                        break;
                    }
                case Keys.F4:
                    {
                        btn编辑_Click(null, null);
                        break;
                    }
                case Keys.F5:
                    {
                        onSearch();
                        break;
                    }
                case Keys.F6:
                    {
                        btn组柜_Click(null, null);
                        break;
                    }
                case Keys.F7:
                    {
                        btn重新组柜_Click(null, null);
                        break;
                    }
                case Keys.F8:
                    {
                        btn删除_Click(null, null);
                        break;
                    }
                case Keys.F9:
                    {
                        btn审核_Click(null, null);
                        break;
                    }
                case Keys.F10:
                    {
                        btn反审_Click(null, null);
                        break;
                    }
                case Keys.F11:
                    {
                        btn提交同步_Click(null, null);
                        break;
                    }
                case Keys.F12:
                    {
                        btn标识批录_Click(null, null);
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

        //private void gridView发货计划列表_RowCellClick(object sender, RowCellClickEventArgs e)
        //{
        //    if (e.RowHandle > -1)
        //    {
        //        string fid = gridView发货计划列表.GetRowCellValue(e.RowHandle, "FID").ToString();

        //        var list = _service.GetDeliveryEntryList(fid);

        //        foreach (var sub in list)
        //        {
        //            v_thdModel v = _service.getTHD(sub.thdbm);
        //            sub.khmc = v.khmc;
        //            sub.khhm = v.khhm;
        //            sub.gg = v.cpgg;
        //            sub.xh = v.cpxh;
        //            sub.pz = v.cppz;
        //            sub.dw = v.dw;
        //            sub.dj = v.dj;
        //            sub.pz = v.cppz;
        //            sub.xh = v.cpxh;
        //            sub.gg = v.cpgg;
        //            sub.khhm = v.khhm;
        //            sub.khmc = v.khmc;
        //            sub.cpdj = v.cpdj;
        //            sub.pzhm = v.pzhm;
        //            sub.kdrq = v.rq.ToString("yyyy-MM-dd");
        //            sub.cpcm = v.cpcm;
        //            sub.cpsh = v.cpsh;
        //        }

        //        gridControl发货计划明细.DataSource = list;
        //    }
        //}

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string brand = "";
            if (cbo品牌.SelectedItem != null)
            {
                TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                if (model != null)
                    brand = model.FID;
            }

            FrmQueryClientAccount frm = new FrmQueryClientAccount(brand);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txt厂家账户.Text = frm.SelectName;
                txt厂家账户.Tag = frm.SelectID;
            }
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryExpressCompany frm = new FrmQueryExpressCompany();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txt承运公司.Text = frm.SelectName;
                txt承运公司.Tag = frm.SelectID;
            }
        }

        private void gridView发货计划列表_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "FSYNCSTATUS")
            {
                switch (e.CellValue.ToInt())
                {
                    case 1:
                    case 2:
                        {
                            e.Appearance.ForeColor = Color.Blue;
                            break;
                        }
                    case -1:
                        {
                            e.Appearance.ForeColor = Color.Red;
                            break;
                        }
                    case 3:
                        {
                            e.Appearance.ForeColor = Color.Gray;
                            break;
                        }
                }
            }
        }

        private void gridView发货计划列表_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "FSYNCSTATUS")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "等待上传";
                        break;
                    case "1":
                        e.DisplayText = "已上传到厂家";
                        break;
                    case "-1":
                        e.DisplayText = "厂家检查不通过";
                        break;
                    case "2":
                        e.DisplayText = "厂家更新成功";
                        break;
                    case "3":
                        e.DisplayText = "上传完成";
                        break;
                }
            }
        }

        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            if (!pnl日期.Visible)
            {
                pnl日期.Visible = true;
                pnl跑龙套2.Height = pnl跑龙套2.Height + 40;
            }
            else
            {
                pnl日期.Visible = false;
                pnl跑龙套2.Height = pnl跑龙套2.Height - 40;
            }
        }

        private string formatDateTime(DateTime date)
        {
            System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd";

            return date.ToString("yyyy/MM/dd", dtFormat);
        }

        private void btn拆单_Click(object sender, EventArgs e)
        {
            if (gridView发货计划列表.FocusedRowHandle > -1)
            {
                var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(gridView发货计划列表.FocusedRowHandle);
                var rowData = _dataSrouce[rowIndex];

                string billno = gridView发货计划列表.GetRowCellValue(gridView发货计划列表.FocusedRowHandle, "FBILLNO").ToStr();
                if (!string.IsNullOrEmpty(billno) & rowData.FSTATUS == 3 && rowData.FSYNCSTATUS == 0)
                {
                    //进入发货计划编辑
                    frm发货计划拆单 frm = new frm发货计划拆单();
                    frm.IcseoutbillModel = rowData;
                    frm.IcseoutbillEntrys = new List<V_ICSEOUTBILLENTRYMODEL>(gridControl发货计划明细.DataSource as V_ICSEOUTBILLENTRYMODEL[]);
                    frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
                    frm.Show();
                }
                else
                {
                    MsgHelper.ShowError("请选择已发货、审核通过，并且未提交到厂家的的数据进行拆单");
                }
            }
        }

        //private void chk全选_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (var model in _dataSrouce)
        //    {
        //        model.FCHECK = chk全选.Checked;
        //    }
        //    gridControl发货计划列表.DataSource = _dataSrouce;
        //    gridControl发货计划列表.RefreshDataSource();
        //}

      

        private void btn约车_Click(object sender, EventArgs e)
        {
            //string ids = "";
            //List<string> list = new List<string>();

            //for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            //{
            //    bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
            //    if (b)
            //    {
            //        var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
            //        var rowData = _dataSrouce[rowIndex];

            //        //发货计划单处于草稿状态，并且约车状态为待发布的时候用户即可勾选多条记录进行约车
            //        if (rowData.FSTATUS == 1 && rowData.FCAR_STATUS == 1)
            //        {
            //            list.Add(rowData.FID);
            //        }
            //    }
            //}

            //if (list.Count > 0)
            //{
            //    ids = string.Join(",", list.ToArray());
            //}

            //if (ids != "")
            //{
            //    _service.ContractCar(list.ToArray());
            //}
            //else
            //{
            //    MsgHelper.ShowError("请选择要处理的数据！");
            //}

            string ids = "";
            List<string> list = new List<string>();

            var selectRowIndexs = gridView发货计划列表.GetSelectedRows();

            foreach (var index in selectRowIndexs)
            {
                var rowData = GetRowByIndex<V_ICSEOUTBILLMODEL>(index);

                //发货计划单处于草稿状态，并且约车状态为待发布的时候用户即可勾选多条记录进行约车
                //if (rowData.FSTATUS == 1 && rowData.FCAR_STATUS == 1)
                if (rowData!=null&&rowData.FCAR_STATUS == 1)
                {
                    list.Add(rowData.FID);
                }
            }

            

            if (list.Count > 0)
            {
                try
                {
                    _service.ContractCar(list.ToArray());
                    MsgHelper.ShowInformation("处理完成！");
                    this.onSearch();
                }
                catch (Exception ex)
                {
                    MsgHelper.ShowInformation(ex.Message);
                }
                
            }
            else
            {
                MsgHelper.ShowError("请选择要处理的数据！");
            }
        }

        //private void gridView发货计划列表_Click(object sender, EventArgs e)
        //{
        //    if (gridView发货计划列表.FocusedRowHandle > -1)
        //    {
        //        string fid = gridView发货计划列表.GetRowCellValue(gridView发货计划列表.FocusedRowHandle, "FID").ToString();

        //        new Thread(() =>
        //        {
                    
        //        }).Start();

                

        //        //var list = _service.GetDeliveryEntryList(fid);

        //        //foreach (var sub in list)
        //        //{
        //        //    v_thdModel v = _service.getTHD(sub.thdbm);
        //        //    sub.khmc = v.khmc;
        //        //    sub.khhm = v.khhm;
        //        //    sub.gg = v.cpgg;
        //        //    sub.xh = v.cpxh;
        //        //    sub.pz = v.cppz;
        //        //    sub.dw = v.dw;
        //        //    sub.dj = v.dj;
        //        //    sub.pz = v.cppz;
        //        //    sub.xh = v.cpxh;
        //        //    sub.gg = v.cpgg;
        //        //    sub.khhm = v.khhm;
        //        //    sub.khmc = v.khmc;
        //        //    sub.cpdj = v.cpdj;
        //        //    sub.pzhm = v.pzhm;
        //        //    sub.kdrq = v.rq.ToString("yyyy-MM-dd");
        //        //    sub.cpcm = v.cpcm;
        //        //    sub.cpsh = v.cpsh;
        //        //}

        //        //gridControl发货计划明细.DataSource = list;

        //        // gridControl发货计划明细.DataSource = _service.GetDeliveryEntryList(fid);
        //    }
        //}

        public void getDeliveryEntryListByFid(object fid)
        {

            var list = _service.GetDeliveryEntryList(fid.ToString()).ToList();

            var thdbmList = list.Select(p => p.thdbm).ToArray();

            var v_thdmodelList = _service.getTHDList(thdbmList);

            var action = new Action<List<V_ICSEOUTBILLENTRYMODEL>>((data) =>
            {
                bindData(data);
            });


            if (v_thdmodelList == null)
            {
                list = new List<V_ICSEOUTBILLENTRYMODEL>();
                Invoke(action, list);
                return;
            }

            //v_thdModel v = _service.getTHD(sub.thdbm);
            
                foreach (var sub in list)
                {
                    var v = v_thdmodelList.FirstOrDefault(p => p.AUTOID.ToString() == sub.thdbm);

                    sub.khmc = v.khmc;
                    sub.khhm = v.khhm;
                    sub.gg = v.cpgg;
                    sub.xh = v.cpxh;
                    sub.pz = v.cppz;
                    sub.dw = v.dw;
                    sub.dj = v.dj;
                    sub.pz = v.cppz;
                    sub.xh = v.cpxh;
                    sub.gg = v.cpgg;
                    sub.khhm = v.khhm;
                    sub.khmc = v.khmc;
                    sub.cpdj = v.cpdj;
                    sub.pzhm = v.pzhm;
                    sub.kdrq = v.rq.ToString("yyyy-MM-dd");
                    sub.cpcm = v.cpcm;
                    sub.cpsh = v.cpsh;
                }
            
            

            

            Invoke(action, list);       

        }

        private void bindData(List<V_ICSEOUTBILLENTRYMODEL> list)
        {
            gridControl发货计划明细.DataSource = list;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            //int[] rownumber = this.gridView发货计划列表.GetSelectedRows();
            //foreach (int number in rownumber)
            //{
            //    var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(number);
            //    var rowData = _dataSrouce[rowIndex];
            //    if (rowData.FSTATUS == 3 && (rowData.FSYNCSTATUS == 0 || rowData.FSYNCSTATUS == -1))
            //    {
            //        list.Add(rowData.FID);
            //    }
            //}


            //for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            //{
            //    bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
            //    if (b)
            //    {
            //        var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
            //        var rowData = _dataSrouce[rowIndex];
            //        _service.SynDeliveryNot(rowData.FID);

            //    }
            //}

            var selectRowIndexs = gridView发货计划列表.GetSelectedRows();

            foreach (var index in selectRowIndexs)
            {
                var rowData = GetRowByIndex<V_ICSEOUTBILLMODEL>(index);
                _service.SynDeliveryNot(rowData.FID);
            }

            this.onSearch();
        }


        string optype = "";



        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            tumMessage = "";
            switch (optype)
            {
                case "0":
                    try
                    { 
                    _dataSrouce = _service.GetDeliveryList(
                Global.LoginUser,
                brand,
                xq == "全部" ? "" : xq,
                "",
                status,
                car,
                "",
                account,
                expresscompany,
                txt厂家单号.Text,
                txt发货计划单号.Text,
                txt组柜单号.Text,
                txt工程名称.Text,
                startdate == "0001/01/01" ? "" : startdate,
                enddate == "0001/01/01" ? "" : enddate,
                !chkClose.Checked);
                    }
                    catch (Exception ee)
                    {
                        tumMessage = ee.ToStr();
                    }


                    break;

                case "2":
                    try
                    {
                        FrmAuditDialog dialog = new FrmAuditDialog("审核", "请选择你要做的处理", "通过", "不通过");
                        DialogResult result = dialog.ShowDialog();
                        if (result == DialogResult.Yes)
                        {
                            //调用发货计划审核接口
                            int res3 = _service.AuditDeliveryByIDs(ids_sh, 3, Global.LoginUser);
                            MsgHelper.ShowInformation("处理成功！");

                        }
                        else if (result == DialogResult.No)
                        {
                            //调用发货计划审核接口
                            int res2 = _service.AuditDeliveryByIDs(ids_sh, 4, Global.LoginUser);
                            MsgHelper.ShowInformation("处理成功！");

                        }

                        foreach (var model in _dataSrouce)
                        {
                            foreach (string id in list)
                            {
                                if (model.FID == id)
                                {
                                    model.FCHECK = true;
                                }
                            }
                        }
                    }
                    catch(Exception ee)
                    {
                        tumMessage = ee.ToStr();
                    }
                    break;

                case "3":
                    //调用发货计划删除接口
                    try
                    { 
                    int res = _service.AuditAntiDeliveryByIDs(ids_sh);
                    if (res > 0)
                    {
                        MsgHelper.ShowInformation("处理成功！");

                        }
                    }
                    catch (Exception ee)
                    {
                        tumMessage = ee.ToStr();
                    }
                    break;

                case "4":
                    //调用发货计划接口
                    try
                    {
                        fid_tb.Clear();
                        for(int k=0;k<listModels.Count;k++)
                        {
                            V_ICSEOUTBILLMODEL rowData = listModels[k];
                            List<MApiModel.api12.Datum> LItem = new List<MApiModel.api12.Datum>();
                            var entryList = _service.GetDeliveryEntryList(rowData.FID);

                            foreach (var sub22 in entryList)
                            {
                                v_thdModel v = _service.getTHD(sub22.thdbm);
                                sub22.khmc = v.khmc;
                                sub22.khhm = v.khhm;
                                sub22.gg = v.cpgg;
                                sub22.xh = v.cpxh;
                                sub22.pz = v.cppz;
                                sub22.dw = v.dw;
                                sub22.dj = v.dj;
                                sub22.pz = v.cppz;
                                sub22.xh = v.cpxh;
                                sub22.gg = v.cpgg;
                                sub22.khhm = v.khhm;
                                sub22.khmc = v.khmc;
                                sub22.cpdj = v.cpdj;
                                sub22.pzhm = v.pzhm;
                                sub22.kdrq = v.rq.ToString("yyyy-MM-dd");
                                sub22.cpcm = v.cpcm;
                                sub22.cpsh = v.cpsh;
                            }

                            string comid = "2";

                            foreach (var subEntry in entryList)
                            {
                                v_thdModel vTHD = _service.getTHD(subEntry.thdbm);
                                comid = vTHD.DB;

                                //list.Add(rowData.FID);
                                MApiModel.api12.Datum subItem = new MApiModel.api12.Datum();
                                subItem.pzhm = rowData.FBILLNO;
                                subItem.rq = rowData.FBILLDATE.Year + "/" + (rowData.FBILLDATE.Month < 10 ? "0" + rowData.FBILLDATE.Month.ToStr() : rowData.FBILLDATE.Month.ToStr()) + "/" + (rowData.FBILLDATE.Day < 10 ? "0" + rowData.FBILLDATE.Day.ToStr() : rowData.FBILLDATE.Day.ToStr());
                                subItem.khhm = vTHD.khhm;
                                subItem.khmc = vTHD.khmc;

                                subItem.pzlb = "";
                                subItem.cplb = 0;
                                //subItem.pjhm = string.IsNullOrEmpty(rowData.thdbm)?"":rowData.thdbm;
                                subItem.zdr = "300384";
                                subItem.pjhm = string.IsNullOrEmpty(vTHD.pzhm) ? "" : vTHD.pzhm;

                                // string[] strArr = pro.FSRCCODE.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                                //产品品种

                                subItem.cppz = vTHD.cppz;
                                //产品规格
                                subItem.cpgg = vTHD.cpgg;
                                //产品型号
                                subItem.cpxh = vTHD.cpxh;


                                subItem.cpdj = vTHD.cpdj;
                                subItem.cpsh = string.IsNullOrEmpty(vTHD.cpsh) ? "" : vTHD.cpsh;// string.IsNullOrEmpty(subEntry.FCOLORNO)?"":subEntry.FCOLORNO;
                                subItem.cpcm = string.IsNullOrEmpty(vTHD.cpcm) ? "" : vTHD.cpcm;// string.IsNullOrEmpty(subEntry.FSTOCKNUMBER)?0:int.Parse(subEntry.FSTOCKNUMBER);
                                subItem.package = vTHD.tpackage;
                                subItem.dw = vTHD.dw;
                                subItem.ks = int.Parse(vTHD.ks);
                                subItem.sl = (int)subEntry.FCOMMITQTY;
                                subItem.bz = string.IsNullOrEmpty(subEntry.FREMARK) ? "" : subEntry.FREMARK;
                                subItem.gg = vTHD.gg == null ? "" :vTHD.gg;

                                try
                                {
                                    decimal dGGS = subItem.ks * subItem.sl * decimal.Parse(vTHD.gg);
                                    subItem.ggs = dGGS.ToStr();
                                }
                                catch
                                {
                                    subItem.ggs = vTHD.GGS == null ? "" :vTHD.GGS;
                                }

                                
                                subItem.pjhm1 = "";
                                //
                                //subItem.package = vTHD.tpackage;
                                subItem.pjhm2 = "";
                                subItem.telphone = string.IsNullOrEmpty(rowData.FDELIVERERTEL) ? "123456789" : rowData.FDELIVERERTEL;

                                string strCarno = "";
                                strCarno += string.IsNullOrEmpty(rowData.FCARNUMBER) ? "" : rowData.FCARNUMBER;
                                strCarno += string.IsNullOrEmpty(rowData.FDELIVERER) ? "" : " 司机:" + rowData.FDELIVERER;
                                strCarno += string.IsNullOrEmpty(rowData.FDELIVERERTEL) ? "" : " 电话:" + rowData.FDELIVERERTEL;

                               decimal dLength=  decimal.Parse(IniHelper.ReadString(Global.IniUrl, "CONFIG", "cph", "40"));

                                if (strCarno.Length > (int)dLength) strCarno = strCarno.Substring(0, (int)dLength-1);

                                subItem.carno = strCarno.Trim().Replace("  ","");


                                subItem.jsdz = string.IsNullOrEmpty(rowData.FRECEIVER_DISTRICT_NAME) ? "广东省" : rowData.FRECEIVER_DISTRICT_NAME;
                                subItem.jsr = subItem.telphone;
                                subItem.pjhm3 = rowData.FBILLNO.Replace("DP", "");
                                subItem.ysfs = string.IsNullOrEmpty(rowData.FDELIVERY_METHODNAME) ? "" : rowData.FDELIVERY_METHODNAME;
                                subItem.jsfs = "";
                                subItem.Province = string.IsNullOrEmpty(rowData.FRECEIVER_PROVINCE_NAME) ? "广东省" : rowData.FRECEIVER_PROVINCE_NAME;
                                subItem.City = string.IsNullOrEmpty(rowData.FRECEIVER_CITY_NAME) ? "佛山市" : rowData.FRECEIVER_CITY_NAME;
                                subItem.Region = "南海区";
                                subItem.bz =string.IsNullOrEmpty( rowData.FREMARK)?"":rowData.FREMARK;
                                LItem.Add(subItem);
                            }

                            if (LItem.Count > 0)
                            {
                                MApiModel.api12.Rootobject getapi6 = new MApiModel.api12.Rootobject();
                                getapi6.data = LItem.ToArray();
                                getapi6.comid = int.Parse(comid);
                                string fid = rowData.FID;

                                if (!fid_tb.ContainsKey(fid)&&fid!="")
                                {
                                    fid_tb.Add(fid, getapi6);
                                }
                            }

                        }


                        List<string> listFalse = new List<string>();
                        int iIndex = 0;
                        List<string> listKeys = new List<string>();
                        foreach (var sub333 in fid_tb)
                        {
                            listKeys.Add(sub333.Key);
                        }
                        foreach (string sub33 in listKeys)
                        {
                            if (!fid_tb.ContainsKey(sub33)) continue;
                            iIndex++;
                            string k = sub33;
                          
                            MApiModel.api12.Rootobject v = fid_tb[sub33];
                            var jsonData = JsonHelper.ToJson(v);
                            string res1 = _service.SyncDeliveryByIDsMN(jsonData, k);
                            if (res1 == "1")
                            {
                                //MsgHelper.ShowInformation("处理完成！");
                            }
                            else
                            {
                                listFalse.Add("第" + iIndex + "条发生错误：" + res1);
                            }
                        }                        

                        if (listFalse.Count > 0)
                        {
                            string mes = "";
                            foreach (var sub in listFalse)
                            {
                                mes += sub+"\r\n";
                            }

                            MsgHelper.ShowError(mes);
                        }
                    }
                    catch (Exception ee)
                    {
                        tumMessage = ee.ToStr();
                    }

                    break;
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            setButton(true);
            if (tumMessage != "")
            {
                MsgHelper.ShowInformation(tumMessage);
            }
            else
            {
                if (optype == "0")
                    gridControl发货计划列表.DataSource = _dataSrouce;
                else
                    this.onSearch();
            }


        }

        string tumMessage = "";

        public void setButton(bool bEnable)
        {
            btn审核.Enabled = bEnable;
            btn反审.Enabled = bEnable;
            btn提交同步.Enabled = bEnable;

        }

        private void gridView发货计划列表_DoubleClick(object sender, EventArgs e)
        {
            btn编辑_Click(null,null);
        }

        private void gridView发货计划列表_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "FCHECK")
            {
                gridView发货计划列表.SetRowCellValue(e.RowHandle, e.Column, !Convert.ToBoolean(e.CellValue));
                return;
            }

            if (e.RowHandle > -1)
            {
                string fid = gridView发货计划列表.GetRowCellValue(e.RowHandle, "FID").ToString();

                Action<object> action = (data) =>
                {
                    getDeliveryEntryListByFid(data);
                };


                Thread t1 = new Thread(new ParameterizedThreadStart(action));

                t1.Start(fid);
            }
        }

        private void gridView发货计划列表_Click(object sender, EventArgs e)
        {
            
        }
    }
}
