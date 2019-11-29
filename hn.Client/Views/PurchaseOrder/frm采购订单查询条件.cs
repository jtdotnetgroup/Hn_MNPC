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
    public partial class FrmOrderQuery: Form
    {
        #region ■------------------ 字段相关

        private DataTable _table = new DataTable();

        private DataTable _tableMarketArea;

        ApiService.APIServiceClient _service;

        #endregion

        #region ■------------------ 构造加载

        public FrmOrderQuery(int status = 3)
        {
            InitializeComponent();
            txt日期开始.DateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 0:0:0"));
            txt日期结束.DateTime = DateTime.Now;
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            try
            {
                #region 请购计划列表

                initComboBox();

              

                /*
                foreach (CodeValueClass item in cbo状态.Properties.Items)
                {
                    if (item.value == status.ToStr())
                    {
                        cbo状态.SelectedItem = item;
                    }
                }
                */
                cbo状态.SelectedIndex = 0;

                //var list = _service.GetPurchasePlanList("", "", status, "", !chkClose.Checked);
                //gridControl请购计划列表.DataSource = list;
                //lbl记录数.Text = string.Format("共查询得到记录{0}条", list.Count());
                #endregion

                #region 销区列表
                // var marketAreaList = _service.GetDics("101", "", true);

                //treeList销区.DataSource = marketAreaList;

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
            cbo状态.SelectedIndex = 0;
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

      

      
        #endregion


        #region ■------------------ 数据加载

        private void bgw加载数据_DoWork(object sender, DoWorkEventArgs e)
        {
         
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
                 
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }





        #endregion

       public  Common.Cls_query.QueryOrder query = new Common.Cls_query.QueryOrder();
        private void btn查询_Click(object sender, EventArgs e)
        {
            //onSearch();
            if (cbo品牌.SelectedItem != null)
            {
                TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                if (model != null)
                    query.brand = model.FID;
            }
            if (searchControl1.Tag != null) query.famount = searchControl1.Tag.ToString();
            if (cbo状态.SelectedItem != null)
            {
                CodeValueClass model = cbo状态.SelectedItem as CodeValueClass;
                if (model != null)
                    query.t_status = model.value.ToString();
            }
          
            query.startTime = txt日期开始.DateTime;
            query.endTime = txt日期结束.DateTime;
            query.P_BillNo = txt采购单号.Text;
            query.bClose = chkClose.Checked;

            this.DialogResult = DialogResult.OK;
        }

        private void onSearch()
        {
            //string xq = "";// txt销区.Text;
            string brand = "";
            int status = 0; 
            string billno = txt采购单号.Text;

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

           // if (txt经营场所.Tag != null)
          //  {
         //       premiseid = txt经营场所.Tag.ToString();
         //   }

           // gridControl采购订单明细.DataSource = null;

        
        }

        private void txt重置_Click(object sender, EventArgs e)
        {
            //txt销区.Text = "";
            txt采购单号.Text = "";
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

   
        private void Frm_SaveAfter(object sender, EventArgs e)
        {
            onSearch();
        }

        private void btn反确认_Click(object sender, EventArgs e)
        {
        
        }

        private void btn设备预览_Click(object sender, EventArgs e)
        {

            FrmPurchaseOrder frm = new FrmPurchaseOrder();
            frm.SaveAfter += new EventHandler(btn查询_Click);
            frm.Show();
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                searchControl1.Text = frm.SelectAccount;
                searchControl1.Tag = frm.SelectID;
               
            }
        }
    }
}
