using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hn.Client.Views
{
    public partial class Form1 : Form
    {
        ApiService.APIServiceClient _service;
        public Form1()
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                List<DateTime> listDatetime = new List<DateTime>();
                DateTime dtStartTime = dateTimePicker1.Value;
                backgroundWorker1.ReportProgress(0, "正在查询...");
                string result = "";
                try
                {
                    DateTime theTime = dtStartTime;
                    backgroundWorker1.ReportProgress(0, "正在查询" + theTime.ToString("yyyy-MM-dd") + "数据");
                    listDatetime.Add(theTime);
                    result = _service.GetCCD(theTime);
                    backgroundWorker1.ReportProgress(0, theTime.ToString("yyyy-MM-dd") + "结果：" + result);
                }
                catch (Exception ee)
                {
                    result = ee.ToStr();
                    backgroundWorker1.ReportProgress(0, result);
                }



            }
            catch (Exception ee)
            {
                backgroundWorker1.ReportProgress(0, ee.ToString());
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null) labInfo.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          
            simpleButton2.Enabled = true;
            labInfo.Text = "同步完成!";
        }
    }
}
