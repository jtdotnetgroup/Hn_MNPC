using DevExpress.XtraGrid.Views.Grid;
using hn.Common;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace hn.Client
{
    public partial class FrmOrderList: Form
    {
        #region ■------------------ 字段相关

        private DataTable _table = new DataTable();

        //private DataTable _tableMarketArea;

        ApiService.APIServiceClient _service;

        #endregion

        #region ■------------------ 构造加载

        public FrmOrderList(int status = 3)
        {
            InitializeComponent();


            gridControl采购订单明细.MouseWheel += GridControl采购订单明细_MouseWheel;

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            try
            {
                #region 请购计划列表

                initComboBox();


             

                //var list = _service.GetPurchasePlanList("", "", status, "", !chkClose.Checked);
                //gridControl请购计划列表.DataSource = list;
                //lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());
                #endregion

                #region 销区列表
               // var marketAreaList = _service.GetDics("101", "", true);

                //treeList销区.DataSource = marketAreaList;

                #endregion

                bgw加载数据.RunWorkerAsync();

                query.startTime =DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 0:0:0"));
                query.endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
                query.t_status = "0";
                query.bClose = true;
                onSearch();
               
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

      
        }

        private void GridControl采购订单明细_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!gridView1.IsLastRow)
            {
                gridView1.MoveLast();
            }
        }

        private void initComboBox()
        {
           
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
                            RefreshDataGridHX(_table.Rows.Count, gridView采购订单列表);
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
            if (gridView采购订单列表.FocusedRowHandle > -1)
            {
                var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                var icprbilldata = list[gridView采购订单列表.GetDataSourceRowIndex(gridView采购订单列表.FocusedRowHandle)];
                if (icprbilldata.FSTATUS != 3)
                {
                    MsgHelper.ShowInformation("只有审核通过的订单才可确认！");
                    return;
                }
                string fid = icprbilldata.FID;
                var listEntry= _service.GetOrderEntryList(fid, null);
                List<ICPOBILLENTRYMODEL> listICPO = new List<ICPOBILLENTRYMODEL>();
                foreach (var sub in listEntry)
                {
                    ICPOBILLENTRYMODEL t = new ICPOBILLENTRYMODEL();
                    t.FENTRYID = sub.FENTRYID;
                    t.FICPOBILLID = sub.FICPOBILLID;
                    t.FID = sub.FID;
                    listICPO.Add(t);
                }

                if (_service.ConfirmSave_ICPO("confirm", listICPO.ToArray(), Global.LoginUser))
                {
                    MsgHelper.ShowInformation("更新成功！");//同时更新请购订单的
                    onSearch();
                }
                else
                {
                    MsgHelper.ShowInformation("更新失败！");
                }

            }
            else
            {
                MsgHelper.ShowInformation("请选择你要确认的数据！");
            }
        }

     
        private void Frm_SaveAfter1(object sender, EventArgs e)
        {
            onSearch();
        }




        #endregion

        #region ■------------------ 数据筛选

     



        #endregion

        private void gridView请购计划列表_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }


       
        private void gridView请购计划列表_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                this.Cursor = Cursors.WaitCursor;
                //222222222222222222
                string fid = gridView采购订单列表.GetRowCellValue(e.RowHandle, "FID").ToString();
               
                V_ICPOBILLENTRYMODEL[] list = _service.GetOrderEntryList(fid, null);

                foreach (var sub in list)
                {
                    ProductViewModel pro = _service.getProductView(sub.FITEMID);
                    if (pro == null) continue;
                    sub.Funit = pro.FUNITNAME;
                    sub.FSRCMODEL = pro.FSRCMODEL;
                    sub.FORDERUNIT = pro.FSRCUNIT;
                    sub.FMODEL = pro.FMODEL;
                    sub.FSRCMODEL = pro.FSRCMODEL;
                    sub.FSRCCODE = pro.FSRCCODE;
                    sub.FORDERUNIT = pro.FGROUPUNIT;
                   
                }
                gridControl采购订单明细.DataSource = list;
                this.Cursor = Cursors.Default;
            }

            //gridControl请购计划明细.DataSource = _service.GetPurchasePlanEntryList(fid);
        }


        string fid_detail = "";
        ICPOBILLENTRYMODEL[] listDetail = new ICPOBILLENTRYMODEL[] { };
        private void gridView请购计划列表_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                this.Cursor = Cursors.WaitCursor;

                optType = "6";
                if (!backgroundWorker2.IsBusy)
                {
                    fid_detail = gridView采购订单列表.GetRowCellValue(e.FocusedRowHandle, "FID").ToString();
                  
                    seButton(false);
                    backgroundWorker2.RunWorkerAsync();
                }
               
            }
        }

        private void btn退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
            optType = "0";
            onSearch();
        }

        Common.Cls_query.QueryOrder query = new Common.Cls_query.QueryOrder();

        BackgroundWorker bgSearch = new BackgroundWorker();

        V_ICPOBILLMODEL[] listSearch = new V_ICPOBILLMODEL[] { };
        private void onSearch()
        {

            simpleButton7.Text = "查询中...";
            simpleButton7.Enabled = false;
            gridControl采购订单明细.DataSource = null;

            if (!backgroundWorker2.IsBusy)
            {
                seButton(false);
                backgroundWorker2.RunWorkerAsync();
            }
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

           // string text = treeList销区.FocusedNode.GetValue("FNAME").ToString();
           // txt销区.Text = text;

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
                gridControl采购订单列表.ExportToXls(fileDialog.FileName);
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
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView采购订单列表.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    if (gridView采购订单列表.FocusedRowHandle > -1)
                    {
                        var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                        var icprbilldata = list[gridView采购订单列表.GetDataSourceRowIndex(gridView采购订单列表.FocusedRowHandle)];
                        if (icprbilldata.FSTATUS != 1&& icprbilldata.FSTATUS != 4 && icprbilldata.FSTATUS!=0&&!(icprbilldata.FSTATUS==3&&icprbilldata.FSYNCSTATUS==4))
                        {
                            MsgHelper.ShowInformation("当前状态不支持修改！");
                            return;
                        }
                        bool bzf = false;
                        if (icprbilldata.FSYNCSTATUS == 4|| icprbilldata.FSTATUS==3)
                        {
                            bzf = true;
                        }

                        FrmPurchaseOrder frm = new FrmPurchaseOrder(icprbilldata,bzf);
                        if (bzf)
                        {
                            frm.Text = "采购订单作废";
                        }
                        else
                        {
                            frm.Text = "采购订单修改";
                        }
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
            if (gridView采购订单列表.FocusedRowHandle > -1)
            {
                var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                var icprbilldata = list[gridView采购订单列表.GetDataSourceRowIndex(gridView采购订单列表.FocusedRowHandle)];
                if (icprbilldata.FSTATUS != 7)
                {
                    MsgHelper.ShowInformation("只有采购确认的订单才可反确认！");
                    return;
                }
                string fid = icprbilldata.FID;
                var listEntry = _service.GetOrderEntryList(fid, null);
                List<ICPOBILLENTRYMODEL> listICPO = new List<ICPOBILLENTRYMODEL>();
                foreach (var sub in listEntry)
                {
                    ICPOBILLENTRYMODEL t = new ICPOBILLENTRYMODEL();
                    t.FENTRYID = sub.FENTRYID;
                    t.FICPOBILLID = sub.FICPOBILLID;
                    t.FID = sub.FID;
                    listICPO.Add(t);
                }

                if (_service.ConfirmSave_ICPO("unconfirm", listICPO.ToArray(), Global.LoginUser))
                {
                    MsgHelper.ShowInformation("更新成功！");
                    onSearch();
                }
                else
                {
                    MsgHelper.ShowInformation("更新失败！");
                }

            }
            else
            {
                MsgHelper.ShowInformation("请选择你要确认的数据！");
            }
        }

        private void btn设备预览_Click(object sender, EventArgs e)
        {
           


            FrmMainB MainForm = (FrmMainB)this.Parent.Parent;
            FrmPurchaseOrder frm = new FrmPurchaseOrder();
            frm.SaveAfter += new EventHandler(btn查询_Click);
            MainForm.OpenChildForm(frm);
                      
           // frm.Show();
        }

        string fid_close = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView采购订单列表.FocusedRowHandle > -1)
            {
                var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                var icprbilldata = list[gridView采购订单列表.GetDataSourceRowIndex(gridView采购订单列表.FocusedRowHandle)];
                /*
                if (icprbilldata.FSTATUS != 5)
                {
                    MsgHelper.ShowInformation("只有未关闭的订单才可进行此操作！");
                    return;
                }
                */
                fid_close = icprbilldata.FID;
                optType = "4";
                if (!backgroundWorker2.IsBusy)
                {
                    seButton(false);
                    backgroundWorker2.RunWorkerAsync();
                }

            }
            else
            {
                MsgHelper.ShowInformation("请选择你要关闭的数据！");
            }
        }


       // string fid_sh = "";
        List<string> fid_sh = new List<string>();
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            int[] rownumber = this.gridView采购订单列表.GetSelectedRows();//获取选中行号；
            if (rownumber.Length > 0)
            {
                fid_sh = new List<string>();
                foreach (var sub in rownumber)
                {
                    var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                    var icprbilldata = list[sub];

                    if (icprbilldata.FSTATUS != 0 && icprbilldata.FSTATUS != 1 && icprbilldata.FSTATUS != 2 && icprbilldata.FSTATUS != 4)
                    {
                       // MsgHelper.ShowInformation("当前状态不可审核！");
                       // return;
                    }
                    else
                    fid_sh.Add(icprbilldata.FID);
                    
                }
                if (fid_sh.Count > 0)
                {
                    optType = "1";
                    if (!backgroundWorker2.IsBusy)
                    {
                        seButton(false);
                        backgroundWorker2.RunWorkerAsync();
                    }
                }
                else
                {
                    MsgHelper.ShowInformation("状态不可审核！");
                    return;
                }

            }
          
        }


        Dictionary<string,MApiModel.api3.Rootobject> fid_tb = new Dictionary<string, MApiModel.api3.Rootobject>();

        public void test()
        {
            Dictionary<int, string> dicEntryID_THDBMDetail = new Dictionary<int, string>();
            try
            {
                MApiModel.api24.Rootobject api24 = new MApiModel.api24.Rootobject();
                api24.action = "getMN_cp_24";
                api24.token = "";

                DateTime theTime = DateTime.Now.AddDays(-1);
                string rq1 = theTime.Year + "/" + (theTime.Month < 10 ? "0" + theTime.Month.ToStr() : theTime.Month.ToStr()) + "/" + (theTime.Day < 10 ? "0" + theTime.Day.ToStr() : theTime.Day.ToStr());

                api24.rq1 = rq1;
                api24.rq2 = rq1;
                api24.pageIndex = 1;
                api24.pageSize = 200;
                api24.pzhm = "201958944";

                MApiModel.recApi24.Rootobject r24 = new MApiModel.recApi24.Rootobject();
                r24 = _service.Remote_Get24(api24);

                Regex reg = new Regex("(\\d+)");

                foreach (var subb in r24.resultInfo)
                {
                    if (!string.IsNullOrEmpty(subb.khhm1) && reg.IsMatch(subb.khhm1))
                    {
                        int iEndtry = int.Parse(reg.Match(subb.khhm1).Groups[1].Value);
                        if (dicEntryID_THDBMDetail.ContainsKey(iEndtry))
                            dicEntryID_THDBMDetail.Add(iEndtry, subb.autoid);
                    }
                }

            }
            catch (Exception ee)
            { Console.WriteLine(ee.Message); }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            test();
            fid_tb.Clear();
            int[] rownumber = this.gridView采购订单列表.GetSelectedRows();//获取选中行号；
            if (rownumber.Length > 0)
            {
                foreach (var sub11 in rownumber)
                {
                    if (gridView采购订单列表.FocusedRowHandle > -1)
                    {
                        var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                        var icprbilldata = list[sub11];
                        if (icprbilldata.FSTATUS != 3)
                        {
                            // MsgHelper.ShowInformation("只有审核过的订单才可同步厂家！");
                            continue;
                        }
                        if (icprbilldata.FSYNCSTATUS != 0)
                        {
                            //MsgHelper.ShowInformation("该订单不可同步到厂家！");
                            continue;
                        }
                        string fid = icprbilldata.FID;
                        var listEntry = _service.GetOrderEntryList(fid, null);
                        List<ICPOBILLENTRYMODEL> listICPO = new List<ICPOBILLENTRYMODEL>();




                        foreach (var sub in listEntry)
                        {
                            if (listICPO.Any(x => x.FITEMID == sub.FITEMID))
                            {
                                ICPOBILLENTRYMODEL theOne = listICPO.First(x => x.FITEMID == sub.FITEMID);
                                theOne.FSRCQTY += (int)sub.FSRCQTY;
                            }
                            else
                            {

                                ICPOBILLENTRYMODEL t = new ICPOBILLENTRYMODEL();
                                t.FentryTotal = listEntry.Count();
                                t.FENTRYID = sub.FENTRYID;
                                t.FITEMID = sub.FITEMID;
                                t.Flevel = sub.Flevel;
                                t.FCOLORNO = sub.FCOLORNO;
                                t.FcontractNO = sub.FcontractNO;
                                t.Funit = sub.Funit;
                                t.FAUDQTY = sub.FAUDQTY;
                                t.FPRICE = sub.FPRICE;
                                t.Famount = sub.Famount;
                                t.FREMARK = sub.FREMARK;
                                t.FSRCQTY = (int)sub.FSRCQTY;
                                listICPO.Add(t);
                            }
                        }

                        ICPOBILLMODEL tempBillModel = icprbilldata;


                        Regex regInt = new Regex("(\\d+)");
                        string strHM = "";
                        string strMC = "";
                        string comid = "";

                        V_CLIENTACCOUNTModel singleDic = _service.GetClientAccountSingle(icprbilldata.FCLIENTID);
                        if (singleDic == null)
                        {
                            MsgHelper.ShowInformation("客户号码为空，不可同步！");
                            return;
                        }
                        else
                        {
                            try
                            {
                                strHM = regInt.Match(singleDic.FACCOUNT).Groups[1].Value;
                                strMC = singleDic.FNAME;
                                if (singleDic.FACCOUNT.Contains("FDK"))
                                {
                                    comid = "10";
                                }
                                else if (singleDic.FACCOUNT.Contains("MN"))
                                {
                                    comid = "2";
                                }
                                else if (singleDic.FACCOUNT.Contains("GW"))
                                {
                                    comid = "3";
                                }

                            }
                            catch
                            {

                            }
                        }
                        if (string.IsNullOrEmpty(strHM))
                        {
                            MsgHelper.ShowInformation("客户号码为空，不可同步！");
                            return;
                        }



                        MApiModel.api3.Rootobject api3 = new MApiModel.api3.Rootobject();
                        api3.action = "setMN_cp_24";
                        api3.token = "";
                        api3.comid = comid;
                        List<MApiModel.api3.Datum> listSubItems = new List<MApiModel.api3.Datum>();

                        

                        foreach (var sub in listICPO)
                        {

                            ProductViewModel pro = _service.getProductView(sub.FITEMID);



                            MApiModel.api3.Datum subItem = new MApiModel.api3.Datum();
                            subItem.sourceno = tempBillModel.FBILLNO;
                            subItem.rq = tempBillModel.FBILLDATE.Year + "/" + tempBillModel.FBILLDATE.Month + "/" + tempBillModel.FBILLDATE.Day;
                            // subItem.comid = "101";
                            subItem.khhm = strHM;
                            subItem.khmc = strMC;
                            subItem.pjhm = tempBillModel.FprojectNO;
                            subItem.zdr = "300384";
                            /////////////////////////////////////////////////////
                            string[] strArr = pro.FSRCCODE.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);



                            //产品品种
                            subItem.cppz = (strArr.Length == 3 ? strArr[0] : "");
                            //产品规格
                            subItem.cpgg = (strArr.Length == 3 ? strArr[2] : "");
                            //产品型号
                            subItem.cpxh = (strArr.Length == 3 ? strArr[1] : "");

                            subItem.cpdj = sub.Flevel == null ? "1" : sub.Flevel;
                            subItem.cpsh = string.IsNullOrEmpty(sub.FCOLORNO) ? "" : sub.FCOLORNO;

                            //产品仓号
                            subItem.cpcm = string.IsNullOrEmpty(sub.FstockNO) ? "" : sub.FstockNO;
                            // subItem.package = sub.FcontractNO
                            subItem.dw = string.IsNullOrEmpty(pro.FSRCUNIT) ? "" : pro.FSRCUNIT;
                            //包装片数
                            subItem.ks = (int)decimal.Parse(pro.FPKGFORMAT);
                            subItem.sl = (int)sub.FSRCQTY;
                            subItem.dj = sub.FPRICE;
                            subItem.je = subItem.sl * subItem.dj;//(int)sub.Famount;
                            subItem.khhm1 = sub.FENTRYID.ToStr();
                            subItem.bz = sub.FREMARK;
                            listSubItems.Add(subItem);

                        }

                        api3.data = listSubItems.ToArray();
                        //api3.data = listSubItems;

                        optType = "3";
                        string fid1 = tempBillModel.FID;
                        if (!fid_tb.ContainsKey(fid1))
                        {
                            fid_tb.Add(fid1, api3);
                        }

                     


                    }
                    else
                    {
                       
                    }
                }

                if (fid_tb.Count > 0)
                {
                    optType = "3";


                    if (!backgroundWorker2.IsBusy)
                    {
                        seButton(false);
                        backgroundWorker2.RunWorkerAsync();
                    }
                }
                else
                {
                    MsgHelper.ShowInformation("请选择你要确认的数据！");
                }




            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            FrmOrderQuery frmQuery = new FrmOrderQuery();
            if (frmQuery.ShowDialog() == DialogResult.OK)
            {
                this.query = frmQuery.query;
                optType = "0";
                onSearch();
            }
        }

        List<string> fid_fs = new List<string>();
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            int[] rownumber = this.gridView采购订单列表.GetSelectedRows();//获取选中行号；
            if (rownumber.Length > 0)
            {
                fid_fs = new List<string>();
                foreach (var sub in rownumber)
                {
                    var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                    var icprbilldata = list[sub];

                    if (icprbilldata.FSTATUS != 3)
                    {

                    }
                    else
                        fid_fs.Add(icprbilldata.FID);

                }
                if (fid_fs.Count > 0)
                {
                    optType = "2";
                    if (!backgroundWorker2.IsBusy)
                    {
                        seButton(false);
                        backgroundWorker2.RunWorkerAsync();
                    }
                }
                else
                {
                    MsgHelper.ShowInformation("状态不可反审核！");
                    return;
                }

            }


     
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }


        string fid_delete = "";
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (gridView采购订单列表.FocusedRowHandle > -1)
            {
              
                var list = gridView采购订单列表.DataSource as V_ICPOBILLMODEL[];
                var icprbilldata = list[gridView采购订单列表.GetDataSourceRowIndex(gridView采购订单列表.FocusedRowHandle)];
               
                if (icprbilldata.FSTATUS != 1&&icprbilldata.FSTATUS!=4)
                {
                    MsgHelper.ShowInformation("只有草稿或审核不通过的订单才可进行此操作！");
                    return;
                }

                fid_delete = icprbilldata.FID;

                optType = "5";
                if (!backgroundWorker2.IsBusy)
                {
                    seButton(false);
                    backgroundWorker2.RunWorkerAsync();
                }




            }
            else
            {
                MsgHelper.ShowInformation("请选择你要删除的数据！");
            }
        }


       
        private void simpleButton4_Click_1(object sender, EventArgs e)
        {

            simpleButton4.Enabled = false;
            simpleButton4.Text = "厂家库存检测中";

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            V_ICPOBILLENTRYMODEL[] listTemp = gridControl采购订单明细.DataSource as V_ICPOBILLENTRYMODEL[];

            string strAccount = "";
            try
            {
                if (gridView采购订单列表.FocusedRowHandle > -1)
                    strAccount = gridView采购订单列表.GetRowCellValue(gridView采购订单列表.FocusedRowHandle, "FACCOUNT").ToString();
            }
            catch
            {

            }

            string strDB = "100";
            
            if (strAccount.Contains("FDK"))
            {
                strDB = "10";
            }
            else if (strAccount.Contains("MN"))
            {
                strDB = "2";
            }
            else if (strAccount.Contains("GW"))
            {
                strDB = "3";
            }



            foreach (var sub in listTemp)
            {
                //FSRCQTY

                MApiModel.api2.Rootobject getapi2 = new MApiModel.api2.Rootobject();
                getapi2.cpgg = sub.GG;
                getapi2.cpxh = sub.XH;
                getapi2.pageSize = 200;
                getapi2.pageIndex = 1;
                var list1 = _service.GetStockListMN_2(getapi2,int.Parse(strDB));
                if (list1.resultInfo.Length == 0)
                {
                    sub.cjkcs = 0;
                }
                else
                {
                    int iCount = 0;
                    foreach (var sss in list1.resultInfo)
                    {
                        iCount += sss.bysl;
                    }


                    sub.cjkcs = iCount;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            simpleButton4.Enabled = true;
            simpleButton4.Text = "厂家库存检查";
           // gridControl采购订单明细.DataSource = listTemp;
            gridControl采购订单明细.RefreshDataSource();

            V_ICPOBILLENTRYMODEL[] listTemp = gridControl采购订单明细.DataSource as V_ICPOBILLENTRYMODEL[];

            





            this.Cursor = Cursors.Default;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            DevExpress.Utils.AppearanceDefault appRed = new DevExpress.Utils.AppearanceDefault
             (Color.Black, Color.Red, Color.Empty, Color.Red, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            DevExpress.Utils.AppearanceDefault appYellow = new DevExpress.Utils.AppearanceDefault
                (Color.Black, Color.Yellow, Color.Empty, Color.Yellow, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            DevExpress.Utils.AppearanceDefault appGreen = new DevExpress.Utils.AppearanceDefault
                (Color.Black, Color.Green, Color.Empty, Color.Green, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            if (e.Column.FieldName == "cjkcs")//指定列
            {
                string strDDS = gridView1.GetRowCellDisplayText(e.RowHandle, "FSRCQTY").ToString().Trim().Replace(",","");

                string strCJS = gridView1.GetRowCellDisplayText(e.RowHandle, "cjkcs").ToString().Trim().Replace(",", "");

                int iDDs = int.Parse(strDDS);
                int iCJS = int.Parse(strCJS);

                if (iCJS > 0)
                {
                    if (iCJS < iDDs)
                    {
                        DevExpress.Utils.AppearanceHelper.Apply(e.Appearance, appRed);
                    }
                    else
                    {
                       // DevExpress.Utils.AppearanceHelper.Apply(e.Appearance, appGreen);
                    }
                }


              
            }
        }


        string tuMessage = "";

        string optType = "0";//
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            tuMessage = "";


            switch (optType)
            {
                case "0":
                    try
                    {
                        listSearch = _service.GetOrderList(
                         Global.LoginUser,
                         "", query.brand, int.Parse((query.t_status == null ? "0" : query.t_status)), "", query.P_BillNo,
                         query.startTime.ToString(),
                        query.endTime.ToStr(),
                         !query.bClose);
                    }
                    catch (Exception ee)
                    {
                        tuMessage = ee.ToString();
                    }
                    break;

                case "1"://shenhe
                    try
                    {
                        foreach (var sub111 in fid_sh)
                        {
                            var listEntry = _service.GetOrderEntryList(sub111, null);
                            List<ICPOBILLENTRYMODEL> listICPO = new List<ICPOBILLENTRYMODEL>();
                            foreach (var sub in listEntry)
                            {
                                ICPOBILLENTRYMODEL t = new ICPOBILLENTRYMODEL();
                                t.FENTRYID = sub.FENTRYID;
                                t.FICPOBILLID = sub.FICPOBILLID;
                                t.FID = sub.FID;
                               
                                listICPO.Add(t);
                            }

                            if (_service.AuditSave_ICPO(listICPO.ToArray(), Global.LoginUser, ""))
                            {
                               // MsgHelper.ShowInformation("审核成功！");
                                try
                                {
                                    listSearch = _service.GetOrderList(
                                     Global.LoginUser,
                                     "", query.brand, int.Parse((query.t_status == null ? "0" : query.t_status)), "", query.P_BillNo,
                                     query.startTime.ToString(),
                                    query.endTime.ToStr(),
                                     !query.bClose);
                                }
                                catch (Exception ee)
                                {
                                    tuMessage = ee.ToString();
                                }
                            }
                            else
                            {
                                //MsgHelper.ShowInformation("审核失败！");
                            }
                        }
                    }
                    catch(Exception ee)
                    {
                        tuMessage = ee.ToStr();
                    }
                    break;

                case "2":
                    try
                    { 
                    foreach (var sub11 in fid_fs)
                    {

                        var listEntry1 = _service.GetOrderEntryList(sub11, null);
                        List<ICPOBILLENTRYMODEL> listICPO1 = new List<ICPOBILLENTRYMODEL>();
                        foreach (var sub in listEntry1)
                        {
                            ICPOBILLENTRYMODEL t = new ICPOBILLENTRYMODEL();
                            t.FENTRYID = sub.FENTRYID;
                            t.FICPOBILLID = sub.FICPOBILLID;
                            t.FID = sub.FID;
                            listICPO1.Add(t);
                        }

                        if (_service.UnAuditSave_ICPO(listICPO1.ToArray(), Global.LoginUser, ""))
                        {
                          //  MsgHelper.ShowInformation("反审成功！");
                            try
                            {
                                listSearch = _service.GetOrderList(
                                 Global.LoginUser,
                                 "", query.brand, int.Parse((query.t_status == null ? "0" : query.t_status)), "", query.P_BillNo,
                                 query.startTime.ToString(),
                                query.endTime.ToStr(),
                                 !query.bClose);
                            }
                            catch (Exception ee)
                            {
                                tuMessage = ee.ToString();
                            }
                        }
                        else
                        {
                            MsgHelper.ShowInformation("反审失败！");
                        }
                        }
                    }
                    catch (Exception ee)
                    {
                        tuMessage = ee.ToStr();
                    }
                    break;

                case "3":
                    try
                    {
                        ApiService.APIServiceClient mapi = new ApiService.APIServiceClient();
                        foreach (var sub22 in fid_tb)
                        {
                            string s = mapi.Remote_InsertICPOEntry(sub22.Value);

                            if (!s.Contains("error"))
                            {
                                Dictionary<int, string> dicEntryID_THDBMDetail = new Dictionary<int, string>();
                                try
                                {
                                    MApiModel.api24.Rootobject api24 = new MApiModel.api24.Rootobject();
                                    api24.action = "getMN_cp_24";
                                    api24.token = "";

                                    DateTime theTime = DateTime.Now;
                                    string rq1 = theTime.Year + "/" + (theTime.Month < 10 ? "0" + theTime.Month.ToStr() : theTime.Month.ToStr()) + "/" + (theTime.Day < 10 ? "0" + theTime.Day.ToStr() : theTime.Day.ToStr());

                                    api24.rq1 = rq1;
                                    api24.rq2 = rq1;
                                    api24.pageIndex = 1;
                                    api24.pageSize = 200;
                                    api24.pzhm = s;

                                    MApiModel.recApi24.Rootobject r24 = new MApiModel.recApi24.Rootobject();
                                    r24 = _service.Remote_Get24(api24);

                                    Regex reg = new Regex("(\\d+)");

                                    foreach (var subb in r24.resultInfo)
                                    {
                                        if (!string.IsNullOrEmpty(subb.khhm1) && reg.IsMatch(subb.khhm1))
                                        {
                                            int iEndtry = int.Parse(reg.Match(subb.khhm1).Groups[1].Value);
                                            if (dicEntryID_THDBMDetail.ContainsKey(iEndtry))
                                                dicEntryID_THDBMDetail.Add(iEndtry, subb.autoid);
                                        }
                                    }

                                }
                                catch(Exception ee)
                                {
                                    Console.WriteLine(ee.Message);
                                }


                                _service.Update_FSYN_Remote_Status(sub22.Key, 4, s,dicEntryID_THDBMDetail);

                                MsgHelper.ShowInformation("同步完毕！");
                                try
                                {


                                    listSearch = _service.GetOrderList(
                                     Global.LoginUser,
                                     "", query.brand, int.Parse((query.t_status == null ? "0" : query.t_status)), "", query.P_BillNo,
                                     query.startTime.ToString(),
                                    query.endTime.ToStr(),
                                     !query.bClose);
                                }
                                catch (Exception ee)
                                {
                                    tuMessage = ee.ToString();
                                }
                            }
                            else
                            {
                                MsgHelper.ShowError(s);
                            }
                        }
                    }
                    catch(Exception ee)
                    {
                        tuMessage = ee.ToStr();
                    }

                    break;

                case "4":
                    var listEntry5 = _service.GetOrderEntryList(fid_close, null);
                    List<ICPOBILLENTRYMODEL> listICPO5 = new List<ICPOBILLENTRYMODEL>();
                    foreach (var sub in listEntry5)
                    {
                        ICPOBILLENTRYMODEL t = new ICPOBILLENTRYMODEL();
                        t.FENTRYID = sub.FENTRYID;
                        t.FICPOBILLID = sub.FICPOBILLID;
                        t.FID = sub.FID;
                        listICPO5.Add(t);
                    }

                    if (_service.CloseSave_ICPO(listICPO5.ToArray(), Global.LoginUser, ""))
                    {
                        MsgHelper.ShowInformation("关闭成功！");
                        try
                        {
                            listSearch = _service.GetOrderList(
                             Global.LoginUser,
                             "", query.brand, int.Parse((query.t_status == null ? "0" : query.t_status)), "", query.P_BillNo,
                             query.startTime.ToString(),
                            query.endTime.ToStr(),
                             !query.bClose);
                        }
                        catch (Exception ee)
                        {
                            tuMessage = ee.ToString();
                        }
                    }
                    else
                    {
                        MsgHelper.ShowInformation("关闭失败！");
                    }

                    break;
                case "5":
                    if (_service.Delete_ICPO(fid_delete))
                    {
                        MsgHelper.ShowInformation("删除成功！");
                        try
                        {
                            listSearch = _service.GetOrderList(
                             Global.LoginUser,
                             "", query.brand, int.Parse((query.t_status == null ? "0" : query.t_status)), "", query.P_BillNo,
                             query.startTime.ToString(),
                            query.endTime.ToStr(),
                             !query.bClose);
                        }
                        catch (Exception ee)
                        {
                            tuMessage = ee.ToString();
                        }
                    }
                    else
                    {
                        MsgHelper.ShowInformation("删除失败！");
                    }
                    break;
                case "6":
                    try
                    {
                        listDetail = _service.GetOrderEntryList(fid_detail, null);
                    }
                    catch(Exception ee)
                    {
                        tuMessage = ee.ToString();
                    }
                    break;

            }

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            seButton(true);
            if (!string.IsNullOrEmpty(tuMessage))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(tuMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (optType == "6")
            {
                gridControl采购订单明细.DataSource = listDetail;
                this.Cursor = Cursors.Default;
            }
            else
            {
                simpleButton7.Text = "过滤查询";
                simpleButton7.Enabled = true;
                gridControl采购订单列表.DataSource = listSearch;
            }
        }

        public void seButton(bool bEnable)
        {
            simpleButton1.Enabled = bEnable;
            simpleButton2.Enabled = bEnable;
            simpleButton3.Enabled = bEnable;
            simpleButton4.Enabled = bEnable;
            simpleButton5.Enabled = bEnable;
            simpleButton6.Enabled = bEnable;
        }


    }
}
