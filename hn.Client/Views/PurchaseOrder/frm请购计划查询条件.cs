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
    public partial class FrmPleasePurchasePlanQuery : Form
    {
        #region ■------------------ 字段相关

        private DataTable _table = new DataTable();

        private DataTable _tableMarketArea;

        ApiService.APIServiceClient _service;

        #endregion

        #region ■------------------ 构造加载

    
    
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


        public FrmPleasePurchasePlanQuery(int status = 3)
        {
            InitializeComponent();

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

     

    

        #endregion

        #region ■------------------ 数据筛选

        private void searchControl经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                FrmQueryMarketArea frm = new FrmQueryMarketArea();
                frm.TopMost = true;
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



        public Common.Cls_query.P_QueryOrder query = new Common.Cls_query.P_QueryOrder();

        private void btn查询_Click(object sender, EventArgs e)
        {
            if (txt经营场所.Tag != null)
                query.address = txt经营场所.Tag.ToString();
           if(txt销区.Tag!=null)
            query.areaid = txt销区.Tag.ToString();

            if (cbo品牌.SelectedItem != null)
            {
                TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                if (model != null)
                    query.brand = model.FID;
            }

            query.startTime = txt日期开始.DateTime;
            query.endTime = txt日期结束.DateTime;
            query.P_BillNo = txt请购单号.Text;

            this.DialogResult = DialogResult.OK;
        }

      

        private void txt重置_Click(object sender, EventArgs e)
        {
            txt销区.Text = "";
            txt请购单号.Text = "";
            cbo品牌.Text = "";
            txt日期开始.Text = "";
            txt日期结束.Text = "";
          
            txt经营场所.Text = "";
            txt经营场所.Tag = null;
        
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

    

    

  
    

  
    }
}
