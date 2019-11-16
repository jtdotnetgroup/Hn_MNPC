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

    public delegate void GreetingDelegate(List<V_ICPOBILLENTRYMODEL> list);
    public partial class FrmPurchasePlanImport : FrmBase
    {
        public event GreetingDelegate showAfter;

        public List<string> listEntryID = new List<string>();

        #region ■------------------ 字段相关
        public event EventHandler SaveAfter;
        private DataTable _table = new DataTable();

        private DataTable _tableMarketArea;

        ApiService.APIServiceClient _service;

        #endregion

        #region ■------------------ 构造加载

        public FrmPurchasePlanImport(string pbrandid,List<string> pEntryID)
        {
            InitializeComponent();

            brandid = pbrandid;

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
                var marketAreaList = _service.GetDics("101", "", true);

                //treeList销区.DataSource = marketAreaList;

                #endregion

                bgw加载数据.RunWorkerAsync();


                listEntryID = pEntryID;



            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            query.startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 0:0:0"));
            query.endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            query.t_status = "0";
            query.brand = "01";
            
            onSearch();
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

        private void Frm_SaveAfter1(object sender, EventArgs e)
        {
            onSearch();
        }

        string brandid = "";


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
                string fid = gridView请购计划列表.GetRowCellValue(e.RowHandle, "FID").ToString();

                string fbrandname = gridView请购计划列表.GetRowCellValue(e.RowHandle, "FPREMISEBRANDNAME").ToString();

                string fbiller = gridView请购计划列表.GetRowCellValue(e.RowHandle, "FBILLERNAME").ToString();

                string fsalearea = gridView请购计划列表.GetRowCellValue(e.RowHandle, "FCLASSAREA2NAME").ToString();


                var list= _service.GetPurchasePlanEntryList(fid, null, true).ToList();
                list.RemoveAll(x => listEntryID.Contains(x.FID));

                foreach (var sub in list)
                {
                    sub.fbrandname = fbrandname;
                    sub.fbiller = fbiller;
                    sub.fsalearea = fsalearea;
                }


                gridControl请购计划明细.DataSource = list;

                V_ICPRBILLENTRYMODEL v = new V_ICPRBILLENTRYMODEL();
               

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

                List<V_ICPRBILLENTRYMODEL> list= _service.GetPurchasePlanEntryList(fid, "3", true).ToList();
                list.RemoveAll(x => listEntryID.Contains(x.FID));

                gridControl请购计划明细.DataSource = list;
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
         
            query.brand = "01"; 
            gridControl请购计划明细.DataSource = null;
            var list = _service.GetPurchasePlanImport3(
                Global.LoginUser,query);
            gridControl请购计划列表.DataSource = list;
           // lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());
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

    
        protected override void OnKeyDown(KeyEventArgs e)
        {
            /*
            switch (e.KeyCode)
            {
              
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
            */
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


      
     
       public  List<V_ICPOBILLENTRYMODEL> listCG = new List<V_ICPOBILLENTRYMODEL>();
        private void simpleButton1_Click(object sender, EventArgs e)
        {


            //gridView请购计划明细.CloseEditor();

            var list = gridView请购计划明细.DataSource as List<V_ICPRBILLENTRYMODEL>;
            if (list == null) return;
            int[] rownumber = this.gridView请购计划明细.GetSelectedRows();//获取选中行号；
            foreach (var i in rownumber)
            {
                V_ICPRBILLENTRYMODEL t = list[i];

                V_ICPOBILLENTRYMODEL sub = new V_ICPOBILLENTRYMODEL();
                sub.FITEMID = t.FITEMID;
                sub.FPRODUCTNAME = t.FPRODUCTNAME;
                sub.FSRCQTY = t.FORDERUNITQTY;
                sub.FADVQTY = t.FADVQTY;
                sub.FREMARK = t.FREMARK;
                sub.FPLANID = t.FID;
                sub.FUNITID = t.FUNITID;
                sub.FUNITNAME = t.FUNITNAME;
                sub.FCOLORNO = t.FCOLORNO;
                sub.FID = t.FID;
                listCG.Add(sub);

                /*
                V_ICPOBILLENTRYMODEL t1 = new V_ICPOBILLENTRYMODEL();
                t1.FMODEL = t.FMODEL;
                t1.FPLANID =t.FID;
                t1.FPRODUCTNAME = t.FPRODUCTNAME;
                t1.FPRODUCTTYPE = t.FPRODUCTTYPE;
                t1.FPRODUCTCODE = t.FPRODUCTCODE;
                t1.FUNITID = t.FUNITID;
                t1.FUNITNAME = t.FUNITNAME;
                t1.FORDERUNIT = t.FORDERUNIT;
                t1.FSTATUS = 1;
                t1.FSTATE = 1;
              
                t1.FBATCHNO = t.FBATCHNO;
                t1.FCOLORNO = t.FCOLORNO;
                t1.FREMARK = t.FREMARK;
                t1.FPRICE = t.FWHOLESALEPRICE;
                t1.FADVQTY = t.FADVQTY;
                t1.FASKQTY = t.FASKQTY;
                t1.FSRCQTY = t.FASKQTY;
                t1.FSRCCOST = t.FASKAMOUNT;
                t1.FNEEDDATE = t.FNEEDDATE;

                t1.FITEMID = t.FITEMID;
                t1.FSRCCODE = t.FSRCCODE;
                t1.FSRCNAME = t.FSRCNAME;
                t1.FSRCMODEL = t.FSRCMODEL;
                t1.FstockNO = t.FSTOREHOUSE;
                t1.FCOLORNO = t.FCOLORNO;
                t1.FUNITID = t.FUNITID;
                t1.FPRICE = t.FWHOLESALEPRICE;
                t1.FREMARK = t.FREMARK;               

                listCG.Add(t1);
                */
            }
            if (this.showAfter != null)
            {
                if (listCG.Count > 0)
                {
                    if (this.showAfter != null)
                    {
                        showAfter(listCG);
                    }

                }
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }

            //gridControl采购订单列表.DataSource = listCG;
            //gridControl采购订单列表.RefreshDataSource();
          

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public Common.Cls_query.P_QueryOrder query = new Common.Cls_query.P_QueryOrder();
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FrmPleasePurchasePlanQuery frmQuery = new FrmPleasePurchasePlanQuery();
            frmQuery.TopMost = true;
            if (frmQuery.ShowDialog() == DialogResult.OK)
            {
                query = frmQuery.query;
                onSearch();
            }
        }
    }
}
