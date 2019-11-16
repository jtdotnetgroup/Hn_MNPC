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
    public partial class frm库存查询 : Form
    {
        #region ■------------------ 字段相关

        private DataTable _table = new DataTable();

        ApiService.APIServiceClient _service;

        #endregion

        #region ■------------------ 构造加载

        public frm库存查询(int status = 3)
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
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

            loadDrop();

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




        private void btn退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
            onSearch();
        }

        MApiModel.recApi2.Rootobject list = new MApiModel.recApi2.Rootobject();
        
        private void onSearch()
        {
            this.Cursor = Cursors.WaitCursor;
            btn查询.Enabled = false;
            labInfo.Text = "查询中...";
            list = new MApiModel.recApi2.Rootobject();
            this.Cursor = Cursors.Default;
            bgw加载数据.RunWorkerAsync();
        }

        private void txt重置_Click(object sender, EventArgs e)
        {
           
            tColorNo.Text = "";
        }

        private void gridView1_CustomDrawRowIndicator_1(object sender, RowIndicatorCustomDrawEventArgs e)
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    {
                        onSearch();
                        break;
                    }
                case Keys.F6:
                    {
                        txt重置_Click(null, null);
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

        private void bgw加载数据_DoWork(object sender, DoWorkEventArgs e)
        {

            MApiModel.api2.Rootobject getapi2 = new MApiModel.api2.Rootobject();
            getapi2.cpgg = tGGXH.Text;
            getapi2.cpxh = tColorNo.Text;
            getapi2.pageSize = (int)numPerPage.Value;
            getapi2.pageIndex = (int)numPageIndex.Value;

            list = _service.GetStockListMN(getapi2);
           
        }

        private void bgw加载数据_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (list.status == 0)
            {
                labInfo.Text = "查询得到" + list.resultInfo.Length + "条记录";
                btn查询.Enabled = true;
                gridControl1.DataSource = list.resultInfo;
            }
            else {
                labInfo.Text = "查询失败：" + list.msg;
            }
        }

        private void pnl跑龙套2_Paint(object sender, PaintEventArgs e)
        {

        }


        public void loadDrop()
        {
            List<string> listData = new List<string>();
            listData.Add("300*300");
            listData.Add("300*450");
            listData.Add("300*600");
            listData.Add("240*660");
            listData.Add("400*800");          
            listData.Add("150*900");
            listData.Add("200*1200");
            listData.Add("600*600");
            listData.Add("600*900");
            listData.Add("600*1200");
            listData.Add("800*800");
            listData.Add("900*900");
            listData.Add("900*1800");
            listData.Add("1200*2400");
            listData.Add("1500*1500");
            listData.Add("1000*1000");
            comGG.Properties.Items.Clear();
            foreach (var item in listData)
            {
                comGG.Properties.Items.Add(item);
            }
        }

        private void comGG_SelectedIndexChanged(object sender, EventArgs e)
        {
            string yjfsid = "";
            if (comGG.SelectedItem != null)
            {
                // SYS_SUBDICSMODEL sub = com运输方式.SelectedItem as SYS_SUBDICSMODEL;
                yjfsid = comGG.SelectedItem.ToStr();
            }
            tGGXH.Text = yjfsid;
        }
    }
}
