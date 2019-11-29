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
    public partial class FrmPurchaseOrderEdit : FrmBase
    {
        V_ICPOBILLMODEL model = new V_ICPOBILLMODEL();
        List<V_ICPOBILLENTRYMODEL> listCG = new List<V_ICPOBILLENTRYMODEL>();

        #region ■------------------ 字段相关
        public event EventHandler SaveAfter;
        private DataTable _table = new DataTable();

        //private DataTable _tableMarketArea;

        ApiService.APIServiceClient _service;

        #endregion

        #region ■------------------ 构造加载

        public FrmPurchaseOrderEdit(V_ICPOBILLMODEL pModel)
        {
            InitializeComponent();
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);


            model = pModel;
            txtCreater.Text = model.FBILLERNAME;
            txtCreater.Tag =model.FBILLER;
            txtCreater.Enabled = false;
            dateDatetime.DateTime =model.FBILLDATE;
            var listAccount = _service.GetClientAccountList(model.FBRANDID, "");
            foreach (var sub in listAccount)
            {
                if (sub.FID == model.FCLIENTID)
                {
                    txt厂家账户.Tag = sub.FID;
                    txt厂家账户.Text = sub.FNAME;
                }
            }
            txtPhone.Text = model.FTELEPHONE;
            txtRemarks.Text = model.FREMARK;


            //初始化品牌列表
            var list = _service.GetBrandList(Global.LoginUser);
            foreach (var item in list)
            {
               
                comBrand.Properties.Items.Add(item);
                if (item.FID == model.FBRANDID)
                {
                    comBrand.SelectedItem = item;
                }

            }

            listCG = _service.GetOrderEntryList(model.FID, null).ToList();

            gridControl采购订单列表.DataSource = listCG;
          
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

      
     

        #endregion


        #region ■------------------ 数据加载

        private void bgw加载数据_DoWork(object sender, DoWorkEventArgs e)
        {
         
        }

        private void bgw加载数据_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        private void bgw加载数据_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          
        }

        #endregion



     
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
          
        }

   
      

       
     
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            
            var list =  new List<V_ICPOBILLENTRYMODEL>();
            int[] rownumber = this.gridView采购订单列表.GetSelectedRows();//获取选中行号；

            foreach (var i in rownumber)
            {
                list.Add(this.gridView采购订单列表.GetRow(i) as V_ICPOBILLENTRYMODEL);
            }
            foreach (var sub in list)
            {
                listCG.Remove(sub);
            }

            gridControl采购订单列表.DataSource = listCG;
            gridControl采购订单列表.RefreshDataSource();
        }

     
        private void txt经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
            string brand = "";
            if (comBrand.SelectedItem != null)
            {
                TB_BrandModel model = comBrand.SelectedItem as TB_BrandModel;
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

        private void txt厂家账户_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var list = gridControl采购订单列表.DataSource as List<V_ICPOBILLENTRYMODEL>;
            if (list.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("明细记录数不可为空！");
                return;
            }

            string brand = "";   
            TB_BrandModel bmodel = comBrand.SelectedItem as TB_BrandModel;
            if (bmodel != null)
            {
                brand = bmodel.FID;
                IniHelper.WriteString(Global.IniUrl, "CONFIG", "FBRANDID", bmodel.FID);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("品牌不可为空！");
                return;
            }


            if (txt厂家账户.Tag == null)
            {
                System.Windows.Forms.MessageBox.Show("厂家账户不可为空！");
                return;
            }

            ICPOBILLMODEL tModel = _service.GetSingleOrder(model.FID);
            tModel.FBRANDID = bmodel.FID;
            tModel.FCLIENTID = txt厂家账户.Tag.ToStr();
            tModel.FDATE = dateDatetime.DateTime;
            tModel.FTELEPHONE = txtPhone.Text;
            tModel.FREMARK = txtRemarks.Text;           
            //tModel.FSTATUS = 3;
            int iTemp = 1;
            List<ICPOBILLENTRYMODEL> listSub = new List<ICPOBILLENTRYMODEL>();
            foreach (var sub in list)
            {
                sub.FENTRYID = iTemp;               
                ICPOBILLENTRYMODEL sub0 = new ICPOBILLENTRYMODEL();
                sub0.FADVQTY = sub.FADVQTY;
                sub0.FBATCHNO = sub.FBATCHNO;
                sub0.FCOLORNO = sub.FCOLORNO;
                sub0.FENTRYID = sub.FENTRYID;
                sub0.FICPOBILLID = sub.FICPOBILLID;
                sub0.FNEEDDATE = sub.FNEEDDATE;
                sub0.FPLANID = sub.FPLANID;
                sub0.FPRICE = sub.FPRICE;
                sub0.FREMARK = sub.FREMARK;
                sub0.FSRCCOST = sub.FSRCCOST;
                sub0.FSRCQTY = sub.FSRCQTY;
                //sub0.FSTATUS = 3;
                listSub.Add(sub0);

            }
          


           string sResult=  _service.SaveICPOBILL(tModel, listSub.ToArray());

            System.Windows.Forms.MessageBox.Show(sResult);
            if (this.SaveAfter != null)
            {
                SaveAfter(null, null);
            }
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
